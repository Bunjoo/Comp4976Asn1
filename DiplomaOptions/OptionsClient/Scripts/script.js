
    var optionPicker = angular.module("optionPicker", []);

    optionPicker.controller('mainCtrl', function ($scope, $http, $window, $httpParamSerializerJQLike) {

        $scope.login = function () {

            //the header/authentication
            $scope.user = {
                grant_type: "password",
                Username: $("#login_usr").val(),
                Password: $("#login_pwd").val()
            };

            console.log( $("#login_usr").val());
            console.log($("#login_pwd").val());

            $http
                .post('http://a2api.amandaxu.xyz/Token', $httpParamSerializerJQLike('$scope.user'))
                .success(function (data, status, headers, config) {
                    $window.sessionStorage.token = data.token;
                    console.log(data.token);
                    $scope.lresponse = "Successfully logged in."
                })
                .error(function (data, status, headers, config) {
                    delete $window.sessionStorage.token;
                    $scope.lresponse = data;
                    $scope.lresponse = "hi";
                    console.log("no work");
                })
        }
        
        $scope.register = function () {
            $scope.newuser = {
                Username: $("#reg_usr").val(),
                Email: $("#reg_email").val(),
                Password: $("#reg_pwd").val(),
                ConfirmPassword: $("#reg_cpwd").val()
            };

            $http
                .post('http://a2api.amandaxu.xyz/api/account/register', $scope.newuser)
                .success(function (data, status, headers, config) {
                    $scope.rresponse = "You have successfully registered!";
                })
                .error(function (data, status, headers, config) {
                    $scope.rresponse = data;
                });
        };

        $scope.register = function () {
            

        }

    })