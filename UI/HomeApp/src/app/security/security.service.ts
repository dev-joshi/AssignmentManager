import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { LOGIN_MOCKS } from './login-mocks';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  securityObject : AppUserAuth = new AppUserAuth();
  constructor() { }

  login (entity: AppUser) : Observable<AppUserAuth> {
      this.resetSecurityObject();

      Object.assign(this.securityObject,
        LOGIN_MOCKS.find(user => user.userName.toLowerCase() === entity.userName.toLowerCase()));

        if (this.securityObject.userName != "")
        {
            localStorage.setItem("bearerToken", this.securityObject.bearerToken);
        }

        return of<AppUserAuth> (this.securityObject);
  }

  logout() : void {
      this.resetSecurityObject();
  }

  resetSecurityObject () : void {
    this.securityObject.userName = "";
    this.securityObject.password = "";
    this.securityObject.bearerToken = "";
    this.securityObject.canAcessStudent = false;
    this.securityObject.canAcessTeacher = false;
    this.securityObject.isAuthenticated = false;

    localStorage.removeItem("bearerToken");
  }
}
