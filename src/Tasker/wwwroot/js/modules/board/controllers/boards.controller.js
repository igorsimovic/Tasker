(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardsController', boardsController);

    boardsController.$inject = ['boardService', '$scope'];

    function boardsController(boardService, $scope) {
        var vm = this;
        vm.title = 'This is the board page';
        vm.starred = [];
        vm.boards = [];
        vm.selectedBoard = {};
        //$scope.$watch(function () {
        //    return vm.models.selected;
        //}, function (current, original) {
        //    console.log('old ', original);
        //    console.log('new ', current);
        //});
        activate();

        function activate() {
            boardService.getAll().then(function (response) {
                //vm.boards = response.data;
                vm.starred = response.data.filter(function (board) {
                    return board.starred === true;
                });
                vm.boards = response.data.filter(function (board) {
                    return board.starred === false;
                });
            }, function (err) {
                console.error(err);
            });
        }
    }
})();
Array.prototype.filterAndRemove = function (propName, propVal) {
    var resultArr = [];
    this.forEach(function (item, index) {
        if (item[propName] === propVal) {
            resultArr = item;
            this.splice(index, 0);
        };
    });
    return resultArr;
}
