import {browser, by, element} from 'protractor';

export class Mahjong {

  get base() {
    return browser.baseUrl;
  }

  navigateTo(path) {
    return browser.get(path);
  }

  url() {
    return browser.getCurrentUrl();
  }

  click(selector) {
    element(by.css(selector)).then(function(ok) {
      ok.element(by.buttonText(ok)).click();
    });
  }

}
