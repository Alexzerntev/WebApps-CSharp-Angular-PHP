import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { SharedModule } from '../shared/shared.module';

import { SpinGameService } from './spin-game.service';

import { WheelComponent } from './wheel/wheel.component';


@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule
  ],
  declarations: [
    WheelComponent
  ],
  providers: [
    SpinGameService
  ]
})
export class SpinGameModule { }
