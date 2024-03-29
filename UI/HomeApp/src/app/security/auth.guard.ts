import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AppUserAuth } from './app-user-auth';
import { SecurityService } from './security.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private security: SecurityService,
    private router : Router) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let claimType: keyof AppUserAuth = route.data["claimType"];
      
      if( this.security?.securityObject?.isAuthenticated
       && (!route.data["claimType"] || this.security?.securityObject[claimType]))
       {
         console.log("yaya");
         console.log(route.data);
         console.log(route.data["externalUrl"]);
          if(route.data["externalUrl"])
          { console.log("yaya");
             window.location.href = route.data['externalUrl'];
          }
           return true;
       }
       else
       {
         this.router.navigate(['login'],
         {queryParams: {returnUrl: state.url}});
         return false; 
       }
  }
}
