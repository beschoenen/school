import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {GameNewComponent} from './game-new.component';
import {GameBoardComponent} from '../game-board/game-board.component';
import {TileComponent} from '../tile/tile.component';
import {MenuBarComponent} from '../menu-bar/menu-bar.component';
import {ApiService} from '../api.service';
import {ApiTestService} from '../api.test.service';
import {FormsModule} from '@angular/forms';
import {RouterTestingModule} from '@angular/router/testing';
import {ToastModule, ToastsManager} from 'ng2-toastr';

describe('GameNewComponent', () => {
  let component: GameNewComponent;
  let fixture: ComponentFixture<GameNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        GameNewComponent,
        MenuBarComponent,
        GameBoardComponent,
        TileComponent
      ],
      imports: [
        FormsModule,
        ToastModule.forRoot(),
        RouterTestingModule
      ]
    }).overrideComponent(GameNewComponent, {
        set: {
          providers: [{ provide: ApiService, useClass: ApiTestService }]
        }
      }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
