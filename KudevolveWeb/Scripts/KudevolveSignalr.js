/// <reference path="jquery-1.10.2.min.js" />
/// <reference path="jquery.signalR-2.1.0.min.js" />


$(function () {

    var kuhub = $.connection.notificationStreamer;

    kuhub.client.addPostComment = function (postid, comment) {
        
    }

    kuhub.client.addNewPost = function (post) {
        $scope.posts.push(post);
        $scope.apply();
        
    }

    kuhub.client.addPetition = function () {

    }

    kuhub.client.notify = function (message) {
        toastr.info("new message: " + message);
        console.log("message");
    }

    $.connection.hub.start().done(function () {

        //Log the connection beginning
        console.log("Signalr is Online");
    });

    ////Code to connect to the signalr server in real time
    //var notify = $.hubConnection();

    //var notifyHubProxy = notify.createHubProxy("notificationStreamer");

    //notifyHubProxy.on("addNewPost", function (post) {

    //    $scope.post.push(post);
    //    $scope.apply();
    //});

    //notifyHubProxy.on("addPostComment", function () {


    //});

    //notifyHubProxy.on("addPetition", function () {

    //});

    //notifyHubProxy.on("notify", function () {

    //});


    ////Start the connection to receive messages
    //notify.start().done(function () {

    //    //Log the beginning o
    //    // toastr.info("Signalr Connected");

    //});


});