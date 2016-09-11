(function () {
    'use strict';

    angular
        .module('header')
        .controller('headerController', headerController);

    headerController.$inject = ['$location'];

    function headerController($location) {
        var vm = this;
        vm.title = 'header';
        vm.sections = {
            user: false,
            add: false,
            info: false,
        };
        activate();
        vm.showSection = showSection;
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

        function activate() { }
    }
})();
