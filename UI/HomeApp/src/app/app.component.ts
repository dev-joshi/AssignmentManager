import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { AppUserAuth } from './security/app-user-auth';
import { SecurityService } from './security/security.service';

@Component({
  selector: 'ah-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'HomeApp';
  securityObject!: AppUserAuth; 
  isAuthenticated : boolean = false;

  constructor(private securityService : SecurityService)
  {
    console.log("cons called");
    this.securityObject = securityService.securityObject;
  }

  ngOnInit(): void {
    if(!this.securityObject?.isAuthenticated)
    {
      console.log("called");

      const details = this.securityService.loggedInDetails();
      if(details && details.isAuthenticated)
      {
        Object.assign(this.securityObject, details); 
      }
    }
  }

  public logOut() : void {
    this.securityService.logout();
  }
}
