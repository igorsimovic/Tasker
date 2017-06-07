(function () {
    'use strict';

    angular
        .module('account')
        .controller('loginController', loginController);

    loginController.$inject = ['$stateParams', '$location', '$scope', '$rootScope', 'accountService'];

    function loginController($stateParams, $location, $scope, $rootScope, accountService) {
        //$scope.test = [];
        $scope.dataSource = [
        { Value: 'mstajic10', email: 'mrdjan@asdasd.com', Checked: true, },
        { Value: 'mstajic11', email: 'mrdjan@asdasd.com', Checked: true },
        { Value: 'mstajic12', email: 'mrdjan@asdasd.com', Checked: true },
        { Value: 'mstajic13', email: 'mrdjan@asdasd.com' },
        { Value: 'jura', email: 'mrdjan@asdasd.com' },
        { Value: 'stefan', email: 'mrdjan@asdasd.com' },
        { Value: 'nikola', email: 'mrdjan@asdasd.com' },
        { Value: 'djole', email: 'mrdjan@asdasd.com' },
        ];

        //$scope.$watch('test', function () {
        //    console.log('login:ctrl', $scope.test);
        //});

        $scope.user = {
            UserName: "",
            Password: ""
        }
        $scope.message = '';

        (function activate() {
            if (accountService.getUser()) {
                $location.path('/boards');
            }
            $rootScope.$broadcast('loginCtrl:notAuth');
        })();

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
