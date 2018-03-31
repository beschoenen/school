import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Api} from '../api.interface';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private api: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    if (this.api.isLoggedIn) {
      this.router.navigate(['games']);
    }
  }

  public login() {
    location.href = this.api.loginUrl;
  }

}
