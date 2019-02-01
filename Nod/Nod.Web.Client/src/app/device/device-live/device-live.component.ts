import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { DeviceDataService } from '../device-data.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToastrService } from 'ngx-toastr';

import { Gps } from '../device.model';

import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-device-live',
  templateUrl: './device-live.component.html',
  styleUrls: ['./device-live.component.scss']
})
export class DeviceLiveComponent implements OnInit {


  private hubsUri: string = environment.hubsUri;







  public hubConnection: HubConnection;
  public messages: string[] = [];
  public message: string;

  ngOnInit() {
    let builder = new HubConnectionBuilder();

    // as per setup in the startup.cs
    this.hubConnection = builder.withUrl(this.hubsUri + 'gps').build();

    // message coming from the server
    this.hubConnection.on("Send", (message) => {
      this.messages.push(message);
    });

    // starting the connection
    this.hubConnection.start();
  }

  send() {
    // message sent from the client to the server
    this.hubConnection.invoke("Echo", this.message);
    this.message = "";
  }

































  deviceId: string;
  deviceNickName: string;
  page = 1;

  limits = [100, 500, 1000, 5000, "All"]

  gpses: Gps[];
  limit;

  constructor(
    private deviceDataService: DeviceDataService,
    private loaderService: LoaderService,
    private toastrService: ToastrService,
    private route: ActivatedRoute
  ) { }

  // ngOnInit() {
  //   this.limit = 100;
  //   this.loaderService.show();
  //   this.route.params.subscribe(params => {
  //     this.deviceId = params['Id'];
  //     this.getGpses();
  //   });
  //   this.route.queryParams.subscribe(params => {
  //     this.deviceNickName = params['nickName'];
  //   });
  // }

  // getGpses() {
  //   this.deviceDataService.getGpses(this.deviceId, this.limit == "All" ? 0 : this.limit)
  //     .subscribe(
  //       result => {
  //         this.gpses = result;
  //         this.loaderService.hide();
  //       },
  //       error => {
  //         this.loaderService.hide();
  //         this.toastrService.error(error.error, "Error");
  //       }
  //     )
  // }

  onPageChange(page) {
    this.page = page;
  }
}
