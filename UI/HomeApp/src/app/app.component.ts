import { Component } from '@angular/core';
import { AppUserAuth } from './security/app-user-auth';
import { SecurityService } from './security/security.service';

@Component({
  selector: 'ah-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'HomeApp';
  securityObject!: AppUserAuth; 

  constructor(private securityService : SecurityService)
  {
    this.securityObject = securityService.securityObject;
  }

  logOut() : void {
    this.securityService.logout();
  }
}
