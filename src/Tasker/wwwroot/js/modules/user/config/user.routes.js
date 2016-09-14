'use strict'
angular.module('user').config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state('user', {
            abstract:true,
            url: '/me',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/user/views/user.view.html',
            controller: 'userController',
            controllerAs: 'vm',
        }).state('user.cards', {
            url: '/cards',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/user/views/user.cards.html',
            controller: 'user.userCardController',
            controllerAs: 'vm',
            parent:'user'
        }).state('user.settings', {
            url: '/settings',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/user/views/user.settings.html',
            controller: 'user.userSettingsController',
            controllerAs: 'vm',
            parent:'user'
        }).state('user.profile', {
            url: '/profile',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/user/views/user.profile.html',
            controller: 'user.userProfileController',
            controllerAs: 'vm',
            parent: 'user'
        });
    }]);