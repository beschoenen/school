import {RouterModule, Routes} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {GameListComponent} from './game-list/game-list.component';
import {GameNewComponent} from './game-new/game-new.component';
import {LobbyComponent} from './lobby/lobby.component';
import {LoginCallbackComponent} from './login/callback.component';
import {LoginComponent} from './login/login.component';

const appRoutes: Routes = [
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'login/callback', component: LoginCallbackComponent},
  {path: 'games', component: GameListComponent},
  {path: 'games/new', component: GameNewComponent},
  {path: 'games/:id/lobby', component: LobbyComponent}
];

export const WebRouting: ModuleWithProviders = RouterModule.forRoot(appRoutes);
