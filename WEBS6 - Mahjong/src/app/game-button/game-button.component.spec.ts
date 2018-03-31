import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {GameButtonComponent} from './game-button.component';
import {ApiService} from '../api.service';
import {ApiTestService} from '../api.test.service';
import {Game} from '../models/game';

describe('GameButtonComponent', () => {
  let component: GameButtonComponent;
  let fixture: ComponentFixture<GameButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GameButtonComponent]
    }).overrideComponent(GameButtonComponent, {
        set: {
          providers: [{ provide: ApiService, useClass: ApiTestService }]
        }
      }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameButtonComponent);
    component = fixture.componentInstance;
    component.game = new Game();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
