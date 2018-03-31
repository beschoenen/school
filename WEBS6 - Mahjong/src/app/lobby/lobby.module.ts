import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GameButtonComponent} from '../game-button/game-button.component';
import {TimelineComponent} from '../timeline/timeline.component';
import {LobbyComponent} from './lobby.component';
import {UserListComponent} from '../user-list/user-list.component';
import {SharedModule} from '../shared.module';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    UserListComponent,
    LobbyComponent,
    TimelineComponent,
    GameButtonComponent
  ],
  bootstrap: [LobbyComponent]
})
export class LobbyModule {
}
