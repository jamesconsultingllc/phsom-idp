import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import { DashboardComponent } from './dashboard/dashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { TableListComponent } from './table-list/table-list.component';
import { IconsComponent } from './icons/icons.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { UpgradeComponent } from './upgrade/upgrade.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { StudentListComponent } from './admin/students/student-list.component';
import { SigninOidcComponent } from './shared/signin-oidc/signin-oidc.component';
import { OpenIdConnectService } from './shared/open-id-connect.service';
import { AuthenticatedRouteGuardService } from './shared/authenticated-route-guard.service';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    SigninOidcComponent
  ],
  providers: [OpenIdConnectService, AuthenticatedRouteGuardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
