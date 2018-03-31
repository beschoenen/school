import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoginCallbackComponent} from './callback.component';
import {LoginComponent} from './login.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    LoginComponent,
    LoginCallbackComponent
  ],
  bootstrap: [LoginComponent]
})
export class LoginModule {
}
