﻿(function () {
    'use strict';

    angular
        .module('board')
        .service('listsService', listsService);

    listsService.$inject = ['$http'];

    function listsService($http) {
        this.getByBoardId = getByBoardId;
        this.createList = createList;
        this.removeList = removeList;

        function getByBoardId(id) {
            return $http.get('/api/v1/boards/' + id);
        }

        function createList(list) {
            return $http.post('/api/v1/lists', list);
        }

        function removeList(id) {
            return $http.delete('api/v1/lists/' + id);

        }

    }
})();