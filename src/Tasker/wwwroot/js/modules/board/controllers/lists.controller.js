(function () {
    'use strict';

    angular
        .module('board')
        .controller('listsController', listsController);

    listsController.$inject = ['$stateParams', 'boardService', 'listsService', 'cardsService', '$uibModal', 'userService'];

    function listsController($stateParams, boardService, listService, cardsService, $uibModal, userService) {
        var vm = this;


        //
        vm.searchableUsers = [];
        vm.collaborators = [];
        vm.title = 'Lists';
        vm.configBoxShown = false;
        vm.userId = userService.getAplicationUser().id;
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
        vm.toggleConfigBox = toggleConfigBox;
        vm.inviteEvent = inviteEvent;
        vm.leaveBoard = leaveBoard;
        var boardId = $stateParams.id || null;
        //

        (function activate() {
            if (boardId) {
                listService.getByBoardId(boardId).then(function (response) {
                    vm.board = response.data;
                    console.log(vm.board);
                }, function (err) {
                    console.error(err);
                });

                userService.getUserList().then(function (response) {
                    response.data = response.data.filter(function (item) {
                        return item.id !== vm.userId;
                    });
                    vm.searchableUsers = response.data;
                }, function (err) {
                    console.log(err);
                });
                listService.getBoardCollaborators(boardId).then(function (response) {
                    vm.collaborators = response.data;
                }, function (err) {
                });

            } else {
                console.error('Board id not provided');
            }
        })();
        //CLOG Functions
        function toggleConfigBox() {
            vm.configBoxShown = !vm.configBoxShown;
        }
        function inviteEvent() {
            if (vm.collaborators.firstOrDefault('id', vm.userToInvite.id)) {
                console.log('invite event distrojd');
                return;
            }
            console.log('invite event started');
            listService.inviteToBoard(boardId, vm.userToInvite.id).then(function () {
                vm.collaborators.push(vm.userToInvite);
            }, function (err) {
                console.log(err);
            }).finally(function () {
                vm.userToInvite = null;
            });
        }

        function leaveBoard() {
            boardService.leaveBoard(vm.userId, boardId).then(function () {
                console.log('board leave successful');
                $state.go('boards');
            }, function (err) {
                console.log('somethin went wrong leave board event', err);
            });
        }

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
            //TODO: more settings should be here not just delete
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
                    newOrder.push({ id: listItem.id, listName: listItem.name, newIndex: index });
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

            cardsService.createCard(newCard).then(function (res) {
                list.cards.push(res.data);
            }, function (err) {
                console.log('Error creating card: ', err);
            });
        }

        //dnd-moved - reorder in same list
        function reorderCards(list) {
            var newOrder = [];
            list.cards.forEach(function (cardItem, index) {
                if (cardItem.order !== index) {
                    cardItem.order = index;
                    newOrder.push({ id: cardItem.id, cardName: cardItem.name, newIndex: index });
                }
            });
            console.log('Update cards with this: ', newOrder);
            if (newOrder.length > 0) {
                cardsService.updateOrder(newOrder).then(function (res) {

                }, function (err) {
                    console.error('Error updating cards: ', err);
                });
            }
        }

        //dnd-inserted - move from one list to another
        function insertCard(index, event, card, list) {
            if (list.id !== card.listId) {
                //cardId, old list id, new list id
                cardsService.moveCard(card.id, card.listId, list.id);
            }
        }

        function openCard(card, labels) {
            console.log('caraarada', card);
            $uibModal.open({
                animation: true,
                templateUrl: 'js/modules/board/views/card.modal.html',
                controller: 'cardController',
                size: 'md',
                resolve: {
                    card: function () { return card; },
                    labels: function () { return labels }
                }
            });
        }

    }
})();
