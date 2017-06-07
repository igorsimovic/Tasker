angular.module('common').directive('checkList', ['cardsService', function (cardsService) {
    return {
        resttrict: 'E',
        scope: {
            checkListId: '=',
            dataSource: '=',
        },
        templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/common/partials/checkListPartial.html',
        link: function ($scope) {
            (function init() {
                $scope.noOfCheckedItems = $scope.dataSource.filter(function (item) {
                    return item.checked;
                }).length;
            })();
            $scope.toggleChecked = function (item, index) {
                if (!item.checked) {
                    cardsService.checkItem(item.id, item.checked).then(function (result) {
                        $scope.noOfCheckedItems--;
                    }, function (err) {
                        console.err('Card check event', err);
                        item.checked = !item.checked;
                    });
                } else {
                    cardsService.checkItem(item.id, item.checked).then(function (result) {
                        $scope.noOfCheckedItems++;
                    }, function (err) {
                        console.err('Card check event', err);
                        item.checked = !item.checked;
                    });
                }
            }
            $scope.startCreationMode = function () {
                $scope.creationMode = true;
            }

            $scope.addCheckItem = function (value) {
                if (!value) {
                    $scope.creationMode = false;
                    return;
                }
                cardsService.addCheckItem($scope.checkListId, value).then(function (result) {
                    $scope.dataSource.push(result.data);
                    $scope.checkItemname = null;
                    $scope.creationMode = false;
                }, function (err) {
                    console.err(err);
                });

            }
        }

    }

}]);