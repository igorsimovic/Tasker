(function () {
    'use strict';

    angular
        .module('account')
        .controller('registerController', registerController);

    registerController.$inject = ['$stateParams', 'accountService', '$scope'];

    function registerController($stateParams, accountService, $scope) {

        $scope.user = {
            FullName: "",
            Email: "",
            UserName: "",
            Password: "",
            Bio: ""
        }

        $scope.register = function () {
            accountService.register($scope.user).then(function (response) {
                USER = response.data;
            });
        }
    }
})();