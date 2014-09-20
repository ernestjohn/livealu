﻿/// <reference path="oauth.js" />
/// <reference path="E:\MY CODE\Kudevolve\KudevolveWeb\Auth/jstorage.js" />
/// <reference path="linq.js" />
/// <reference path="flavr.js" />

// Declares how the application should be bootstrapped. See: http://docs.angularjs.org/guide/module
var app = angular.module('app', []);

// Path: /
app.controller('DashboardCtrl', ['$scope', function ($scope) {
    $scope.posts = [];

    var loggedUserId = sessionStorage.getItem("userid");

    //First Hit a APi request to get data into the scope
    $.get("http://kudevolvemain.azurewebsites.net/api/v1/posts", function (data) {
        //Update the Scope object data
        $scope.posts = data;
    });


    $.get("http://kudevolvemain.azurewebsites.net/api/v1/discussions", function (data) {
        //Update the Scope object data
        $scope.discussions = data;
    });

    $.get("http://kudevolvemain.azurewebsites.net/api/v1/petitions", function (data) {
        //Update the Scope object data
        $scope.petitions = data;
    });

    $.get("http://kudevolvemain.azurewebsites.net/api/v1/suggestions", function (data) {
        //Update the Scope object data
        $scope.suggestions = data;
    });

    $.get("http://kudevolvemain.azurewebsites.net/api/v1/articles", function (data) {
        //Update the Scope object data
        $scope.articles = data;
    });

    //The Signalr Code
    var kuhub = $.connection.notificationStreamer;

    kuhub.client.addPostComment = function (data) {
        var arr = data.split("|", 2);
        postid = arr[0];
        comment = arr[1];

        var index = Enumerable.From($scope.posts).IndexOf( Enumerable.From($scope.posts).Where(function (pst) {
            pst.PostId == postid
        }).FirstOrDefault());

        //Add the comment to the actual post
        $scope.posts[index].comments.push(comment);
        $scope.$apply();
    }

    kuhub.client.addNewPost = function (mypost) {
        post = JSON.parse(mypost);
       // alert("From Signalr Server" + post);
        console.log("New post added");
        $scope.posts.push(post);
        //alert(JSON.stringify($scope.posts));
        console.log("Scope push no error!");
        $scope.$apply();
    }
   

    kuhub.client.addPetition = function () {
        
    }

    kuhub.client.addTweet = function (tweet) {
        //Add a tweet from the streaming server
    }

    kuhub.client.notify = function (message) {
        toastr.info("Online message: " + message);
        //$scope.newpost = message;
        $scope.$apply();
        //alert($scope.newpost);
        console.log(message);
    }

    $.connection.hub.start().done(function () {
        //Log the connection beginning
        console.log("Signalr is Online");
        //Add the user signalr connections
        var userconn = new Object();
        userconn.userid = loggedUserId;
        userconn.connectionid = $.connection.id;
        var usercon = JSON.stringify(userconn);

        //Call the signalr-based real time post sender
        $.ajax({
            url: 'http://kudevolvemain.azurewebsites.net/api/v1/users/' + loggedUserId + '/signalrconnections',
            async: true,
            type: 'POST',
            data: JSON.stringify(usercon),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                toastr.info("You have successfully connected your user id to signalr and your groups")
            },
            error: function (x, y, z) {
                alert('Oooops!!' + x + '\n' + y + '\n' + z);
            }
        });
    });


    //End the Signalr Code

    //Code for sharing a post
    var share = new Share(".share-button", {
        title: "Share Post",
        text: $scope.postShared,
        networks: {
            facebook: {
                app_id: "651257824946190",
                before: function () {
                    console.log("BEFORE", this);

                },
                after: function () {
                    console.log("User shared:", this.url);
                }
            }
        }
    });

    $scope.posts.addComment = function (post) {
        var mypostid = post.PostId;
        var html =
'   <div class="form-row">' +
'       <textarea ng-model="commento" class="form-control" name="comm" rows="2" placeholder="Enter Comment here" cols="*" style="margin: 0px; width: 295px; height: 89px; color: black;"></textarea>'
        '   </div>';
        var commen = "";
        
        new $.flavr({
            title: 'Make a comment on post',
            iconPath: 'http://kudevolvemain.azurewebsites.net/icons/',
            icon: 'chat-bubble.png',
            content: post.Content,
            dialog: 'form',
            form: { content: html, addClass: 'form-html' },
            onSubmit: function ($container, $form) {
                var obj = $form.serialize();
                
                var dta = $form.serialize();
                var resp = dta.split("=", 2);
                var re = resp[1];
                commen = re;
                alert(re);//This is the actual comment in the post
                
                
                //alert($form.valueOf());
                return false;
            }
           
        });
        var com = new Object();
        com.PostUser = loggedUserId;
        com.Content = commen;


        //Call the signalr-based real time post sender
        $.ajax({
            url: 'http://kudevolvemain.azurewebsites.net/api/v1/posts/' + mypostid + '/comments',
            async: true,
            type: 'POST',
            data: JSON.stringify(com),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                toastr.info("You have successfully made a comment")
            },
            error: function (x, y, z) {
                alert('Oooops!!' + x + '\n' + y + '\n' + z);
            }
        });

    }

    $scope.sharePost = function (postToBeshared) {
        $scope.postShared = postToBeshared;
    }

    //The Refresh Function
    $scope.refreshPosts = function () {

        $.get("http://kudevolvemain.azurewebsites.net/api/v1/posts", function (data) {
            //Update the Scope object data
            $scope.posts = data;
        });
    }

    $scope.notAvailable = function () {

        new $.flavr({
            content: 'Welcome to Kudevolve',
            iconPath: 'http://kudevolvemain.azurewebsites.net/icons/',
            icon: 'star.png',
            buttons: {
                Ok: { style: 'info' },
                Cancel: { style: 'danger' }
            }
        });

    }

    $scope.addComment = function (postid, comment) {

    }

    $scope.addPost = function () {

        var postToPost = new Object();//Build the object
        postToPost.OwnerId = "";
        postToPost.Content = $scope.newpost;

        //alert($scope.newpost);
       
        //Make the post data as a JSON String
        var postdata = { Ownerid: "ernesto", Content: $scope.newpost };
        var DTo = JSON.stringify(postToPost);

        //Call the signalr-based real time post sender
        $.ajax({
            url: 'http://kudevolvemain.azurewebsites.net/api/v1/posts',
            async: true,
            type: 'POST',
            data: JSON.stringify(postToPost),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                toastr.info("You have successfully made a post")
            },
            error: function (x, y, z) {
                alert('Oooops!!' + x + '\n' + y + '\n' + z);
            }
        });


    }

}]);


app.controller('LoginCtrl', ['$scope', function ($scope) {

    //The simple login code
        $scope.simplelogin = function () {

        var loginObject = new Object();
        loginObject.Email = $scope.email;
        loginObject.Password = $scope.password;

        var loginData = JSON.stringify(loginObject);

        //Make the REST call
        //Call the signalr-based real time post sender
        $.ajax({
            url: 'http://kudevolvemain.azurewebsites.net/api/users/login',
            async: true,
            type: 'POST',
            data: JSON.stringify(loginData),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                sessionStorage.setItem("userid", data.Id);
                sessionStorage.setItem("userlogged", true);
                window.location = "http://kudevolvemain.azurewebsites.net/dashboard/index";
                toastr.info("You have successfully Logged In")
            },
            error: function (x, y, z) {
                alert('Oooops!!' + x + '\n' + y + '\n' + z);
            }
        });

    }

    //Code to be the listener after the user logs into the app. Gets the users profile
    OAuth.initialize('Vyvllf3OhAQrHCFhXfIiYG_iz20');

    $scope.facebook = function () {

        var provider = 'facebook';
        var options = "";

        OAuth.popup('facebook', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            result.get("https://graph.facebook.com/v2.0/me")
            .done(function (user_info) {
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(JSON.stringify(user_info));
                //Set The Angular Scope values to the new stuff

                sessionStorage.setItem("firstname", user_info.first_name);
                sessionStorage.setItem("email", user_info.email);
                sessionStorage.setItem("middlename", user_info.last_name);
                sessionStorage.setItem("name", user_info.name);


                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }
    $scope.twitter = function () {

        OAuth.popup('twitter', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            result.get("https://api.twitter.com/1.1/account/verify_credentials.json")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }

    $scope.instagram = function () {

        OAuth.popup('instagram', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            result.get("https://api.instagram.com/v1/users/self")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }

    $scope.linkedin = function () {

        OAuth.popup('linkedin', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            alert(JSON.stringify(result));
            result.get("https://api.linkedin.com/v1/people/~:(id,first-name,last-name,industry,headline,summary)?format=json")
            .done(function (user_info) {
               // var data = jQuery.parseXML(user_info);
                alert(JSON.stringify(user_info));
                alert(user_info);
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }

    $scope.google = function () {

        OAuth.popup('google_plus', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            result.get("https://www.googleapis.com/plus/v1/people/me")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

       
    }

    $scope.windows = function () {

        OAuth.popup('live', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            alert(error);
            alert(result);
            result.get("https://apis.live.net/v5.0/me")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });
    }


}]);



app.controller('RegisterCtrl', ['$scope', function ($scope) {

    $scope.newuser = null;
    OAuth.initialize('Vyvllf3OhAQrHCFhXfIiYG_iz20');

    $scope.facebook = function () {

        var provider = 'facebook';
        var options = "";

        OAuth.popup('facebook', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            result.get("https://graph.facebook.com/v2.1/me?fields=id,name,bio,about,email,birthday,first_name,gender,last_name,middle_name,timezone,is_verified,link,locale")
            .done(function (user_info) {
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                //alert(JSON.stringify(user_info));
                //Set The Angular Scope values to the new stuff

                $scope.FirstName = user_info.first_name;
                $scope.LastName = user_info.last_name;
                $scope.Email = user_info.email;
                $scope.UserName = user_info.name;

                $scope.$apply();

                new $.flavr({
                    content: 'Facebook Connection <br/> Thank you for connecting Kudevolve with your facebook account. <br/> However more info is required <br/> Admin(Sensei)',
                    iconPath: 'http://kudevolvemain.azurewebsites.net/icons/',
                    icon: 'star.png',
                    buttons: {
                        Ok: { style: 'info' }
                       
                    }
                });
               
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }
    $scope.twitter = function () {

        OAuth.popup('twitter', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            result.get("https://api.twitter.com/1.1/account/verify_credentials.json")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }

    $scope.instagram = function () {

        OAuth.popup('instagram', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            result.get("https://api.instagram.com/v1/users/self")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }

    $scope.linkedin = function () {

        OAuth.popup('linkedin', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            alert(JSON.stringify(result));
            result.get("https://api.linkedin.com/v1/people/~:(id,first-name,last-name,industry,headline,summary)?format=json")
            .done(function (user_info) {
                // var data = jQuery.parseXML(user_info);
                alert(JSON.stringify(user_info));
                alert(user_info);
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });

    }

    $scope.google = function () {

        OAuth.popup('google_plus', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            //alert(error);
            alert(result);
            result.get("https://www.googleapis.com/plus/v1/people/me")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });


    }

    $scope.windows = function () {

        OAuth.popup('live', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            alert(error);
            alert(result);
            result.get("https://apis.live.net/v5.0/me")
            .done(function (user_info) {
                alert(JSON.stringify(user_info));
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response
                alert(user_info.email);
            })
            .fail(function (error) {
                // handle errors here
                alert(error);
            })

        });
    }


    $scope.register = function () {

        alert($scope.County);
        alert($scope.DateOfBirth);

        var regobj = new Object();
        regobj.FirstName = $scope.FirstName;
        regobj.SecondName = $scope.LastName;
        regobj.Email = $scope.Email;
        regobj.County = $scope.County;
        regobj.UserName = $scope.UserName;
        regobj.PhoneNumber = $scope.PhoneNumber;
        regobj.DateOfBirth = $scope.DateOfBirth;
        regobj.Password = $scope.Password;

        alert(JSON.stringify(regobj));
        //Make the REST call
        //Call the signalr-based real time post sender
        $.ajax({
            url: 'http://kudevolvemain.azurewebsites.net/api/v1/users/register',
            async: true,
            type: 'POST',
            data: JSON.stringify(regobj),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                alert(data);
                new $.flavr({
                    content: 'Thank you <br/>You have successfuly registered',
                    iconPath: '~/icons/',
                    icon: 'star.png',
                    buttons: {
                        read: { style: 'info' },
                        later: { style: 'danger' }
                    }
                });
                window.location = "http://kudevolvemain.azurewebsites.net/accounts/login";
               
            },
            error: function (x, y, z) {
                new $.flavr({
                    content: 'Something happened<br/>Please try again',
                    iconPath: '~/icons/',
                    icon: 'star.png',
                    buttons: {
                        read: { style: 'info' },
                        later: { style: 'danger' }
                    }
                });
                alert('Oooops!!' + x + '\n' + y + '\n' + z);
            }
        });
    }

}]);
