(function () {
    'use strict';

    angular
        .module('account')
        .controller('registerController', registerController);

    registerController.$inject = ['$stateParams', 'accountService', '$scope', '$location'];

    function registerController($stateParams, accountService, $scope, $location) {

        $scope.user = {
            FullName: "",
            Email: "",
            UserName: "",
            Password: "",
            Bio: "",
            Initials:""
        }

        $scope.register = function () {
            debugger;
            accountService.register($scope.user).then(function (response) {
                $location.path('/login');
            });
        }
    }
})();