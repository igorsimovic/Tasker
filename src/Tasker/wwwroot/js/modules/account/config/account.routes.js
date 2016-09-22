'use strict'
angular.module('account').config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state('login', {
            url: '/login',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/account/views/login.view.html',
            controller: 'loginController',
            controllerAs: 'vm'
        }).state('register', {
            url: '/register',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/account/views/register.view.html',
            controller: 'registerController',
            controllerAs: 'vm'
        })
    }]);