angular.module('common').directive('datePickerWrapper', [function () {
    return {
        restrict: 'E',
        transclude: true,
        scope: {
            bindModel: '=ngModel',
        },
        templateUrl: GLOBAL_SETTINGS.app.componentsRoute + '/common/partials/dateTimeWrapper.html',
        link: function ($scope) {
            $scope.date = $scope.bindModel;
            $scope.clearDueDate = function () {
                $scope.date = null;
                $scope.$emit('datePicker:saveDate');
            }
            $scope.$watch('date', function () {
                $scope.bindModel = $scope.date;
            });

            $scope.saveDueDate = function () {
                $scope.$emit('datePicker:saveDate');
            }

        },
    }
}]);