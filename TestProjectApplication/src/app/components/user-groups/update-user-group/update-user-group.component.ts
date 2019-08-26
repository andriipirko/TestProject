import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';
import { UserGroup } from '../../../models/userGroups/userGroup';
import { User } from '../../../models/users/user';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-update-user-group',
  templateUrl: './update-user-group.component.html',
  styleUrls: ['../../../shared-styles/modal-component-styles.scss', './update-user-group.component.scss']
})
export class UpdateUserGroupComponent implements OnInit {

  @Input()
  public userGroup: UserGroup;

  public users: User[];

  constructor(private usersService: UsersService, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.usersService.getAllUsers().subscribe(res => {
      this.users = res;
      this.cdr.detectChanges();
    });
  }

}
