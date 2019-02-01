import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class WidgetComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
