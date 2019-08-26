import { Component, OnInit, OnDestroy, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { ISubscription } from "rxjs/Subscription";

import { User } from '../../models/users/user';
import { UsersService } from '../../services/users.service';
import { SignUp } from '../../models/signUp/signUp';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AllUsersComponent implements OnInit, OnDestroy {

  public users: User[];
  public createNewUserMode: boolean = false;

  private getUsersSubscriber: ISubscription;

  constructor(private usersService: UsersService, private cdr: ChangeDetectorRef,
      private authorizationService: AuthorizationService) { }

  ngOnInit() {
    this.getUsersSubscriber = this.usersService.getAllUsers().subscribe(res => {
      this.users = res;
      this.cdr.detectChanges();
    });
  }

  ngOnDestroy(): void {
    this.getUsersSubscriber.unsubscribe();
  }

  public signUp(request: SignUp) {
    this.authorizationService.signUp(request).subscribe(res => {
      this.hideCreateUserMode();
    })
  }

  public hideCreateUserMode() {
    this.createNewUserMode = false;
  }

}
