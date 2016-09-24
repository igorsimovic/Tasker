(function () {
    'use strict';

    angular
        .module('board')
        .service('boardService', board);

    board.$inject = ['$http'];

    function board($http) {
        this.getAll = getAll;
        this.getById = getById;
        this.updateBoard = updateBoard;
        this.createBoard = createBoard;
        this.refreshBoards = refreshBoards;

        function getAll() {
            return $http.get('/api/v1/boards');
        }

        function getById(id) {
            return $http.get('/api/v1/boards/' + id);
        }

        function createBoard(board) {
            return $http.post('/api/v1/boards', board);
        }

        function updateBoard(board) {
            return $http.put('/api/v1/boards/' + board.id, board);
        }

        function refreshBoards(userID) {
            return $http.get('api/v1/boards/userID/' + userID);
        }

    }
})();