﻿'use strict';

// Init the application configuration module for AngularJS application
var ApplicationConfiguration = (function () {
    // Init module configuration options
    var applicationModuleName = 'tasker';
    var applicationModuleVendorDependencies = [
        'ui.router',
        'dndLists',
        'ngSanitize',
        'ui.select',
        'colorpicker.module',
        'ui.bootstrap',
        'xeditable',
        'ngCookies',
        '720kb.datepicker'
    ];

    // Add a new vertical module
    var registerModule = function (moduleName, dependencies) {
        // Create angular module
        angular.module(moduleName, dependencies || []);

        // Add the module to the AngularJS configuration file
        angular.module(applicationModuleName).requires.push(moduleName);
    };

    return {
        applicationModuleName: applicationModuleName,
        applicationModuleVendorDependencies: applicationModuleVendorDependencies,
        registerModule: registerModule
    };
})();