(function () {
    'use strict';

    angular
        .module('board')
        .service('boardService', board);

    board.$inject = ['$http'];

    function board($http) {
        this.getAll = getAll;
        function getAll() {
            return $http.get('/api/v1/boards');
        }
    }
})();