(function () {
    'use strict';

    angular
        .module('board')
        .service('cardsService', cardsService);

    cardsService.$inject = ['$http'];

    function cardsService($http) {
        this.getByListId = getByListId;
        this.createCard = createCard;
        this.removeCard = removeCard;
        this.updateCardName = updateCardName;
        this.updateCardDescription = updateCardDescription;
        this.insertComment = insertComment;
        this.insertLabels = insertLabels;
        this.removeLabel = removeLabel;
        this.updateOrder = updateOrder;
        this.moveCard = moveCard;
        this.checkItem = checkItem;
        this.addCheckList = addCheckList;
        this.getCheckLists = getCheckLists;
        this.addCheckItem = addCheckItem;
        this.updateDueDate = updateDueDate;

        function getByListId(id) {
            return $http.get('/api/v1/lists/' + id);
        };

        function createCard(card) {
            return $http.post('/api/v1/cards', card);
        };

        function removeCard(id) {
            return $http.delete('api/v1/cards/' + id);

        };

        function updateCardName(id, name) {
            return $http.put('api/v1/cards/' + id + '/name', { Name: name });
        };

        function updateCardDescription(id, description) {
            return $http.put('api/v1/cards/' + id + '/description', { Description: description });
        };

        function insertComment(id, userId, text) {
            return $http.put('api/v1/cards/' + id + '/comments', { UserId: userId, Text: text });
        };

        function insertLabels(id, labelIds) {
            return $http.put('api/v1/cards/' + id + '/addLabels', labelIds);
        };

        function removeLabel(id, labelId) {
            return $http.put('api/v1/cards/' + id + '/removeLabel', { Id: labelId });
        };

        function updateOrder(newOrderArr) {
            return $http.put('api/v1/cards/order', newOrderArr);
        }

        function moveCard(id, oldListId, newListId) {
            return $http.put('api/v1/cards/' + id + '/move', { destinationId: oldListId, targetId: newListId });
        }

        function checkItem(id, value) {
            return $http.put('api/v1/cards/checkItem/', { ID: id, Checked: value });
        }
        function addCheckList(cardId, checklistName) {
            return $http.post('api/v1/cards/' + cardId + '/addCheckList/' + checklistName);
        }
        function getCheckLists(cardId) {
            return $http.get('api/v1/cards/' + cardId + '/checkLists');
        }
        function addCheckItem(id, content) {
            return $http.post('api/v1/cards/chekList/' + id, { Value: content });
        }
        function updateDueDate(id, date) {
            return $http.put('api/v1/cards/' + id + '/dueDate', { Date: date })
        }
    }
})();