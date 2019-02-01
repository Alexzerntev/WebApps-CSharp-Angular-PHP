import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../shared/shared.module'

import { MapComponent } from './map/map.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [MapComponent],
  exports: [MapComponent]
})
export class MapsModule { }
