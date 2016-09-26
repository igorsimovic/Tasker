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

        function getByListId(id) {
            return $http.get('/api/v1/lists/' + id);
        }

        function createCard(card) {
            return $http.post('/api/v1/cards', card);
        }

        function removeCard(id) {
            return $http.delete('api/v1/cards/' + id);

        }

    }
})();