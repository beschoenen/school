import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  template: ''
})
export class LoginCallbackComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      localStorage.setItem('user-email', params['username']);
      localStorage.setItem('user-token', params['token']);

      this.router.navigate(['games']);
    });
  }

}
