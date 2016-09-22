(function () {
    'use strict';

    angular
        .module('account')
        .controller('loginController', loginController);

    loginController.$inject = ['$stateParams', '$location', '$scope', 'accountService'];

    function loginController($stateParams, $location, $scope, accountService) {
        
        $scope.user = {
            UserName : "",
            Password : ""
        }

        $scope.login = function () {
            accountService.login($scope.user).then(function (response) {
                $location.path('/boards');
            });
        }
    }
})();
