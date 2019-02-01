import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common'

import { DeviceDataService } from '../device-data.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToastrService } from 'ngx-toastr';

import { Gps, HardwareStatus } from '../device.model';

@Component({
  selector: 'app-device-history',
  templateUrl: './device-history.component.html',
  styleUrls: ['./device-history.component.scss']
})
export class DeviceHistoryComponent implements OnInit {

  categories = ["Gps", "Hardware"];
  selectedCategory = "Gps";

  deviceId: string;
  deviceNickName: string;
  page = 1;

  startDateTime: Date;
  endDateTime: Date;


  gpses: Gps[];
  hardwareStatuses: HardwareStatus[];
  charStatuses = [];

  constructor(
    private deviceDataService: DeviceDataService,
    private loaderService: LoaderService,
    private toastrService: ToastrService,
    private route: ActivatedRoute,
    public datepipe: DatePipe
  ) { }

  ngOnInit() {
    this.startDateTime = new Date(Date.now());
    this.endDateTime = new Date(Date.now());
    this.startDateTime.setHours(this.endDateTime.getHours() - 1);
    this.route.params.subscribe(params => {
      this.deviceId = params['Id'];
      this.getGpses();
    });
    this.route.queryParams.subscribe(params => {
      this.deviceNickName = params['nickName'];
    });
  }

  onSelectChange() {
    this.getItems();
  }

  getGpses() {
    this.loaderService.show();
    this.deviceDataService.getGpsesByDateTime(this.deviceId, this.startDateTime, this.endDateTime)
      .subscribe(
        result => {
          this.gpses = result;
          this.loaderService.hide();
        },
        error => {
          this.loaderService.hide();
          this.toastrService.error(error.error, "Error");
        }
      )
  }

  getHardwareStatuses() {
    this.loaderService.show();
    this.deviceDataService.getHardwareStatusesByDateTime(this.deviceId, this.startDateTime, this.endDateTime)
      .subscribe(
        result => {
          this.hardwareStatuses = result;
          this.toChartData();
          this.loaderService.hide();
        },
        error => {
          this.loaderService.hide();
          this.toastrService.error(error.error, "Error");
        }
      )
  }

  getItems() {
    if (this.selectedCategory == "Gps") {
      this.getGpses();
    }
    else if (this.selectedCategory == "Hardware") {
      this.getHardwareStatuses();
    }
  }

  toChartData() {
    var mainPower = [];
    var battery = [];
    var mcuTemperature = [];

    for (let index = this.hardwareStatuses.length - 1; index >= 0; index--) {
      mainPower.push({
        "name": this.datepipe.transform(this.hardwareStatuses[index].DateTime, 'HH:mm'),
        "value": this.hardwareStatuses[index].MainPower
      });
      battery.push({
        "name": this.datepipe.transform(this.hardwareStatuses[index].DateTime, 'HH:mm'),
        "value": this.hardwareStatuses[index].Battery
      });
      mcuTemperature.push({
        "name": this.datepipe.transform(this.hardwareStatuses[index].DateTime, 'HH:mm'),
        "value": this.hardwareStatuses[index].McuTemperature
      });
    }
    this.charStatuses = [{
      "name": "Main Power",
      "series": mainPower
    },
    {
      "name": "Battery",
      "series": battery
    },
    {
      "name": "Mcu Temperature",
      "series": mcuTemperature
    }]
  }
}
