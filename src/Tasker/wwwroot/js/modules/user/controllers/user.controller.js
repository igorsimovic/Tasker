(function () {
    'use strict';

    angular
        .module('user')
        .controller('userController', userController);

    userController.$inject = ['$stateParams', '$state', '$scope' ,'userService'];

    function userController($stateParams, $state, $scope , userService) {
        var vm = this;
        vm.title = 'board';
        vm.changeTab = changeTab;
        vm.tabs = [
               { name: 'profile', active: true },
               { name: 'cards', active: false },
               { name: 'settings', active: false },
        ];
        $scope.sharedData = {};
        $scope.sharedData.selectTab = changeTab;
        $scope.sharedData.bio = userService.getUser();
        function changeTab(tabName) {
            vm.tabs.forEach(function (tab) {
                if (tab.name !== tabName) {
                    tab.active = false;
                } else {
                    tab.active = true;
                }
            });
            $state.go('user.' + tabName);//why?
        };
        activate();

        function activate() {
            console.log('aassaaaa');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('user')
        .controller('user.userProfileController', userProfileController);

    userProfileController.$inject = ['$scope'];

    function userProfileController($scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'userCard';
        activate();

        function activate() {
            $scope.sharedData.selectTab('profile');
            console.log('profile tab active');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('user')
        .controller('user.userCardController', userCardController);

    userCardController.$inject = ['$scope'];

    function userCardController($scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'userCard';

        activate();

        function activate() {
            $scope.sharedData.selectTab('cards');
            console.log('cards tab active');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('user')
        .controller('user.userSettingsController', userSettingsController);

    userSettingsController.$inject = ['$scope', 'userService'];

    function userSettingsController($scope, userService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'userCard';
        activate();
        vm.showSection = showSection;
        vm.closeSections = closeSections;
        vm.saveUser = saveUser;
        vm.changePassword = changePassword;
        vm.resetForm = resetForm;
        vm.sections = {
            bio: false,
            password: false,
            avatar: false,
        };
        vm.bio = $scope.sharedData.bio;

        function resetForm() {
            vm.passwordForm.$invalid = false;
            vm.passwordForm.$submitted = false;
            vm.passwordForm.repeatPassword.$setValidity('required', false);
            vm.passwordForm.newPassword.$setValidity('required', false);
        }

        function changePassword() {
            if (vm.bio.newPassword !== vm.bio.repeatPassword) {
                vm.passwordForm.repeatPassword.$setValidity('required', true);
                vm.passwordForm.newPassword.$setValidity('required', true);
                return;
            } else {
                userService.changePassword(vm.bio).then(function (data) {
                    console.log('jej');
                }, function (err) {
                    console.log('noo', err);
                });
            }
        }

        function saveUser() {
            userService.save(vm.bio).then(function (data) {

            }, function (err) {
                console.log(err);
            });
        }

        function showSection(name) {
            for (var prop in vm.sections) {
                if (vm.sections.hasOwnProperty(prop)) {
                    if (prop === name) {
                        vm.sections[prop] = true;
                    } else {
                        vm.sections[prop] = false;
                    }
                }
            }
        }
        function closeSections() {
            for (var prop in vm.sections) {
                if (vm.sections.hasOwnProperty(prop)) {
                    vm.sections[prop] = false;
                }
            }
        };
        function activate() {
            $scope.sharedData.selectTab('settings');
            console.log('settings tab active');
        }
    }
})();



