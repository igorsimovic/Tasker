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
            return $http.put('api/v1/cards/' + id + '/name', { name: name });
        };

        function updateCardDescription(id, description) {
            return $http.put('api/v1/cards/' + id + '/description', { description: description });
        };

        function insertComment(id, userId, text) {
            return $http.put('api/v1/cards/' + id + '/comments', { UserId: userId, Text: text});
        };
    }
})();