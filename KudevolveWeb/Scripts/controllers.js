

app.controller('LoginCtrl', ['$scope', function ($scope) {
    hello.init({
        facebook: '651257824946190',
        windows: '',
        google: '',
        twitter: 'CWfPQ6dvtFMCgrnslLvaxsexH',
        instagram: 'd60739a1ef124a56b8b9584047ebe949',
        linkedin: '776zl6upnp3ejp',
        yahoo: ''

    }, { redirect_uri: '/Dashboard/Index' });

    //Code to be the listener after the user logs into the app. Gets the users profile
    hello.on('auth.login', function (auth) {

        // call user information, for the given network
        hello(auth.network).api('/me').success(function (r) {
            var $target = $("#profile_" + auth.network);
            if ($target.length == 0) {
                $target = $("<div id='profile_" + auth.network + "'></div>").appendTo("#profile");
            }

            //Set the Object in the storage
            $.jStorage.set("user", r);

            var loginViewModel = "{Email:" + $scope.email + ", Password: " + $scope.password + ", RememberMe: false}";

            //Make a POST request to the Server for authentication
            $.post("api/v1/users/login", loginViewModel, function (data, status, jqXHR) {

                if (status == 200) {
                    //do something
                    alert("Login Successful" + data.firstname);

                    //Redirect to the Dashboard page
                    window.location = "Dashboard/Index";

                }


            });


            $target.html('<img src="' + r.thumbnail + '" /> Hey ' + r.name).attr('title', r.name + " on " + auth.network);
        });
    });


    //Perform authentication here
    var facebook = function () {

        hello('facebook').login(function () {

            alert("Successfully Connected to facebook");

        });

    }
    var twitter = function () {

        hello('twitter').login(function () {

            alert("Successfully Connected to facebook");

        });

    }
    var instagram = function () {

        hello('instagram').login(function () {

            alert("Successfully Connected to facebook");

        });

    }

    var linkedin = function () {

        hello('linkedin').login(function () {

            alert("Successfully Connected to facebook");

        });

    }

    var google = function () {

        hello('google').login(function () {

            alert("Successfully Connected to facebook");

        });

    }


}]);

app.controller('RegisterCtrl', ['$scope', function ($scope) {
    hello.init({
        facebook: '651257824946190',
        windows: '',
        google: '',
        twitter: 'CWfPQ6dvtFMCgrnslLvaxsexH',
        instagram: 'd60739a1ef124a56b8b9584047ebe949',
        linkedin: '776zl6upnp3ejp',
        yahoo: ''

    }, { redirect_uri: '/Dashboard/Index' });

    //Code to be the listener after the user logs into the app. Gets the users profile
    hello.on('auth.login', function (auth) {

        // call user information, for the given network
        hello(auth.network).api('/me').success(function (r) {
            var $target = $("#profile_" + auth.network);
            if ($target.length == 0) {
                $target = $("<div id='profile_" + auth.network + "'></div>").appendTo("#profile");
            }

            //Set the Object in the storage
            $.jStorage.set("user", r);

            $scope.email = r.email;
            $scope.birthday = r.birthday;
            $scope.gender = r.gender;
            $scope.firstname = r.firstname;
            $scope.secondname = r.lastname;


            var loginViewModel = "{Email: '', Password: " + newpost + ", RememberMe: false}";

            //Make a POST request to the Server for authentication
            $.post("api/v1/users/login", loginViewModel, function (data, status, jqXHR) {

                if (status == 200) {
                    //do something
                    $scope.posts.push(data);
                    $scope.apply();

                }


            });



            $target.html('<img src="' + r.thumbnail + '" /> Hey ' + r.name).attr('title', r.name + " on " + auth.network);
        });
    });


    //Perform authentication here
    var facebook = function () {

        hello('facebook').login(function () {

            alert("Successfully Connected to facebook");

        });

    }
    var twitter = function () {

        hello('twitter').login(function () {

            alert("Successfully Connected to facebook");

        });

    }
    var instagram = function () {

        hello('instagram').login(function () {

            alert("Successfully Connected to facebook");

        });

    }

    var linkedin = function () {

        hello('linkedin').login(function () {

            alert("Successfully Connected to facebook");

        });

    }

    var google = function () {

        hello('google').login(function () {

            alert("Successfully Connected to facebook");

        });

    }


}]);