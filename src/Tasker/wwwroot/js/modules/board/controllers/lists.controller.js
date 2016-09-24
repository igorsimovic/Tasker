(function () {
    'use strict';

    angular
        .module('board')
        .controller('listsController', listsController);

    listsController.$inject = ['$stateParams', 'listsService'];

    function listsController($stateParams, listService) {
        var vm = this;
        vm.title = 'Lists';
        vm.addList = addList;
        vm.listSettings = listSettings;
        vm.reorderList = reorderList;
        vm.addCard = addCard;
        vm.openCard = openCard;

        var boardId = $stateParams.id || null;

        (function activate() {
            if (boardId) {
                listService.getByBoardId(boardId).then(function (response) {
                    vm.board = response.data;
                    console.log(vm.board);
                }, function (err) {
                    console.error(err);
                });
            } else {
                console.error('Board id not provided');
            }
        })();

        function addList() {
            var newList = {
                name: "List " + (vm.board.lists.length + 1),
                order: (vm.board.lists.length + 1),
                cards: [],
                boardId: boardId
            };
            listService.createList(newList).then(function (res) {
                vm.board.lists.push(res.data);
            }, function (err) {
                console.error('Error creating list: ', err);
            });
        }

        function listSettings(list) {
            //
            deleteList(list);
        }

        function deleteList(list) {
            listService.removeList(list.id).then(function (res) {
                vm.board.lists = vm.board.lists.filter(function (item) {
                    return item.id !== list.id;
                });
            }, function (err) {
                console.error('Error removing list: ', err);
            });
        }

        function reorderList(event, index, item, external, type) {
            debugger
            console.log('Arguments: ', arguments);
        }

        function addCard(list) {
            var newCard = {
                order: list.cards.length + 1,
                name: 'Card - ' + list.cards.length + 1
            }
            list.cards.push(newCard);
        }

        function openCard(card) {
            console.log('Open Card: ', card);
        }

    }
})();
