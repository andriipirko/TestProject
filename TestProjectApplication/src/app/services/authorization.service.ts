import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { ISubscription } from "rxjs/Subscription";
import { SignIn } from '../models/signIn/signIn';
import { Router } from '@angular/router';
import { resourceConfigs } from '../config/resourceConfig';
import { SignInResponse } from '../models/signIn/signInResponse';
import { ProfileService } from './profile.service';
import { RefreshService } from './refresh.service';
import { SignUp } from '../models/signUp/signUp';
import { SignUpResponse } from '../models/signUp/signUpResponse';

@Injectable()
export class AuthorizationService implements OnDestroy {

  private signInSubscriber: ISubscription;

  constructor(private http: HttpClient, private profile: ProfileService,
    private router: Router, private refresh: RefreshService) { }

  ngOnDestroy(): void {
    this.signInSubscriber.unsubscribe();
  }

  public signIn(signInModel: SignIn) : void {
    this.signInSubscriber = this.sendSignInResponse(signInModel).subscribe(res => {
      this.profile.setToken(res.token);
      this.profile.setRole(res.role);
      this.refresh.successfulyAuthorized();
      this.router.navigate([ '/myInfo' ]);
    });
  }

  public signUp(signUpModel: SignUp): Observable<SignUpResponse> {
    return this.http.post<SignUpResponse>(`${resourceConfigs.apiAddress}/Authentication/SignUp`, signUpModel);
  }

  private sendSignInResponse(signInModel: SignIn) : Observable<SignInResponse> {
    return this.http.post<SignInResponse>(`${resourceConfigs.apiAddress}/Authentication/SignIn`, signInModel)
  }
}
