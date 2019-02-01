import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

import { DashboardRootComponent } from './dashboard-root/dashboard-root.component';
import { DashboardCardsComponent } from './dashboard-cards/dashboard-cards.component';

@NgModule({
  imports: [
    SharedModule,
    RouterModule
  ],
  declarations: [
    DashboardRootComponent,
    DashboardCardsComponent
  ]
})
export class DashboardModule {
  
 }
