import { Component, OnInit, AfterViewInit, Input} from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';

import { UserHistoryService } from '../user-history.service';

import { Spin } from './spins.model';
import { UserHistoryModule } from '../user-history.module';
import { identifierModuleUrl } from '@angular/compiler';

@Component({
  selector: 'app-spins',
  templateUrl: './spins.component.html',
  styleUrls: ['./spins.component.scss']
})
export class SpinsComponent implements AfterViewInit {
  @Input() spinList: Array<Spin>;
  id = '';
  constructor(private userHistoryService: UserHistoryService, private router: Router, private route: ActivatedRoute) { }

  ngAfterViewInit() {
//     this.route.params.subscribe((params: Params) => {
//       const id = params['UserId'];
// });
  }
}
