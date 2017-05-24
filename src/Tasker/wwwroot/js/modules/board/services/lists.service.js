(function () {
    'use strict';

    angular
        .module('board')
        .service('listsService', listsService);

    listsService.$inject = ['$http'];

    function listsService($http) {
        this.getByBoardId = getByBoardId;
        this.createList = createList;
        this.removeList = removeList;
        this.updateOrder = updateOrder;
        this.updateName = updateName;
        this.inviteToBoard = inviteToBoard;
      
        this.getBoardCollaborators = getBoardCollaborators;

        function getBoardCollaborators(boardId) {
            return $http.get('/api/v1/boards/' + boardId + '/collaborators');
        }

        function inviteToBoard(boardId, userId) {
            return $http.put('/api/v1/boards/' + boardId + '/invite/' + userId);
        }



        function getByBoardId(id) {
            return $http.get('/api/v1/boards/' + id);
        }

        function createList(list) {
            return $http.post('/api/v1/lists', list);
        }

        function removeList(id) {
            return $http.delete('api/v1/lists/' + id);
        }

        function updateOrder(newOrderArr) {
            return $http.put('api/v1/lists/order', newOrderArr);
        }

        function updateName(id, newName) {
            return $http.put('api/v1/lists/' + id + '/name', { name: newName });
        }

    }
})();