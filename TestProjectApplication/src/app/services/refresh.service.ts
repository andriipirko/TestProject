import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefreshService {
  private authorizedObject = new Subject<boolean>();

  public authorized = this.authorizedObject.asObservable();

  constructor() { }

  public successfulyAuthorized() {
    this.authorizedObject.next(true);
  }
}
