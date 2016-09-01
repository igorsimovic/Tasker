(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardController', boardController);

    boardController.$inject = ['boardService'];

    function boardController(boardService) {
        var vm = this;
        vm.title = 'board';

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
