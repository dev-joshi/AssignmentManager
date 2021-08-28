import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { LOGIN_MOCKS } from './login-mocks';

const API_URL = "http://localhost:5000/api/security/login";

const httpOptions = {
  headers: new HttpHeaders(
    {
      'Content-Type': 'application/json'
    }
  ),
}

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  securityObject: AppUserAuth = new AppUserAuth();
  constructor(private client: HttpClient) { }

  login(entity: AppUser): Observable<AppUserAuth> {

    this.resetSecurityObject();
    Object.assign(this.securityObject,
      LOGIN_MOCKS.find(user => user.userName.toLowerCase() === entity.userName.toLowerCase() && user.password === entity.password));

    /*      return this.client.post<AppUserAuth>(API_URL + "login", entity, httpOptions).pipe(
            tap(response =>
               {
                  Object.assign(this.securityObject, response);
                  localStorage.setItem("bearerToken", this.securityObject.bearerToken);
               })
          )
    */

    if (this.securityObject.userName != "") {
      localStorage.setItem("bearerToken", this.securityObject.bearerToken);
    }

    return of<AppUserAuth>(this.securityObject);
  }

  logout(): void {
    this.resetSecurityObject();
  }

  resetSecurityObject(): void {
    this.securityObject.userName = "";
    this.securityObject.password = "";
    this.securityObject.bearerToken = "";
    this.securityObject.canAcessStudent = false;
    this.securityObject.canAcessTeacher = false;
    this.securityObject.isAuthenticated = false;

    localStorage.removeItem("bearerToken");
    localStorage.removeItem('Details');
  }

  loggedInDetails() : AppUserAuth
  {
     var cachedAuthDetails = localStorage.getItem('Details');

     if(cachedAuthDetails != null)
     {
        let auth: AppUserAuth = JSON.parse(cachedAuthDetails);
        return auth;
     }

     return new AppUserAuth();
  }
}
