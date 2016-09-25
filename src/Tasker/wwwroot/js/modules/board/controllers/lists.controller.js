(function () {
    'use strict';

    angular
        .module('board')
        .controller('listsController', listsController);

    listsController.$inject = ['$stateParams', 'listsService'];

    function listsController($stateParams, listService) {
        var vm = this;
        //
        vm.title = 'Lists';
        vm.addList = addList;
        vm.listSettings = listSettings;
        vm.reorderLists = reorderLists;
        //
        vm.addCard = addCard;
        vm.reorderCards = reorderCards;
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


        //LIST Funcitons
        function addList() {
            var newList = {
                name: "List " + (vm.board.lists.length + 1),
                order: (vm.board.lists.length),
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
            //TODO: settings should be here not just delete
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

        //dnd-drop
        function reorderLists() {
            var newOrder = [];
            vm.board.lists.forEach(function (listItem, index) {
                listItem.order = index;
                newOrder.push({ listId: listItem.id, listName: listItem.name, newIndex: index });
            });
            listService.updateOrder(newOrder).then(function (res) {
                console.log('indexes updated: ', res);
            }, function (err) {
                console.error('Indexes not updated: ', err);
            });
            console.log('Update server with those values: ', newOrder);
        }



        //CARD Functions
        function addCard(list) {
            var newCard = {
                order: list.cards.length + 1,
                name: 'Card - ' + (list.cards.length + 1),
                listId: list.id
            }
            list.cards.push(newCard);
        }

        function reorderCards(event, index, item, external, type) {
            console.log('reorder cards: ', arguments);
            return item;
        }

        function openCard(card) {
            console.log('Open Card: ', card);
        }

    }
})();
