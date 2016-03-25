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