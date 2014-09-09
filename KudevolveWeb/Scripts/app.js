/// <reference path="oauth.js" />
/// <reference path="E:\MY CODE\Kudevolve\KudevolveWeb\Auth/jstorage.js" />
/// <reference path="linq.js" />




// Declares how the application should be bootstrapped. See: http://docs.angularjs.org/guide/module
var app = angular.module('app', []);

// Path: /
app.controller('DashboardCtrl', ['$scope', function ($scope) {
    $scope.posts = "";

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

    kuhub.client.addPostComment = function (postid, comment) {
        var index = Enumerable.From($scope.posts).IndexOf( Enumerable.From($scope.posts).Where(function (pst) {
            pst.PostId == postid
        }).FirstOrDefault());

        //Add the comment to the actual post
        $scope.posts[index].comments.push(comment);
        $scope.apply();
    }

    kuhub.client.addNewPost = function (post) {
        alert(post);
        $scope.posts.push(post);
        $scope.apply();
    }

    kuhub.client.addPetition = function () {
        
    }

    kuhub.client.notify = function (message) {
        toastr.info("new message: " + message);
        $scope.newpost = message;
        alert($scope.newpost);
        $scope.apply();
        console.log(message);
    }

    $.connection.hub.start().done(function () {
        //Log the connection beginning
        console.log("Signalr is Online");
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

        toastr.info("Feature not yet available in Kudevolve")

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
            url: 'http://localhost:4775/api/v1/posts',
            type: 'POST',
            data: JSON.stringify(postToPost),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $scope.posts.push(data);
                $scope.apply();
                toastr.info("You have successfully made a post")
            },
            error: function (x, y, z) {
                alert('Oooops!!' + x + '\n' + y + '\n' + z);
            }
        });


    }

}]);


app.controller('LoginCtrl', ['$scope', function ($scope) {

    //Code to be the listener after the user logs into the app. Gets the users profile
    OAuth.initialize('Vyvllf3OhAQrHCFhXfIiYG_iz20');

    $scope.facebook = function () {

        var provider = 'facebook';
        var options = "";

        OAuth.popup('facebook', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            alert(error);
            alert(result);
            result.get("https://graph.facebook.com/v2.0/me")
            .done(function (user_info) {
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response

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
            alert(error);
            alert(result);
            result.get("https://api.twitter.com/1/account/verify_credentials.json")
            .done(function (user_info) {
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
            alert(error);
            alert(result);
            result.get("https://api.instagram.com/v1/users/self")
            .done(function (user_info) {
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
            alert(error);
            alert(result);
            result.get("http://api.linkedin.com/v1/people/~")
            .done(function (user_info) {
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

        OAuth.popup('google', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            alert(error);
            alert(result);
            result.get("https://www.googleapis.com/plus/v1/people/me")
            .done(function (user_info) {
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

    OAuth.initialize('Vyvllf3OhAQrHCFhXfIiYG_iz20');

    //Perform authentication here
    $scope.facebook = function () {

        var provider = 'facebook';
        var options = "";

        OAuth.popup('facebook', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            alert(error);
            alert(result);
            result.get("https://graph.facebook.com/v2.0/me")
            .done(function (user_info) {
                // user_info contains the user information (e.g. user_info.email, or user_info.avatar)
                // user_info.raw contains the original response

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
            alert(error);
            alert(result);
            result.get("https://api.twitter.com/1/account/verify_credentials.json")
            .done(function (user_info) {
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
            alert(error);
            alert(result);
            result.get("https://api.instagram.com/v1/users/self")
            .done(function (user_info) {
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
            alert(error);
            alert(result);
            result.get("http://api.linkedin.com/v1/people/~")
            .done(function (user_info) {
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

        OAuth.popup('google', function (error, result) {
            //handle error with error
            //use result.access_token in your API request
            alert(error);
            alert(result);
            result.get("https://www.googleapis.com/plus/v1/people/me")
            .done(function (user_info) {
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

        $.post("http://kudevolvemain.azurewebsites.net/api/v1/users/register", postdata, function (data, status, jqXHR) {

            if (status == 200) {
                //do something
                $scope.posts.push(data);
                $scope.apply();

            }


        });
    }

}]);
