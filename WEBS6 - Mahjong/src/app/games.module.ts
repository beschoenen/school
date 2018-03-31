import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from './shared.module';
import {GameNewComponent} from './game-new/game-new.component';
import {GameListComponent} from './game-list/game-list.component';
import {HttpModule} from '@angular/http';
import {FormsModule} from '@angular/forms';
import {BrowserModule} from '@angular/platform-browser';
import {GameFilterPipe} from './game-filter/game-filter.pipe';
import {GameFilterComponent} from './game-filter/game-filter.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    GameListComponent,
    GameNewComponent,
    GameFilterComponent,
    GameFilterPipe
  ],
  bootstrap: [GameListComponent]
})
export class GamesModule {
}
