import { Component, OnInit, AfterViewInit, Input} from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';

import { HistoryService } from '../history.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToasterService } from 'angular2-toaster';

import { Spin } from './spins.model';
import { HistoryModule } from '../history.module';
import { identifierModuleUrl } from '@angular/compiler';

@Component({
  selector: 'app-spins',
  templateUrl: './spins.component.html',
  styleUrls: ['./spins.component.scss']
})
export class SpinsComponent implements AfterViewInit {
  @Input() spinList: Array<Spin>;
  id = '';
  constructor(
    private historyService: HistoryService,
    private router: Router,
    private route: ActivatedRoute,
    private loaderService: LoaderService,
    private toasterService: ToasterService) { }

  ngAfterViewInit() {
    this.loaderService.show();
    this.historyService.getSpins().subscribe(result => {
      this.spinList = result;
      this.loaderService.hide();
    },
    error => {
      this.loaderService.hide();
      this.toasterService.pop('error', 'Σφάλμα', error);
    });
      console.log(this.spinList);
  }
}
