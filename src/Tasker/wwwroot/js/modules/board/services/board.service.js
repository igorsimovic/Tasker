(function () {
    'use strict';

    angular
        .module('board')
        .service('boardService', board);

    board.$inject = ['$http'];

    function board($http) {
        this.getAll = getAll;
        this.getById = getById;

        function getAll() {
            return $http.get('/api/v1/boards');
        }

        function getById(id) {
            return $http.get('/api/v1/boards/' + id);
        }

    }
})();