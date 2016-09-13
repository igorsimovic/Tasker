(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardsController', boardsController);

    boardsController.$inject = ['boardService', '$scope', '$document'];

    function boardsController(boardService, $scope, $document) {
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
            var originalStarred = angular.copy(vm.starred); //this is bad. should use drad-end or smth like this.
            var originalBoards = angular.copy(vm.boards);
            var originalStarredStatus = item.starred;
            var originalIndex = -1;
            if (originalStarredStatus) {
                originalIndex = originalStarred.indexOfObj(item.id);
            } else {
                originalIndex = originalBoards.indexOfObj(item.id);
            }
            //console.log('originals', originalStarred, ' ', originalBoards);
            item.starred = !item.starred;
            boardService.updateBoard(item).then(function () {
                if (originalStarredStatus) {
                    vm.starred.splice(originalIndex, 1);
                    vm.boards.push(item);
                } else {
                    vm.boards.splice(originalIndex, 1);
                    vm.starred.push(item);
                }
            }, function (err) {
                item.starred = !item.starred;
                vm.starred = angular.copy(originalStarred);
                vm.originalBoards = angular.copy(originalBoards);
            }).finally(function () {
                originalStarred = null;
                originalBoards = null;
                originalStarredStatus = null;
                originalIndex = -1;
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

Array.prototype.indexOfObj = function (key) {
    return this.map(function (item) {
        return item.id;
    }).indexOf(key);
}

