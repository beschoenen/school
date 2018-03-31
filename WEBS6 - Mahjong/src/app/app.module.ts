import {ApiService} from './api.service';
import {LoginModule} from './login/login.module';
import {LobbyModule} from './lobby/lobby.module';
import {SharedModule} from './shared.module';
import {GamesModule} from './games.module';
import {WebRouting} from './app.routes';
import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';

@NgModule({
  imports: [
    // Modules
    LoginModule,
    LobbyModule,
    GamesModule,
    SharedModule,
    // Routes
    WebRouting
  ],
  declarations: [
    AppComponent
  ],
  providers: [
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
