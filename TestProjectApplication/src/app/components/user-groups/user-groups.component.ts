import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { ISubscription } from "rxjs/Subscription";
import { UserGroup } from '../../models/userGroups/userGroup';
import { UserGroupsService } from '../../services/user-groups.service';

@Component({
  selector: 'app-user-groups',
  templateUrl: './user-groups.component.html',
  styleUrls: ['./user-groups.component.scss']
})
export class UserGroupsComponent implements OnInit, OnDestroy {
  
  public userGroups: UserGroup[];
  public selectedUserGroup: UserGroup;
  public editUserGroupMode: boolean = false;

  private getUserGroupsSubscriber: ISubscription;

  constructor(private userGroupsService: UserGroupsService, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.retrieveUserGroups();
  }

  ngOnDestroy(): void {
    this.getUserGroupsSubscriber.unsubscribe();
  }

  public editUserGroup(userGroupId: number) {
    this.selectedUserGroup = this.userGroups.find(ug => ug.id == userGroupId);
    this.editUserGroupMode = true;
  }

  public createUserGroup() {
    let newUserGroupName = prompt('User group name: ');
    if (newUserGroupName.length != 0) {
      this.userGroupsService.createUserGroup(newUserGroupName).subscribe(res => {
        this.retrieveUserGroups();
      })
    }
  }

  public deleteUserGroup(userGroupId: number) {
    if (confirm('Are you sure?')) {
      this.userGroupsService.deleteUserGroup(userGroupId).subscribe(res => {
        this.retrieveUserGroups();
      })
    }
  }

  private retrieveUserGroups() {
    this.getUserGroupsSubscriber = this.userGroupsService.getUserGroups().subscribe(res => {
      this.userGroups = res;
      this.cdr.detectChanges();
    });
  }

}
