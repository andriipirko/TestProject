import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ISubscription } from "rxjs/Subscription";

import { User } from '../../models/users/user';
import { UsersService } from '../../services/users.service';
import { ProfileService } from '../../services/profile.service';
import { RefreshService } from '../../services/refresh.service';

@Component({
  selector: 'app-my-info',
  templateUrl: './my-info.component.html',
  styleUrls: ['./my-info.component.scss']
})
export class MyInfoComponent implements OnInit, OnDestroy {
  public user: User;

  private getUserInfoSubsciber: ISubscription;

  constructor(private userService: UsersService, private profile: ProfileService, private router: Router,
    private refresh: RefreshService) { }

  ngOnInit() {
    this.getUserInfoSubsciber = this.userService.getCurrentUserInfo().subscribe(res => {
      this.user = res;
      this.profile.successfulyAuthorized = true;
      this.profile.setRole(res.role);
      this.refresh.successfulyAuthorized();
    }, (err: HttpErrorResponse) => {
      if (err.status == 401) {
        this.router.navigate(['authorization']);
      }
      console.log('error message', err.message);
    }););
  }

  ngOnDestroy(): void {
    this.getUserInfoSubsciber.unsubscribe();
  }

}
