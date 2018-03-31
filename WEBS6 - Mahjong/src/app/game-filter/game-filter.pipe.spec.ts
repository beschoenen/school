import {GameFilterPipe} from './game-filter.pipe';
import {ApiTestService} from '../api.test.service';
import {ApiService} from '../api.service';

describe('GameFilterPipe', () => {
  it('create an instance', () => {
    const pipe = new GameFilterPipe(new ApiTestService() as ApiService);
    expect(pipe).toBeTruthy();
  });
});
