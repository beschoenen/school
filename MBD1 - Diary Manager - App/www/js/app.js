angular.module('diary', ['ionic', 'diary.controllers', 'diary.services', 'angular.img'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {

    if(window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);
    }

    if(window.StatusBar) {
      StatusBar.styleDefault();
    }
  });
})

.config(function($stateProvider, $urlRouterProvider) {

  $stateProvider

  .state('login', {
    url: '/login',
    templateUrl: 'templates/auth/login.html',
    controller: 'LoginController'
  })

  .state('register', {
    url: '/register',
    templateUrl: 'templates/auth/register.html',
    controller: 'RegisterController'
  })

  .state('device', {
    url: '/device',
    templateUrl: 'templates/auth/device.html',
    controller: 'DeviceController'
  })

  .state('entries', {
    url: '/entries',
    templateUrl: 'templates/entries/index.html',
    controller: 'EntriesController'
  })

  .state('entry', {
    url: '/entries/:id',
    templateUrl: 'templates/entries/entry.html',
    controller: 'EntryController'
  })

  .state('image', {
    url: '/entries/:id/images/:imageId',
    templateUrl: 'templates/entries/image.html',
    controller: 'ImageController'
  });

  $urlRouterProvider.otherwise('/login');

});
