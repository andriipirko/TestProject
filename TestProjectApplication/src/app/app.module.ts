import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from "@angular/common/http";

import { appRoutes } from './config/routes';

// Angular material imports
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';

// Custom services
import { AuthorizationService } from './services/authorization.service';
import { UsersService } from './services/users.service';
import { UserGroupsService } from './services/user-groups.service';

import { AppComponent } from './app.component';
import { UserCardComponent } from './components/user-card/user-card.component';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { MyInfoComponent } from './components/my-info/my-info.component';
import { AllUsersComponent } from './components/all-users/all-users.component';
import { UserGroupsComponent } from './components/user-groups/user-groups.component';
import { UserGroupCardComponent } from './components/user-group-card/user-group-card.component';
import { CreateNewUserModalComponent } from './components/all-users/create-new-user-modal/create-new-user-modal.component';
import { UpdateUserGroupComponent } from './components/user-groups/update-user-group/update-user-group.component';


@NgModule({
  declarations: [
    AppComponent,
    UserCardComponent,
    AuthorizationComponent,
    MyInfoComponent,
    AllUsersComponent,
    UserGroupsComponent,
    UserGroupCardComponent,
    CreateNewUserModalComponent,
    UpdateUserGroupComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatListModule
  ],
  providers: [
    AuthorizationService,
    UsersService,
    UserGroupsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
