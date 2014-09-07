/// <reference path="E:\MY CODE\Kudevolve\KudevolveWeb\Auth/hello.all.js" />
/// <reference path="E:\MY CODE\Kudevolve\KudevolveWeb\Auth/kudevolveauth.js" />
/// <reference path="E:\MY CODE\Kudevolve\KudevolveWeb\Auth/jstorage.js" />

angular.module('app.controllers', [])

    // Path: /
    .controller('DashboardController', ['$scope', '$location', '$http', '$window', function ($scope, $location, $window)
    {
        //Code to get the user information
        var user = $.jStorage.get("user");

        $scope.user = user;

        var user = 
        $scope.posts = "";
        //First Hit a APi request to get data into the scope
        $.get("api/posts", function (data) {
            //Update the Scope object data
            $scope.posts = data;
        });

        $.get("api/discussions", function (data) {
            //Update the Scope object data
            $scope.discussions = data;
        });

        $.get("api/petitions", function (data) {
            //Update the Scope object data
            $scope.petitions = data;
        });

        $.get("api/suggestions", function (data) {
            //Update the Scope object data
            $scope.suggestions = data;
        });

        $.get("api/articles", function (data) {
            //Update the Scope object data
            $scope.articles = data;
        });

        //The Refresh Function
        $scope.refreshPosts = function () {

            $.get("api/posts", function (data) {
                //Update the Scope object data
                $scope.posts = data;
            });
        }
        $scope.$root.title = 'AngularJS for Visual Studio';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });

        $scope.addPost() = function () {
            var postItem = "";
            postItem.content = $scope.newPost;
            $scope.user = 
            $scope.posts.push(post);
        }

    }])
.controller('AuthController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        
    //Functions to login using Auth Controllers
    var facebook = function () {

        hello('facebook').login();

    }

    var twitter = function () {
        hello('twitter').login();
    }
    var instagram = function () {
        hello('instagram').login();
    }
    var linkedin = function () {
        hello('linkedin').login();
    }
    var google = function () {
        hello('google').login();
    }
  
}])



    //// Path: /about
    //.controller('AboutCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    //    $scope.$root.title = 'AngularJS SPA | About';
    //    $scope.$on('$viewContentLoaded', function () {
    //        $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
    //    });
    //}])

    //// Path: /login
    //.controller('LoginCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    //    $scope.$root.title = 'AngularJS SPA | Sign In';
    //    // TODO: Authorize a user
    //    $scope.login = function () {
    //        $location.path('/');
    //        return false;
    //    };
    //    $scope.$on('$viewContentLoaded', function () {
    //        $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
    //    });
    //}])

    //// Path: /error/404
    //.controller('Error404Ctrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    //    $scope.$root.title = 'Error 404: Page Not Found';
    //    $scope.$on('$viewContentLoaded', function () {
    //        $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
    //    });
    //}]);