(function () {
    'use strict';

    angular
        .module('account')
        .controller('loginController', loginController);

    loginController.$inject = ['$stateParams', '$location', '$scope', '$rootScope', 'accountService'];

    function loginController($stateParams, $location, $scope, $rootScope, accountService) {
        
        $scope.user = {
            UserName : "",
            Password : ""
        }

        $scope.message = '';

        $scope.login = function () {
            accountService.login($scope.user).then(function (response) {
                accountService.setUser(response);
                $rootScope.$broadcast('loginCtrl:login', response);

                $location.path('/boards');
            }, function (err) {
                $scope.message = err;
            });
        }
    }
})();
