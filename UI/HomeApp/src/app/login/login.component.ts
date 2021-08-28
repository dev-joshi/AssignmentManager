import { Component, OnInit } from '@angular/core';  
import { ActivatedRoute, Router } from '@angular/router';  
import { ILogin } from 'src/app/interfaces/login';  
import { AuthService } from '../services/auth.service'  
import { FormBuilder, FormGroup, Validators } from '@angular/forms';  
import { AppUserAuth } from '../security/app-user-auth';
import { SecurityService } from '../security/security.service';
import { AppUser } from '../security/app-user';

  
@Component({  
   selector: 'app-login',  
   templateUrl: './login.component.html',  
   styleUrls: ['./login.component.css']  
   })  
export class LoginComponent implements OnInit {  
  
   model: ILogin = { userid: "admin", password: "admin@123" }  
   submitted : boolean = false;
   loginForm!: FormGroup;  
   message!: string;  
   returnUrl!: string;  
   securityObject! : AppUserAuth;
   appUser : AppUser = new AppUser();
   redirectUrl : any = null;
   constructor(  
      private formBuilder: FormBuilder,  
      private router: Router,  
      private authService: AuthService,
      private securityService: SecurityService,
      private route : ActivatedRoute
   ) { }  
  
   ngOnInit() {  
      this.loginForm = this.formBuilder.group({  
         userid: ['', Validators.required],  
         password: ['', Validators.required]  
      });  
   this.submitted = false;
   this.redirectUrl = this.route?.snapshot?.queryParamMap?.get('returnUrl');
   this.authService.logout();  
   }  
  
// convenience getter for easy access to form fields  
get f() { return this.loginForm?.controls; }  
  
  
   login() {

      this.submitted =true;
      // stop here if form is invalid 

      if (this.loginForm?.invalid) {
         return;
      }
      else {
         this.appUser.userName = this.f.userid.value;
         this.appUser.password = this.f.password.value;
         
         this.securityService.login(this.appUser).subscribe(response => {
            this.securityObject = response;
            if(this.securityObject?.isAuthenticated)
            {
              localStorage.setItem('Details', JSON.stringify(this.securityObject));
              console.log("login true");
            }
            if (this.redirectUrl) {
               this.router.navigateByUrl(this.returnUrl);
            }
         },
         () => {
            this.securityObject = new AppUserAuth();
         });
      }
   }
} 