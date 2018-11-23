import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { IconsComponent } from '../../icons/icons.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { StudentListComponent } from '../../admin/students/student-list.component';
import { AuthenticatedRouteGuardService } from '../../shared/authenticated-route-guard.service';

export const adminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent, canActivate: [AuthenticatedRouteGuardService] },
    { path: 'user-profile',   component: UserProfileComponent, canActivate: [AuthenticatedRouteGuardService] },
    { path: 'student-list',   component: StudentListComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'notifications',  component: NotificationsComponent }
];
