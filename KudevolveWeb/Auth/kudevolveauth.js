/// <reference path="hello.all.js" />
/// <reference path="jstorage.js" />
hello.init({
    facebook: '651257824946190',
    windows: '000000004C114187',
    google: '',
    twitter: 'CWfPQ6dvtFMCgrnslLvaxsexH',
    instagram: 'd60739a1ef124a56b8b9584047ebe949',
    linkedin: '776zl6upnp3ejp',
    yahoo: ''
    
}, { redirect_uri: '/Dashboard/Index' });

//Code to be the listener after the user logs into the app. Gets the users profile
hello.on('auth.login', function(auth){
	
    // call user information, for the given network
    hello( auth.network ).api( '/me' ).success(function(r){
        var $target = $("#profile_"+ auth.network );
        if($target.length==0){
            $target = $("<div id='profile_"+auth.network+"'></div>").appendTo("#profile");
        }

        //Set the Object in the storage
        $.jStorage.set("user", r);


        $target.html('<img src="'+ r.thumbnail +'" /> Hey '+r.name).attr('title', r.name + " on "+ auth.network);
    });
});