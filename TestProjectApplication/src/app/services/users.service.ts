import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

import { resourceConfigs } from '../config/resourceConfig';
import { ProfileService } from './profile.service';
import { User } from '../models/users/user';

@Injectable()
export class UsersService {

  constructor(private http: HttpClient, private profile: ProfileService) { }

  public getCurrentUserInfo() : Observable<User> {
    return this.http.get<User>(`${resourceConfigs.apiAddress}/Users/UserInfo`, { headers: this.profile.getHeaders() });
  }

  public getAllUsers() : Observable<User[]> {
    return this.http.get<User[]>(`${resourceConfigs.apiAddress}/Users`, { headers: this.profile.getHeaders() });
  }
}
