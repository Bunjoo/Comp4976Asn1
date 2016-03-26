$(document).ready(function () {
    $("#LoginBtn").click(function () {
        $('#loginForm').removeClass('hide');
        $('#regForm').addClass('hide');
        $('#choiceForm').addClass('hide');
    });
    $("#RegBtn").click(function () {
        $('#regForm').removeClass('hide');
        $('#loginForm').addClass('hide');
        $('#choiceForm').addClass('hide');
    });
    $("#OptionBtn").click(function () {
        $('#choiceForm').removeClass('hide');
        $('#loginForm').addClass('hide');
        $('#regForm').addClass('hide');
    });
});

(function () {
    
    var app = angular.module("myApp", []);

    $scope.register = function () {
        console.log("Registering");
        var regUrl = "http://a2api.amandaxu.xyz/api/Account/Register";
        /*
        var username = ($scope.reg.username);
        var email = ($scope.reg.email);
        var password = ($scope.reg.password);
        var confirmPass = ($scope.reg.confirmPass);
        */
        //check for password
    }


});