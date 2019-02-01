import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './core/auth/services/auth.guard';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './core/auth/login/login.component';
import { RegisterComponent } from './core/auth/register/register.component';
import { DashboardRootComponent } from './dashboard/dashboard-root/dashboard-root.component';
import { DeviceListComponent } from './device/device-list/device-list.component';
import { DeviceHistoryComponent } from './device/device-history/device-history.component'
import { DeviceLiveComponent } from './device/device-live/device-live.component'

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardRootComponent, canActivate: [AuthGuard] },
  { path: 'devices', component: DeviceListComponent, canActivate: [AuthGuard] },
  { path: 'deviceHistory/:Id', component: DeviceHistoryComponent, canActivate: [AuthGuard] },
  { path: 'deviceLive/:Id', component: DeviceLiveComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
