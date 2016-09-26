(function () {
    'use strict';

    angular
        .module('header')
        .controller('appController', app);

    app.$inject = ['$location', 'accountService'];

    function app($location, accountService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'app';
        vm.appUser = null;
        activate();


        function activate() {
            //accountService.getUser();
            vm.appUser = accountService.user;
        }
    }
})();
