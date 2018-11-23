import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { User, UserManager } from 'oidc-client';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {

  private userManager: UserManager = new UserManager(environment.openIdConnectSettings);
  private currentUser: User;
  private readonly redirectUrlKey: string = 'redirectUrl';

  userLoaded$ = new ReplaySubject<boolean>(1);

  get userAvailable(): boolean {
    return this.currentUser != null;
  }

  get user(): User {
    return this.currentUser;
  }

  get redirectUrl() : string {
    return localStorage.getItem(this.redirectUrlKey);
  }

  set redirectUrl(redirectUrl : string) {
    localStorage.setItem(this.redirectUrlKey, redirectUrl);
  }

  constructor() {
    this.userManager.clearStaleState();

    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded', user);
      }

      this.currentUser = user;
      this.userLoaded$.next(true);
    });

    this.userManager.events.addUserUnloaded((e) => {
      if (!environment.production) {
        console.log('User unloaded');
      }

      this.currentUser = null;
      this.userLoaded$.next(false);
    });
  }

  triggerSignIn() {
    this.userManager.signinRedirect().then(function () {
      if (!environment.production) {
        console.log('Redirection to signin triggered');
      }
    });
  }

  handleCallBack() {
    this.userManager.signinRedirectCallback().then(function (user) {
      if (!environment.production) {
        console.log('Callback after signin handled', user);
      }
    })
  }

  handleSilentCallback() {
    this.userManager.signinSilentCallback().then(function (user) {
      this.currentUser = user
      if (!environment.production) {
        console.log('Callback after silent signin handled.', user);
      }
    });
  }

  triggerSignOut() {
    this.userManager.signoutRedirect().then(function (resp) {
      if (!environment.production) {
        console.log('Redirection to sign out trigger', resp);
      }
    });
  }
}
