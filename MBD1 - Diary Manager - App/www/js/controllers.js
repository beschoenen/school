angular.module('diary.controllers', ['ngCordova', 'ngCordova.custom', 'diary.services', 'ionic'])

.controller('LoginController', function($scope, $ionicPlatform, $cordovaDevice, $cordovaSplashscreen, $location, User, Device) {

  $scope.showAuth = function() {

    Device.getFingerprint().then(successAuth, errorAuth);

    function errorAuth(err) {
      alert("We could not log you in!");
      console.log(err);
    }

    function successAuth() {
      User.me().then(function() {
        $location.path('/entries');
      }, function() {
        alert("Device is not linked to any account");
      });

    }
  };

  $scope.goToRegister = function() {
    $location.path('/register');
  };

  $scope.addDevice = function() {
    $location.path('/device');
  };

})

.controller('DeviceController', function($scope, $ionicHistory, User, Device) {

  $scope.code = {};

  $scope.addDevice = function() {
    User.addDevice($scope.code.code, Device.getInfo()).then(function() {
      alert("This device has been added to your account");
      $ionicHistory.backView().go();
    }, function(error) {
      alert(error.data.error);
    });
  }

})

.controller('RegisterController', function($scope, $ionicHistory, User, Device) {

  $scope.user = {};

  $scope.register = function() {
    if(!$scope.user.firstName || !$scope.user.lastName) {
      return alert("Please fill out all fields");
    }

    User.register($scope.user, Device.getInfo()).then(function(response) {
      var body = response.data;

      if(body.success) {
        alert("You were registered successfully!");
        $ionicHistory.backView().go();
      } else {
        alert("There was an error: " + body.error);
      }
    })
  };

})

.controller('EntriesController', function($scope, $http, $location, $ionicHistory, $ionicPopover, $ionicPlatform, User, Entry) {

  var page = 1;

  $scope.entries = [];

  $ionicHistory.clearHistory();

  $scope.canLoadMore = Entry.canLoadMore;

  $scope.loadMore = function() {
    $scope.entries = Entry.list(page);
    $scope.$broadcast('scroll.infiniteScrollComplete');
    page++;
  };

  $scope.doRefresh = function() {
    Entry.clear();
    page = 1;

    $scope.loadMore();
    $scope.$broadcast('scroll.refreshComplete');
  };

  $scope.goToEntry = function(id) {
    $location.path('/entries/' + id)
  };

  $ionicPopover.fromTemplateUrl('menu', { scope: $scope }).then(function(popover) {
    $scope.popover = popover;
  });

  $scope.showAddDeviceDialog = function() {
    $scope.popover.hide();

    var code = Math.random().toString(36);

    User.addCode(code, false).then(function() {
      alert("Please fill out this code on your new device: " + code);
    }, function() {
      alert("Something went wrong!");
    });
  };

  $scope.showWebLoginDialog = function() {
    $scope.popover.hide();

    var code = Math.random().toString(36);

    User.addCode(code, true).then(function() {
      alert("Please fill out in the web portal: " + code);
    }, function() {
      alert("Something went wrong!");
    });
  };

})

.controller('EntryController', function($scope, $http, $location, $ionicHistory, $stateParams, Entry, Device, Image) {

  $scope.entry = { datetimeCreated: new Date() };

  $scope.$on('$ionicView.afterEnter', function() {
    if($stateParams.id !== "new") {
      Entry.show($stateParams.id).then(setEntry, function() {
        alert("Could not open entry!");
        $ionicHistory.backView().go();
      });
    }

    $scope.canSave = false;
    $scope.canDelete = false;
  });

  $scope.$watchCollection('entry', function() {
    $scope.canSave = (!!$scope.entry.title);
    $scope.canDelete = (!!$scope.entry.id);
  });

  $scope.save = function() {
    if(!$scope.entry.id) {
      Entry.create($scope.entry).then(setEntry);
    } else {
      Entry.update($scope.entry);
    }
  };

  function setEntry(response) {
    $scope.entry = response.data.data;

    Image.setEntryId($scope.entry.id);
    $scope.entry.images = Image.listImages();

    $scope.canSave = false;
  }

  $scope.delete = function() {
    if(confirm("Are you sure that you want to delete this entry?")) {
      Entry.delete($scope.entry).then(function() {
        $ionicHistory.backView().go();
      }, function() {
        alert("Something went wrong!");
      });
    }
  };

  $scope.takeImage = function() {
    if(!$scope.entry.id) {
      return alert("Please save your entry first!");
    }

    Device.getCamera().then(Image.addImage);
  };

  $scope.viewImage = function(id) {
    $location.path('/entries/' + $scope.entry.id + '/images/' + id);
  };

  $scope.getThumbURL = Image.getThumbURL;
})

.controller('ImageController', function($scope, $stateParams, $ionicHistory, Entry, Device, Image) {

  Image.setEntryId($stateParams.id);

  $scope.image = Image.getImage($stateParams.imageId);
  $scope.canSave = false;

  $scope.$watch('image.name', function() {
    $scope.canSave = (!!$scope.image.name);
  });

  $scope.delete = function() {
    if(confirm("Are you sure that you want to delete this image?")) {
      Image.removeImage($stateParams.imageId).then(function() {
        $ionicHistory.backView().go();
      }, function() {
        alert("Something went wrong!");
      });
    }
  };

  $scope.save = function() {
    Image.setTitle($scope.image.id, $scope.image.name).then(function() {
      $scope.canSave = false;
    });
  };

  $scope.getImageURL = Image.getImageURL;
});
