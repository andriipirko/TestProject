import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { UserGroup } from '../models/userGroups/userGroup';
import { resourceConfigs } from '../config/resourceConfig';
import { ProfileService } from './profile.service';

@Injectable()
export class UserGroupsService {

  constructor(private http: HttpClient, private profile: ProfileService) { }

  public getUserGroups() : Observable<UserGroup[]> {
    return this.http.get<UserGroup[]>(`${resourceConfigs.apiAddress}/userGroups`, { headers: this.profile.getHeaders() });
  }

  public createUserGroup(userGroupName: string) : Observable<any> {
    return this.http.post<any>(`${resourceConfigs.apiAddress}/UserGroups`, { userGroupName: userGroupName }, { headers: this.profile.getHeaders() });
  }

  public deleteUserGroup(userGroupId: number) : Observable<any> {
    return this.http.delete<any>(`${resourceConfigs.apiAddress}/UserGroups/${userGroupId}`, { headers: this.profile.getHeaders() });
  }
}
