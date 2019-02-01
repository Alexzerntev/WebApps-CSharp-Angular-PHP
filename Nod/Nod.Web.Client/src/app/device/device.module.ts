import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { MapsModule } from '../maps/maps.module';
import { ChartsModule } from '../charts/charts.module';

import { DeviceDataService } from './device-data.service';

import { DeviceListComponent } from './device-list/device-list.component';
import { DeviceHistoryComponent } from './device-history/device-history.component';
import { DeviceLiveComponent } from './device-live/device-live.component';
import { GpsGridComponent } from './gps-grid/gps-grid.component';
import { HardwareStatusGridComponent } from './hardware-status-grid/hardware-status-grid.component';


@NgModule({
  imports: [
    SharedModule,
    MapsModule,
    ChartsModule
  ],
  declarations: [
    DeviceListComponent,
    DeviceHistoryComponent,
    DeviceLiveComponent,
    GpsGridComponent,
    HardwareStatusGridComponent
  ],
  providers: [
    DeviceDataService
  ]
})
export class DeviceModule { }
