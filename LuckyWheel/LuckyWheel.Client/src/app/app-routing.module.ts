import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeRootComponent } from './home/home-root/home-root.component';
import { BlankComponent } from './shared/blank/blank.component'; 

const routes: Routes = [
  {path: '',   redirectTo: '/home', pathMatch: 'full' },
  {path: 'blank', component: BlankComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
