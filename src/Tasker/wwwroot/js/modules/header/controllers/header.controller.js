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
            console.log('this is the user', user);
            vm.user = user;
            vm.user.userId = user.user_id;
            userService.getUser(vm.user.user_id).then(function (response) {
                //vm.user = response.data;
                //console.log('user response', vm.user);
                vm.user.bio = response.data.bio;
                vm.user.fullName = response.data.fullName;
                vm.user.id = response.data.id;
                vm.user.initials = response.data.initials;
                vm.user.userName = response.data.userName;
                userService.setApplicationUser(vm.user);
            });
        });
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


        function activate() {
            if (!vm.user) {
                vm.user = accountService.getUser();
                if (vm.user) {
                    userService.getUser(vm.user.userId).then(function (response) {
                        //vm.user = response.data;
                        //console.log('user response', vm.user);
                        vm.user.bio = response.data.bio;
                        vm.user.fullName = response.data.fullName;
                        vm.user.id = response.data.id;
                        vm.user.initials = response.data.initials;
                        vm.user.userName = response.data.userName;
                        userService.setApplicationUser(vm.user);
                    });
                }
            }
        }

        function logOut() {
            accountService.logout();
            vm.user = null;
        }
    }
})();
