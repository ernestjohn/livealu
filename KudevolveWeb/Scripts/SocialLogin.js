/// <reference path="oauth.js" />
OAuth.initialize('Vyvllf3OhAQrHCFhXfIiYG_iz20');
var provider = 'facebook';

OAuth.popup(provider)
.done(function (result) {
    result.me()
    .done(function (response) {
        alert(response);
        console.log('Firstname: ', response.firstname);
        console.log('Lastname: ', response.lastname);
    })
    .fail(function (err) {
        //handle error with err
    });
})
.fail(function (err) {
    //handle error with err
});