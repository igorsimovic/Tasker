(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardsController', boardsController);

    boardsController.$inject = ['boardService', '$scope', 'userService', 'accountService'];

    function boardsController(boardService, $scope, userService, accountService) {
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
                refreshBoards();
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
            item.orderNo = index;
            item.originalIndex = originalIndex;
            boardService.updateBoard(item).then(function () {
                refreshBoards();
            }, function (err) {
                rollBack(originalStarred, originalBoards);
            }).finally(function () {
                originalStarred = null;
                originalBoards = null;
                originalStarredStatus = null;
                originalIndex = -1;
            });
        }

        function rollBack(originalStarred, originalBoards) {
            originalStarred.forEach(function (item, index) {
                for (var prop in item) {
                    if (item.hasOwnProperty(prop)) {
                        vm.starred[i][prop] = item[prop];
                    }
                }
            });
            originalBoards.forEach(function (item, index) {
                for (var prop in item) {
                    if (item.hasOwnProperty(prop)) {
                        vm.boards[i][prop] = item[prop];
                    }
                }
            });
        }

        function activate() {
            boardService.refreshBoards(accountService.authData.userId).then(function (response) {
                vm.starred = response.data.filter(function (item) {
                    return item.starred;
                });
                vm.boards = response.data.filter(function (item) {
                    return !item.starred;
                });
            });
        }

        function refreshBoards() {
            boardService.refreshBoards(accountService.authData.userId).then(function (response) {
                vm.starred = response.data.filter(function (item) {
                    return item.starred;
                });
                vm.boards = response.data.filter(function (item) {
                    return !item.starred;
                });
            });
        }
    }
})();

Array.prototype.indexOfObj = function (key) {
    return this.map(function (item) {
        return item.id;
    }).indexOf(key);
}

