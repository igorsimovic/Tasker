(function () {
    'use strict';

    angular
        .module('header')
        .controller('headerController', headerController);

    headerController.$inject = ['$scope', 'headerService', 'accountService', 'userService'];

    function headerController($scope, headerService, accountService, userService) {

        var vm = this;
        vm.title = 'header';
        vm.sections = {
            user: false,
            add: false,
            info: false,
        };

        $scope.$on('loginCtrl:login', function (e, user) {
                vm.user = user;
        });

        if (vm.user) {
            userService.getUser(vm.user.userId).then(function (response) {
                vm.user = response.data;
            });
        }

        vm.showSection = showSection;
        vm.closeSections = closeSections;
        vm.logOut = logOut;

        activate();
        function showSection(name) {
            for (var prop in vm.sections) {
                if (vm.sections.hasOwnProperty(prop)) {
                    if (prop === name) {
                        vm.sections[prop] = true;
                    } else {
                        vm.sections[prop] = false;
                    }
                }
            }
        }
        function closeSections() {
            for (var prop in vm.sections) {
                if (vm.sections.hasOwnProperty(prop)) {
                    vm.sections[prop] = false;
                }
            }
        }


        function activate() { }

        function logOut() {
            accountService.logout();
        }
    }
})();
