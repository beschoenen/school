angular.module('diary.services', ['ngCordova.custom'])

.constant('host', 'http://krichter.nl:3000/api/')

.factory('User', function(host, $http, Device) {

  $http.defaults.headers.post['Content-Type'] = 'application/json';

  function getUser() {
    return $http.get(host + 'users/me', {
      headers: { 'X-Device-Token': Device.getInfo().code }
    });
  }

  function register(user, device) {
    return $http.post(host + 'users', {
      user: user,
      device: device
    });
  }

  function linkDevice(code, device) {
    return $http.post(host + 'devices', {
      code: code,
      device: device
    });
  }

  function addCode(code, web) {
    return $http.post(host + 'codes', {
      isWeb: web || false,
      code: code,
      validUntil: new Date().setTime(new Date().getTime() + 1000 * 60)
    }, {
      headers: { 'X-Device-Token': Device.getInfo().code }
    });
  }

  return {
    me: getUser,
    register: register,
    addDevice: linkDevice,
    addCode: addCode
  }

})

.factory('Entry', function(host, $http, $q, Device) {
  var loadedPage = 0;
  var moreDataCanBeLoaded = true;

  $http.defaults.headers.common['X-Device-Token'] = Device.getInfo().code;

  var entries = [];

  function getEntries(page) {
    page = page || 0;

    if(page < loadedPage) {
      return entries;
    }

    entries.$promise = $http.get(host + 'entries?page=' + page).then(function(response) {
      if(response.data.data.length < 20) {
        moreDataCanBeLoaded = false;
      }

      if(response.data.data.length > 0) {
        Array.prototype.push.apply(entries, response.data.data);
      }

      return entries;
    });

    return entries;
  }

  function getEntry(id) {
    return $q(function(resolve, reject) {
      $http.get(host + 'entries/' + id).then(function(response) {
        resolve(response);
      }, reject);
    });
  }

  function createEntry(entry) {
    return $q(function(resolve, reject) {
      $http.post(host + 'entries', entry).then(function(response) {
        entries.push(response.data.data);
        resolve(response);
      }, reject);
    });
  }

  function updateEntry(entry) {
    function success() {
      var index = entries.findIndex(function(item) {
        return item.id === entry.id;
      });

      entries[index] = entry;
    }

    return $q(function(resolve, reject) {
      $http.put(host + 'entries/' + entry.id, entry).then(function(response) {
        success();
        resolve(response);
      }, reject);
    });
  }

  function deleteEntry(entry) {
    function success() {
      var index = entries.findIndex(function(item) {
        return item.id === entry.id;
      });

      entries.splice(index, 1);
    }

    return $q(function(resolve, reject) {
      $http.delete(host + 'entries/' + entry.id).then(function(response) {
        success();
        resolve(response);
      }, reject);
    });
  }

  function canLoadMore() {
    return moreDataCanBeLoaded;
  }

  function clear() {
    entries = [];
    loadedPage = 0;
  }

  return {
    list: getEntries,
    show: getEntry,
    create: createEntry,
    update: updateEntry,
    delete: deleteEntry,
    canLoadMore: canLoadMore,
    clear: clear
  }

})

.factory('Image', function(host, $http, $q, $cordovaFileTransfer, Device) {

  $http.defaults.headers.common['X-Device-Token'] = Device.getInfo().code;

  var entryId;

  var images = {};

  function setEntryId(id) {
    entryId = id;
  }

  function getImage(index) {
    return images[entryId][index];
  }

  function listImages(refresh) {
    if(!images.hasOwnProperty(entryId)) {
      images[entryId] = [];
    }

    if(images[entryId].length < 1 || refresh) {
      images[entryId].$promise = $http.get(host + 'entries/' + entryId + '/images').then(function(response) {
        Array.prototype.push.apply(images[entryId], response.data.data);
        return images[entryId];
      });
    }

    return images[entryId];
  }

  function addImage(path) {
    if(!entryId) {
      throw "Invalid entry id";
    }

    if(!images.hasOwnProperty(entryId)) {
      images[entryId] = [];
    }

    return $q(function(resolve, reject) {
      $cordovaFileTransfer.upload(host + 'entries/' + entryId + '/images', path, {
        fileKey: 'image',
        headers: {
          'X-Device-Token': Device.getInfo().code
        }
      }).then(function(response) {
        var image = JSON.parse(response.response).data;
        images[entryId].push(image);

        resolve(image);
      }, function(error) {
        reject(JSON.parse(error.response));
      })
    });
  }

  function setTitle(id, title) {
    return $http.put(host + 'images/' + id, { name: title });
  }

  function removeImage(index) {
    if(!entryId) {
      throw "Invalid entry id";
    }

    return $q(function(resolve, reject) {
      $http.delete(host + 'images/' + getImage(index).id).then(function(response) {
        images[entryId].splice(index, 1);
        resolve(response);
      }, reject);
    });
  }

  function getImageURL(id) {
    return host + 'images/' + id;
  }

  function getThumbURL(id) {
    return getImageURL(id) + '/thumb'
  }

  return {
    setEntryId: setEntryId,
    getImage: getImage,
    listImages: listImages,
    addImage: addImage,
    setTitle: setTitle,
    removeImage: removeImage,
    getImageURL: getImageURL,
    getThumbURL: getThumbURL
  }

})

.factory('Device', function($q, $cordovaDevice, $cordovaFingerprint, $cordovaCamera) {

  function getInfo() {
    if(ionic.Platform.is('browser')) {
      return {
        code: "123",
        name: "Testing browser"
      }
    }

    return {
      code: $cordovaDevice.getUUID(),
      name: [
        $cordovaDevice.getManufacturer(),
        $cordovaDevice.getModel(),
        $cordovaDevice.getPlatform(),
        $cordovaDevice.getVersion()
      ].join(' ')
    }
  }

  function getFingerprint() {
    if(ionic.Platform.is('browser')) {
      return $q(function(resolve) {
        resolve()
      });
    }

    return $cordovaFingerprint.show({
      clientId: "diary-manager-app",
      clientSecret: "v8XbdH2G26s6KbLv3pTmmyax"
    });
  }

  function getCamera() {
    if(ionic.Platform.is('browser')) {
      return $q(function(resolve) {
        resolve('../img/icon.png');
      });
    }

    return $cordovaCamera.getPicture();
  }

  return {
    getInfo: getInfo,
    getFingerprint: getFingerprint,
    getCamera: getCamera
  }

});
