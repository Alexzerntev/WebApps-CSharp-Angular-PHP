import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './core/auth/services/auth.guard';
import { RoleGuard } from './core/auth/services/role.guard';

import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './core/auth/register/register.component';
import { LoginComponent } from './core/login/login.component';
import { ApplicationComponent } from './application/application.component';
import { ApplicationHistoryComponent } from './application-history/application-history.component';
import { SearchComponent } from './search/search.component';
import { StudentHelpComponent } from './student-help/student-help.component';
import { LessonsComponent } from './lessons/lessons.component';
import { SecretaryHistoryComponent } from './secretary-history/secretary-history.component';
import { SecretaryHelpComponent } from './secretary-help/secretary-help.component';
import { ProfileComponent } from './profile/profile.component';


const routes: Routes = [
  // user section
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'application', component: ApplicationComponent, canActivate: [AuthGuard] },
  { path: 'application-history', component: ApplicationHistoryComponent, canActivate: [AuthGuard] },
  { path: 'search', component: SearchComponent },
  { path: 'student-help', component: StudentHelpComponent },
  { path: 'profile', component: ProfileComponent },
  // secretary section

  { path: 'lessons', component: LessonsComponent, canActivate: [AuthGuard] },
  { path: 'secretary-history', component: SecretaryHistoryComponent, canActivate: [AuthGuard] },
  { path: 'secretary-help', component: SecretaryHelpComponent }
  // { path: 'user-info', component: UserInfoComponent, canActivate: [RoleGuard] }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
