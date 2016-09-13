(function () {
	'use strict';

	angular
        .module('user')
        .service('userService', userService);

	userService.$inject = ['$http'];

	function userService($http) {

		this.save = saveUser;

		function saveUser(user) {
			return $http.put('/api/v1/user/' + user.id, user);
		}
	}
})();