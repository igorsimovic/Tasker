(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardsController', boardsController);

    boardsController.$inject = ['boardService'];

    function boardsController(boardService) {
        var vm = this;
        vm.title = 'This is the board page';

        activate();

        function activate() {
            boardService.getAll().then(function (response) {
                vm.boards = response.data;
                console.log(vm.boards);
            }, function (err) {
                console.error(err);
            });
        }
    }
})();
