import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { ModuleWithProviders } from '@angular/core';

import { DashboardComponent } from './dashboard/dashboard.component';
import { UsersListComponent } from './users/users-list/users-list.component';
import { WheelComponent } from './wheel/wheel.component';
import { UserProfileComponent } from './users/user-profile/user-profile.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SpinsComponent } from './users/user-history/spins/spins.component';
import { TransactionsComponent } from './users/user-history/transactions/transactions.component';


export const routes: Routes = [
  { path: 'dashboard',      component: DashboardComponent },
  { path: 'users-list',   component: UsersListComponent },
  { path: 'wheel',   component: WheelComponent },
  { path: '',          redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'user-profile/:UserId',   component: UserProfileComponent},
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)],
  exports: [

  ],
})
export class AppRoutingModule { }
