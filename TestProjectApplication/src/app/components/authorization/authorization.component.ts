import { Component, OnInit } from '@angular/core';
import { SignIn } from '../../models/signIn/signIn';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.scss']
})
export class AuthorizationComponent implements OnInit {
  public signInModel: SignIn;

  constructor(private authService: AuthorizationService) { }

  ngOnInit() {
    this.signInModel = new SignIn();
  }

  public signIn() {
    this.authService.signIn(this.signInModel);
  }

}
