import { Component, OnInit, Input } from '@angular/core';

import { HardwareStatus } from '../../device/device.model';

@Component({
  selector: 'app-hardware-chart',
  templateUrl: './hardware-chart.component.html',
  styleUrls: ['./hardware-chart.component.scss']
})
export class HardwareChartComponent implements OnInit {

  @Input() charStatuses = []

  view: any[] = [1000, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Time';
  showYAxisLabel = true;
  yAxisLabel = 'Value';

  // yScaleMin = 0;
  // yScaleMax = 5000;

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  // line, area
  autoScale = true;

  constructor() {
    //Object.assign(this, { single, multi })
  }

  // onSelect(event) {
  //   console.log(event);
  // }

  ngOnInit() {
  }

}

// }

// export var single = [
//   {
//     "name": "Germany",
//     "value": 8940000
//   },
//   {
//     "name": "USA",
//     "value": 5000000
//   },
//   {
//     "name": "France",
//     "value": 7200000
//   }
// ];

// export var multi = [
//   {
//     "name": "Germany",
//     "series": [
//       {
//         "name": "2010",
//         "value": 7300000
//       },
//       {
//         "name": "2011",
//         "value": 8940000
//       }
//     ]
//   },

//   {
//     "name": "USA",
//     "series": [
//       {
//         "name": "2010",
//         "value": 7870000
//       },
//       {
//         "name": "2011",
//         "value": 8270000
//       }
//     ]
//   },

//   {
//     "name": "France",
//     "series": [
//       {
//         "name": "2010",
//         "value": 5000002
//       },
//       {
//         "name": "2011",
//         "value": 5800000
//       }
//     ]
//   }
// ];

