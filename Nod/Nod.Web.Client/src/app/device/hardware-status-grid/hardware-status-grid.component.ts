import { Component, OnInit, Input } from '@angular/core';

import { HardwareStatus } from '../device.model';

@Component({
  selector: 'app-hardware-status-grid',
  templateUrl: './hardware-status-grid.component.html',
  styleUrls: ['./hardware-status-grid.component.scss']
})
export class HardwareStatusGridComponent implements OnInit {

  @Input() hardwareStatuses: HardwareStatus[];

  page = 1;

  constructor() { }

  ngOnInit() {
  }


  onPageChange(page) {
    this.page = page;
  }

}
