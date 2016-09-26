(function () {
    'use strict';

    angular
        .module('board')
        .controller('cardController', cardController);

    cardController.$inject = ['$scope', '$uibModal', '$uibModalInstance', 'cardsService', 'card', 'userService'];

    function cardController($scope, $uibModal, $uibModalInstance, cardsService, card, userService) {
        $scope.card = card;
        $scope.showLabels = false;
        $scope.showChecklist = false;

        $scope.deleteCard = function (id) {
            cardsService.deleteCard(id);
            console.log('delete card');
        };

        $scope.saveComment = function () {
            var user = userService.getAplicationUser();

            if (!$scope.card.comments)
                $scope.card.comments = [];
            cardsService.insertComment($scope.card.id, user.id, $scope.comment).then(function (response) {
                $scope.card.comments.push($scope.comment);
                $scope.comment = null;
            }, function (error) {
                console.log('Error with comments');
            });
        };

        $scope.toggleShowLabels = function ($event) {
            $scope.showChecklist = false;
            $scope.showLabels = !$scope.showLabels;
            if (!$scope.showLabels) {
                $event.currentTarget.blur();
            }
        };

        $scope.toggleShowChecklist = function ($event) {
            $scope.showLabels = false;
            $scope.showChecklist = !$scope.showChecklist;
            if (!$scope.showChecklist) {
                $event.currentTarget.blur();
            }
        };

        $scope.updateName = function (name) {
            cardsService.updateCardName($scope.card.id, name);
        };

        $scope.updateDescription = function (description) {
            cardsService.updateCardDescription($scope.card.id, description);
        };
    }
})();
