angular.module('common').directive('searchSelect', function () {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            dataSource: '=',
            bindModel: '=ngModel',
            displayPropHeader: '=',
            //displayPropSmall: '='
        },
        templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/common/partials/searchSelect.partial.html',
        link: function ($scope, element) {
            console.log($scope.displayPropHeader);

            $scope.toggleChecked = function (item, index) {
                $scope.bindModel = $scope.dataSource.filter(function (item) {
                    return item.Checked;
                });
            }
            $scope.focus = function (control) {
                $scope.showListOptions = true;
            }

            $(document).click(function (e) {
                $scope.$apply(function () {
                    $scope.showListOptions = false;
                });
            });
            $('#search-select').click(function (e) {
                e.stopPropagation();
            });
        }
    }
});

angular.module('common').filter('filterItems', function () {
    return function (items, searchQuery, key) {
        if (!searchQuery) {
            return items;
        }
        var filtered = [];
        items.forEach(function (item) {
            if (item[key].includes(searchQuery)) {
                filtered.push(item);
            }
        });
        return filtered;
    };
});