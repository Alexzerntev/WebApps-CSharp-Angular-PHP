import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRootComponent } from './home-root/home-root.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule
  ],
  declarations: [
    HomeRootComponent
  ],
  schemas:[
    NO_ERRORS_SCHEMA
  ]

})
export class HomeModule { }
