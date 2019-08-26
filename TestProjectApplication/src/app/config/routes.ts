import { Routes } from '@angular/router';

import { AuthorizationComponent } from '../components/authorization/authorization.component';
import { MyInfoComponent } from '../components/my-info/my-info.component';
import { AllUsersComponent } from '../components/all-users/all-users.component';
import { UserGroupsComponent } from '../components/user-groups/user-groups.component';

export const appRoutes: Routes = [
  {
    component: AuthorizationComponent,
    path: 'authorization',
  },
  {
    component: MyInfoComponent,
    path: 'myInfo'
  },
  {
    component: AllUsersComponent,
    path: 'allUsers'
  },
  {
    component: UserGroupsComponent,
    path: 'userGroups'
  },
  { path: '', redirectTo: 'myInfo', pathMatch: 'full' }
]