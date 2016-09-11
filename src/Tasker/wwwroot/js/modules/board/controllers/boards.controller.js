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
        vm.toggleStarredStatus = toggleStarredStatus;
        vm.toggleCreationMode = toggleCreationMode;
        vm.createBoard = createBoard;
        vm.newBoard = {};
        activate();
        vm.creationMode = false;
        function toggleCreationMode() {
            vm.creationMode = !vm.creationMode;
        }

        function createBoard() {
            boardService.createBoard(vm.newBoard).then(function (response) {
                vm.boards.push(response.data);
                vm.creationMode = false;
            }, function (err) {
                console.log(err);
            }).finally(function () {
                vm.newBoard = {};
            });
        }

        function toggleStarredStatus(event, index, item, external, type) {
            var originalStarred = angular.copy(vm.starred);
            var originalBoards = angular.copy(vm.boards);
            item.starred = !item.starred;
            boardService.updateBoard(item).then(function () {
            }, function (err) {
                item.starred = !item.starred;
                vm.starred = angular.copy(originalStarred);
                vm.originalBoards = angular.copy(originalBoards);
            }).finally(function () {
                var originalStarred = null;
                var originalBoards = null;
            });
        }

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

