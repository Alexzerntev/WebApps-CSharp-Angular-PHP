import { NgModule } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { SharedModule } from '../shared/shared.module';

import { HardwareChartComponent } from './hardware-chart/hardware-chart.component';

@NgModule({
  imports: [
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    NgxChartsModule
  ],
  declarations: [
    HardwareChartComponent
  ],
  exports: [
    HardwareChartComponent
  ]
})
export class ChartsModule { }

