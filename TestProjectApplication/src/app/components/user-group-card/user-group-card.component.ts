import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserGroup } from '../../models/userGroups/userGroup';

@Component({
  selector: 'app-user-group-card',
  templateUrl: './user-group-card.component.html',
  styleUrls: ['./user-group-card.component.scss']
})
export class UserGroupCardComponent implements OnInit {

  @Input()
  userGroup: UserGroup;

  @Output()
  edit = new EventEmitter<number>();

  @Output()
  delete = new EventEmitter<number>();


  constructor() { }

  ngOnInit() {
  }

  public editUserGroup(userGroupId: number) {
    this.edit.emit(userGroupId);
  }

  public deleteUserGroup(userGroupId: number) {
    this.delete.emit(userGroupId);
  }

}
