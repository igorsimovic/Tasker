(function () {
    'use strict';

    angular
        .module('board')
        .controller('cardController', cardController);

    cardController.$inject = ['$scope', '$uibModal', '$uibModalInstance', 'cardsService', 'card'];

    function cardController($scope, $uibModal, $uibModalInstance, cardsService, card) {
        $scope.card = card;

        $scope.deleteCard = function (id) {
            cardsService.deleteCard(id);
            console.log('delete card');
        };

        $scope.saveComment = function () {
            $scope.card.comments.push($scope.comment);
            console.log('save comment');
        };

        $scope.openLabelModal = function () {
            $uibModal.open({
                animation: true,
                templateUrl: 'js/modules/board/views/card.modal.html',
                controller: 'cardController',
                size: 'sm',
                resolve: {
                    card: function () { return $scope.card; }
                }
            });
        };
    }
})();
