import { Component, ChangeDetectionStrategy, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/users/user';
import { ProfileService } from './services/profile.service';
import { UsersService } from './services/users.service';
import { RefreshService } from './services/refresh.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements AfterViewInit {
  ngAfterViewInit(): void {
    // this.cdr.detectChanges();
  }
  constructor(public profile: ProfileService, private cdr: ChangeDetectorRef,
    private refresh: RefreshService, private router: Router) {
    this.refresh.authorized.subscribe(res => {
      this.cdr.detectChanges();
    });
  }

  public navigationOptions = [
    {
      title: 'My information',
      path: '/myInfo',
      permission: 'user'
    },
    {
      title: 'All users',
      path: '/allUsers',
      permission: 'moderator'
    },
    {
      title: 'User groups',
      path: '/userGroups',
      permission: 'moderator'
    }
  ];

  public navigate(path: string) {
    this.router.navigate([path]);
  }
}
