import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../models/users/user';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {

  @Input()
  public user: User;

  @Input()
  public showDeleteAction: boolean = true;

  constructor() { }

  ngOnInit() {
  }

}
