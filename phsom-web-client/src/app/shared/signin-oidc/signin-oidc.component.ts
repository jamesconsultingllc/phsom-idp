import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from '../open-id-connect.service';
import { Router } from '@angular/router';
import { environment } from 'environments/environment.prod';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.scss']
})
export class SigninOidcComponent implements OnInit {

  constructor(private openIdConnectService: OpenIdConnectService, private router: Router) { }

  ngOnInit() {
    const redirectUrl = this.openIdConnectService.redirectUrl;
    const router = this.router;
    this.openIdConnectService.userLoaded$.subscribe((userLoaded) => {
      if (userLoaded) {
        router.navigate([redirectUrl]);
      } else {
        if (!environment.production) {
          console.log('An error has occurred: user wasn\'t loaded');
        }
      }
    });

    this.openIdConnectService.handleCallBack();
  }
}
