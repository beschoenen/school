import {Mahjong} from './app.po';

describe('mahjong App', () => {
  let app: Mahjong;

  beforeEach(() => {
    app = new Mahjong();
  });

  it('should automatically redirect to login', () => {
    app.navigateTo('/');
    expect(app.url()).toBe(`${app.base}/login`);
  });
});
