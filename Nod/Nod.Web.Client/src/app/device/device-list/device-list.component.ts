import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DeviceDataService } from '../device-data.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToastrService } from 'ngx-toastr';



import { Device } from '../device.model';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.scss']
})
export class DeviceListComponent implements OnInit {

  devices: Device[];
  page = 1;

  constructor(
    private deviceDataService: DeviceDataService,
    private loaderService: LoaderService,
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loaderService.show();
    this.deviceDataService.getDevices()
      .subscribe(
        result => {
          this.devices = result;
          this.loaderService.hide();
        },
        error => {
          this.loaderService.hide();
          this.toastrService.error(error.error, "Error");
        }
      )
  }

  onPageChange(page) {
    this.page = page;
  }

  openDevice(device: Device, isLive: boolean) {
    if (isLive) {
      this.router.navigate([`/deviceLive/${device.Id}`], { queryParams: { nickName: device.NickName } });
      return;
    }
    this.router.navigate([`/deviceHistory/${device.Id}`], { queryParams: { nickName: device.NickName } });
    return;
  }

}
