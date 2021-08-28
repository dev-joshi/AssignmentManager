import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './security/auth.guard';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
    data: {
      externalUrl: 'https://www.youtube.com/'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'student',
    component: AuthGuard,
    canActivate: [AuthGuard],
    data : [
      {claimType: 'canAcessStudent'},
      {externalUrl: 'https://www.youtube.com/'}]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
