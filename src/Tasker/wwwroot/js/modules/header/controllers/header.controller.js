(function () {
    'use strict';

    angular
        .module('header')
        .controller('headerController', headerController);

    headerController.$inject = ['$location', 'headerService'];

    function headerController($location, headerService) {
        var vm = this;
        vm.title = 'header';
        vm.sections = {
            user: false,
            add: false,
            info: false,
        };
        vm.user = headerService.getUser();
        vm.showSection = showSection;
        vm.closeSections = closeSections;
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
    }
})();
