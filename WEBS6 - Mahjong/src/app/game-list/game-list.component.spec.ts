import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {GameListComponent} from './game-list.component';
import {MenuBarComponent} from '../menu-bar/menu-bar.component';
import {GameFilterComponent} from '../game-filter/game-filter.component';
import {GameFilterPipe} from '../game-filter/game-filter.pipe';
import {ApiService} from '../api.service';
import {ApiTestService} from '../api.test.service';
import {RouterTestingModule} from '@angular/router/testing';

describe('GameListComponent', () => {
  let component: GameListComponent;
  let fixture: ComponentFixture<GameListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        GameListComponent,
        MenuBarComponent,
        GameFilterComponent,
        GameFilterPipe,
      ],
      imports: [
        RouterTestingModule
      ]
    }).overrideComponent(GameListComponent, {
        set: {
          providers: [{ provide: ApiService, useClass: ApiTestService }]
        }
      }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
