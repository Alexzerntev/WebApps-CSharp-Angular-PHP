import { Component, OnInit, Input } from '@angular/core';

import { Gps } from '../device.model';

@Component({
  selector: 'app-gps-grid',
  templateUrl: './gps-grid.component.html',
  styleUrls: ['./gps-grid.component.scss']
})
export class GpsGridComponent implements OnInit {

  @Input() gpses: Gps[];
  page = 1;

  constructor() { }

  ngOnInit() {
  }


  onPageChange(page) {
    this.page = page;
  }
}
