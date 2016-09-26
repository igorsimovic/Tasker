(function () {
    'use strict';

    angular
        .module('board')
        .controller('listsController', listsController);

    listsController.$inject = ['$stateParams', 'boardService', 'listsService', 'cardsService'];

    function listsController($stateParams, boardService, listService, cardsService) {
        var vm = this;

        //
        vm.title = 'Lists';
        vm.changeBoardName = changeBoardName;
        vm.addList = addList;
        vm.listSettings = listSettings;
        vm.reorderLists = reorderLists;
        vm.changeListName = changeListName;
        //
        vm.addCard = addCard;
        vm.reorderCards = reorderCards;
        vm.openCard = openCard;
        vm.insertCard = insertCard;

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

        //BOARD Functions
        function changeBoardName(name) {
            boardService.updateName(boardId, name).then(function (res) {

            }, function (err) {
                console.error('Error changing board name: ', err);
            });
        }

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

        //dnd-moved
        function reorderLists() {
            var newOrder = [];
            vm.board.lists.forEach(function (listItem, index) {
                if (listItem.order !== index) {
                    listItem.order = index;
                    newOrder.push({ listId: listItem.id, listName: listItem.name, newIndex: index });
                }
            });
            if (newOrder.length > 0) {
                listService.updateOrder(newOrder).then(function (res) {
                    console.log('List indexes updated successfully: ', res);
                }, function (err) {
                    console.error('List indexes update error: ', err);
                });
            }
        }

        function changeListName(id, name) {
            listService.updateName(id, name).then(function (res) {

            }, function (err) {
                console.error('Error changing list name: ', err);
            });
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

        //dnd-moved - reorder in same list
        function reorderCards(list) {
            var newOrder = [];
            list.cards.forEach(function (cardItem, index) {
                if (cardItem.order !== index) {
                    cardItem.order = index;
                    newOrder.push({ cardId: cardItem.id, cardName: cardItem.name, newIndex: index });
                }
            });
            console.log('Update cards with this: ', newOrder);
            if (newOrder.length > 0) {
                card
            }
        }

        //dnd-inserted - move from one list to another
        function insertCard(index, event, card, list) {
            if (list.id !== card.listId) {
                console.log('INSERTED: ', arguments);
            }
        }

        function openCard(card) {
            console.log('Open Card: ', card);
        }

    }
})();
