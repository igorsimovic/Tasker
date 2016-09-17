(function () {
	'use strict';

	angular
        .module('user')
        .service('userService', userService);

	userService.$inject = ['$http' ,'headerService'];

	function userService($http, headerService) {

		this.save = saveUser;
		this.getUser = getUser;
		function getUser(forceReload) {
		    return headerService.getUser(forceReload);
		}
		function saveUser(user) {
			return $http.put('/api/v1/user/' + user.id, user);
		}
	}
})();