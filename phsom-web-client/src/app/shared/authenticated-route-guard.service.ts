import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { OpenIdConnectService } from './open-id-connect.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticatedRouteGuardService implements CanActivate {

  constructor(private openIdConnectService: OpenIdConnectService, private router: Router) { }

  canActivate( route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
    if (this.openIdConnectService.userAvailable) {
      return true;
    } else {
      this.openIdConnectService.redirectUrl = state.url;
      this.openIdConnectService.triggerSignIn();
      return false;
    }
  }
}
