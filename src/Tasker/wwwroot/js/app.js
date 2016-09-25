'use strict';

// Start by defining the main module and adding the module dependencies
angular.module(ApplicationConfiguration.applicationModuleName, ApplicationConfiguration.applicationModuleVendorDependencies);

// Setting HTML5 Location Mode
angular.module(ApplicationConfiguration.applicationModuleName).config(['$locationProvider',
  function ($locationProvider) {
      //$locationProvider.html5Mode({
      //    enabled: true,
      //    requireBase: false
      //});
      $locationProvider.hashPrefix('!');
  }
]);

angular.module(ApplicationConfiguration.applicationModuleName).config(['$httpProvider',
  function ($httpProvider) {
      $httpProvider.interceptors.push('AuthInterceptorService');
      //$httpProvider.interceptors.push('HttpErrorResponseInterceptor');
  }
]);

// Intercept state change errors
//angular.module(ApplicationConfiguration.applicationModuleName).run(['$rootScope', 'Notify', '$state',
//  function ($rootScope, Notify, $state) {
//      $rootScope.$state = $state;
//      $rootScope.$on('$stateChangeError', function (e, toState, toParams, fromState, fromParams, error) {
//          if (error === 'NOT_AUTHORIZED') {
//              Notify.error('You are not authorized for this action.');
//          }
//          if (error === 'NOT_AUTHENTICATED') {
//              Notify.error('Please log in to continue.');
//          }
//      });
//  }
//]);

 //Redirect to home view when route not found
angular.module(ApplicationConfiguration.applicationModuleName).config(['$urlRouterProvider',
  function ($urlRouterProvider) {
      $urlRouterProvider.otherwise('/boards');
  }
]);

// Then define the init function for starting up the application
angular.element(document).ready(function () {
    // Init the app
    angular.bootstrap(document.body, [ApplicationConfiguration.applicationModuleName], { strictDi: true });
});

angular.module(ApplicationConfiguration.applicationModuleName).factory('AuthInterceptorService', ['$q', '$location', function ($q, $location) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authDataString = localStorage.getItem('authData');
        var authData = JSON.parse(authDataString);

        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    };

    var _responseError = function (rejection) {

        if (rejection.status === 401) {
            localStorage.removeItem('authData');
            $location.path('/login');
        }
        return $q.reject(rejection);
    };

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);