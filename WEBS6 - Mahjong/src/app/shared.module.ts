import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MenuBarComponent} from './menu-bar/menu-bar.component';
import {GameBoardComponent} from './game-board/game-board.component';
import {TileComponent} from './tile/tile.component';
import {ToastModule} from 'ng2-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {RouterModule} from '@angular/router';

@NgModule({
  imports: [
    RouterModule,
    ToastModule.forRoot(),
    BrowserAnimationsModule,
    CommonModule
  ],
  declarations: [
    MenuBarComponent,
    GameBoardComponent,
    TileComponent
  ],
  exports: [
    MenuBarComponent,
    GameBoardComponent,
    TileComponent
  ]
})
export class SharedModule {
}
