(function () {
    'use strict';

    angular
        .module('header')
        .service('headerService', headerService);

    headerService.$inject = ['$http'];

    function headerService($http) {
        var _user = USER;

        this.getUser = function () {
            return _user;
        }
    }
})();