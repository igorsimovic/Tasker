(function () {
    'use strict';

    angular
        .module('header')
        .controller('headerController', headerController);

    headerController.$inject = ['$location', 'headerService', 'accountService'];

    function headerController($location, headerService, accountService) {
        var vm = this;
        vm.title = 'header';
        vm.sections = {
            user: false,
            add: false,
            info: false,
        };

        //accountService.getUser().then(function (response) {
        //    vm.user = response.data;
        //});

        vm.authData = accountService.authData;

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
