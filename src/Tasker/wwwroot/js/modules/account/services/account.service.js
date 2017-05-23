(function () {
    'use strict';

    angular
        .module('account')
        .service('accountService', account);

    account.$inject = ['$http', '$q', '$location'];

    function account($http, $q, $location) {

        var authData = {
            isAuth: false,
            userName: "",
            userId: ""
        }

        this.register = register;
        this.login = login;
        this.logout = logout;
        this.authData = authData;

        var user = {};
        this.user = user;

        this.getUser = function () {
            var userFromLS = JSON.parse(localStorage.getItem('authData'));
            if (userFromLS) {
                user = userFromLS;
                return user;
            }
            return null;;
        }
        this.setUser = function (newUser) {
            user = newUser;
        }


        function register(user) {
            return $http.post('/api/v1/jwt/register', user);
        }

        function login(user) {

            var data = "grant_type=password&username=" + user.UserName + "&password=" + user.Password;

            var deferred = $q.defer();

            $http.post('/api/v1/jwt/login', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {
                response = response.data;
                localStorage.setItem('authData', JSON.stringify({ token: response.access_token, userId: response.user_id, userName: user.UserName }));

                authData.isAuth = true;
                authData.userId = response.user_id;
                authData.userName = user.UserName;

                deferred.resolve(response);

            }, function (err) {
                deferred.reject(err)
            });

            return deferred.promise;
        }


        function logout() {
            localStorage.removeItem('authData');
            authData.isAuth = false;
            authData.userId = "";
            authData.userName = "";
            $location.path('/login');
        }


    }
})();