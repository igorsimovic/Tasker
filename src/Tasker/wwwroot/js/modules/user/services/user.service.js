(function () {
    'use strict';

    angular
        .module('user')
        .service('userService', userService);

    userService.$inject = ['$http'];

    function userService($http) {

        this.save = saveUser;
        this.getUser = getUser;
        this.setApplicationUser = setApplicationUser;
        this.changePassword = changePassword;
        var user = {};

        function changePassword(passwordData) {
            return $http.put('/api/v1/user/' + passwordData.userId + '/changePassword', passwordData);
        }

        this.getAplicationUser = function () {
            return user;
        }
        function setApplicationUser(input) {
            user = input;
        }
        function getUser(id) {
            return $http.get('/api/v1/User/' + id);
        }

        function saveUser(user) {
            return $http.put('/api/v1/user/' + user.id, user);
        }
    }
})();