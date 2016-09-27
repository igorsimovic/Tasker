(function () {
    'use strict';

    angular
        .module('board')
        .controller('cardController', cardController);

    cardController.$inject = ['$scope', '$uibModal', '$uibModalInstance', 'cardsService', 'card', 'userService', 'labels'];

    function cardController($scope, $uibModal, $uibModalInstance, cardsService, card, userService, labels) {
        $scope.card = card;
        $scope.showLabels = false;
        $scope.showChecklist = false;
        
        $scope.labelsToAdd = [];

        $scope.$watch('card.labels', function () {
            $scope.labels = labels.filter(function (l) {
                return !($scope.card.labels.filter(function (label) {
                    return l.id === label.id;
                })[0]);
            });
        });

        $scope.deleteCard = function (id) {
            cardsService.deleteCard(id);
            console.log('delete card');
        };

        $scope.saveComment = function () {
            var user = userService.getAplicationUser();

            if (!$scope.card.comments)
                $scope.card.comments = [];

            if (user.id) {
                cardsService.insertComment($scope.card.id, user.id, $scope.comment).then(function (response) {
                    $scope.card.comments.push(response.data);
                    $scope.comment = null;
                }, function (error) {
                    console.log('Error with comments');
                });
            }
            else {
                console.log('Unauthorized action');
            }
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

        $scope.toggleSelectLabel = function (labelId) {
            var label = $scope.labels.filter(function (l) {
                return l.id === labelId;
            })[0];

            if (label) {
                label.selected = !label.selected;
                if (label.selected) {
                    $scope.labelsToAdd.push(label.id);
                }
                else {
                    var index = $scope.labelsToAdd.indexOf(label.id);
                    $scope.labelsToAdd.splice(index, 1);
                }
            }
        };

        $scope.cancelLabels = function () {
            $scope.labelsToAdd = [];
            $scope.labels.forEach(function (label) {
                label.selected = false;
            });
            $scope.showLabels = false;
        }

        $scope.insertLabels = function () {
            if ($scope.labelsToAdd.length) {
                cardsService.insertLabels($scope.card.id, $scope.labelsToAdd).then(function (response) {
                    angular.copy(response.data, $scope.card);
                    $scope.cancelLabels();
                }, function (error) {
                    console.log('Error adding labels');
                });
            }
        };

        $scope.removeLabel = function (labelId) {
            cardsService.removeLabel($scope.card.id, labelId).then(function (response) {
                angular.copy(response.data, $scope.card);
            }, function (error) {
                console.log('Error removing label');
            });
        };
    }
})();
