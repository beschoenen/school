import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {LobbyComponent} from './lobby.component';
import {MenuBarComponent} from '../menu-bar/menu-bar.component';
import {UserListComponent} from '../user-list/user-list.component';
import {TimelineComponent} from '../timeline/timeline.component';
import {GameBoardComponent} from '../game-board/game-board.component';
import {TileComponent} from '../tile/tile.component';
import {GameButtonComponent} from '../game-button/game-button.component';
import {ApiService} from '../api.service';
import {ApiTestService} from '../api.test.service';
import {RouterTestingModule} from '@angular/router/testing';
import {FormsModule} from '@angular/forms';
import {ToastModule} from 'ng2-toastr';

describe('LobbyComponent', () => {
  let component: LobbyComponent;
  let fixture: ComponentFixture<LobbyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        LobbyComponent,
        MenuBarComponent,
        UserListComponent,
        TimelineComponent,
        GameBoardComponent,
        TileComponent,
        GameButtonComponent
      ],
      imports: [
        FormsModule,
        RouterTestingModule,
        ToastModule.forRoot(),
      ]
    }).overrideComponent(LobbyComponent, {
        set: {
          providers: [{ provide: ApiService, useClass: ApiTestService }]
        }
      }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LobbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
