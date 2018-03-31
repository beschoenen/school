import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Api} from '../api.interface';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html'
})
export class MenuBarComponent implements OnInit {

  constructor(private router: Router, private api: ApiService) {
  }

  ngOnInit(): void {
    if (!localStorage.getItem('user-email')) {
      this.router.navigate(['/login']);
    }
  }

  logout(): void {
    this.api.logout();
    this.router.navigate(['login']);
  }

}
