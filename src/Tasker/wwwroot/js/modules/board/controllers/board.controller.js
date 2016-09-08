(function () {
    'use strict';

    angular
        .module('board')
        .controller('boardController', boardController);

    boardController.$inject = ['$stateParams', 'boardService'];

    function boardController($stateParams, boardService) {
        var vm = this;
        vm.title = 'board';

        var id = $stateParams.id || null;

        activate();

        function activate() {
            if (id) {
                boardService.getById(id).then(function (response) {
                    vm.board = response.data;
                    console.log(vm.board);
                }, function (err) {
                    console.error(err);
                });
            } else {
                console.error('Board id not provided');
            }
        }
    }
})();
