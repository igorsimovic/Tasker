(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardsController', boardsController);

    boardsController.$inject = ['boardService', '$scope', 'userService', 'accountService'];

    function boardsController(boardService, $scope, userService, accountService) {
        var vm = this;
        vm.currentUser = accountService.getUser();
        vm.title = 'This is the board page';
        vm.starred = [];
        vm.boards = [];
        vm.selectedBoard = {};
        vm.toggleStarredStatus = toggleStarredStatus;
        vm.starredInserted = starredInserted;
        vm.boardsInserted = boardsInserted;
        vm.starredMoved = starredMoved;
        vm.boardMoved = boardMoved;
        vm.toggleCreationMode = toggleCreationMode;
        vm.createBoard = createBoard;
        vm.newBoard = {};
        activate();
        vm.creationMode = false;


        function toggleCreationMode() {
            vm.creationMode = !vm.creationMode;
        }

        function createBoard() {
            vm.newBoard.userCreatedBy = accountService.getUser().UserID;
            vm.newBoard.orderNo = vm.boards.length;
            boardService.createBoard(vm.newBoard).then(function (response) {
                //vm.boards.push(response.data);
                vm.creationMode = false;
                refreshBoards();
            }, function (err) {
                console.log(err);
            }).finally(function () {
                vm.newBoard = {};
            });
        }

        function starredMoved(index, item) {
            console.log('starred moved', arguments);
        }

        function boardMoved(index, item) {
            console.log('boards moved', arguments);
        }

        function boardsInserted(a, b, c, d) {
            console.log('boards inserted', arguments);
        }

        function starredInserted(event, index, item, external, type) {
            console.log('starred inserted', arguments);
        }

        function toggleStarredStatus(event, index, item, external, type, list) {
            console.log('dnd drop', list);
            var originalStarredStatus = item.starred;
            if (originalStarredStatus) { // for now we are disabling reorder of the boards in both lists;
                if (list === 'starred')
                    return false;
            } else {
                if (list === 'boards')
                    return false;
            }
            var originalStarred = angular.copy(vm.starred); //this is bad. should use drad-end or smth like this.
            var originalBoards = angular.copy(vm.boards);
            var originalIndex = -1;
            if (originalStarredStatus) {
                originalIndex = originalStarred.indexOfObj(item.id);
            } else {
                originalIndex = originalBoards.indexOfObj(item.id);
            }
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
            return item;
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
            getAllCollaborators();
            refreshBoards();//inital board get
        }
        function getAllCollaborators() {
            userService.getUserList().then(function (response) {
                response.data = response.data.filter(function (item) {
                    return item.id !== vm.currentUser.UserID;
                });
                vm.searchableUsers = response.data;
            }, function (err) {
                console.log(err);
            });
        }

        function refreshBoards() {
            boardService.refreshBoards(vm.currentUser.UserID).then(function (response) {
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

