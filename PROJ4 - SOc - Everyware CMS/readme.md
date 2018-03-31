# Bartiméus CMS

|  | Travis | Codecov | StyleCI |
|---|---|---|---|
| __Develop__ | [![Build Status](https://travis-ci.org/beschoenen/SOc-everyware-cms.svg?branch=develop)](https://travis-ci.org/beschoenen/SOc-everyware-cms) | [![codecov](https://codecov.io/gh/beschoenen/SOc-everyware-cms/branch/develop/graph/badge.svg)](https://codecov.io/gh/beschoenen/SOc-everyware-cms) | [![StyleCI](https://styleci.io/repos/88636615/shield?branch=develop)](https://styleci.io/repos/88636615) |
| __Master__ | [![Build Status](https://travis-ci.org/beschoenen/SOc-everyware-cms.svg?branch=master)](https://travis-ci.org/beschoenen/SOc-everyware-cms) | [![codecov](https://codecov.io/gh/beschoenen/SOc-everyware-cms/branch/master/graph/badge.svg)](https://codecov.io/gh/beschoenen/SOc-everyware-cms) | [![StyleCI](https://styleci.io/repos/88636615/shield?branch=master)](https://styleci.io/repos/88636615) |

## Run tests
### Normal tests
```$ phpunit```

### Browser tests
Make sure PhantomJS is installed:
```
$ npm install -g phantomjs
```

Run tests:
```
$ phantomjs --webdriver=127.0.0.1:9515
$ DB_CONNECTION=selenium php artisan serve --port 8001
$ composer browser-tests
```
