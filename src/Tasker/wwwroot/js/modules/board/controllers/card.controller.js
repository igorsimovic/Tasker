(function () {
    'use strict';

    angular
        .module('board')
        .controller('cardController', cardController);

    cardController.$inject = ['$scope', '$uibModalInstance', 'cardsService', 'card'];

    function cardController($scope, $uibModalInstance, cardsService, card) {
        $scope.card = card;

        $scope.deleteCard = function (id) {

        }
    }
})();
