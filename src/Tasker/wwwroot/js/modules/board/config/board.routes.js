'use strict'
angular.module('board').config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        //$urlRouterProvider.otherwise('/boards');
        $stateProvider.state('boards', {
            url: '/boards',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/board/views/boards.view.html',
            controller: 'boardsController',
            controllerAs: 'vm'
        }).state('board', {
            url: '/boards/:id',
            templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/board/views/board.view.html',
            controller: 'boardController',
            controllerAs: 'vm'
        })
    }]);