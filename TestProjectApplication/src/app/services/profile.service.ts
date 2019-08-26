import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private role: string;
  public successfulyAuthorized: boolean = false;

  constructor() { }

  public setToken(token: string): void {
    localStorage.setItem('auth_token', token);
    this.successfulyAuthorized = true;
  }

  public getToken(): string {
    return localStorage.getItem('auth_token');
  }

  public setRole(role: string) {
    this.role = role.toLowerCase();
  }

  public getRole(): string {
    return this.role;
  }

  public hasPermission(requiredPermission: string) : boolean {
    switch (requiredPermission) {
      case 'admin':
        return this.role == 'admin';
      case 'moderator':
        return this.role == 'moderator' || this.role == 'admin';
      case 'user':
        return this.role == 'admin' || this.role == 'moderator' || this.role == 'user';
    }
  }

  public getHeaders() : HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
      'Authorization': `Bearer ${localStorage.getItem('auth_token')}`});
  }
}
