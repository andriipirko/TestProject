import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { SignUp } from '../../../models/signUp/signUp';

@Component({
  selector: 'app-create-new-user-modal',
  templateUrl: './create-new-user-modal.component.html',
  styleUrls: ['../../../shared-styles/modal-component-styles.scss', './create-new-user-modal.component.scss']
})
export class CreateNewUserModalComponent implements OnInit {
  public signUpModel = new SignUp();

  @Output()
  signedUp = new EventEmitter<SignUp>();

  @Output()
  canceled = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit() {
  }

  public signUp() {
    this.signedUp.emit(this.signUpModel);
  }

  public cancel() {
    this.canceled.emit(true);
  }

}
