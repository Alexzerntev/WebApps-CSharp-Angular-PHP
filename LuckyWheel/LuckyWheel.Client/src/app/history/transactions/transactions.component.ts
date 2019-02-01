import { Component, OnInit, AfterViewInit, Input} from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';

import { HistoryService } from '../history.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToasterService } from 'angular2-toaster';

import { Transactions } from './transactions.model';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit  {
  @Input() transactionlist: Array<Transactions>;
  id = '';

  constructor(
    private historyService: HistoryService,
     private router: Router,
     private route: ActivatedRoute,
     private loaderService: LoaderService,
     private toasterService: ToasterService
    ) { }

  ngOnInit() {
    this.loaderService.show();
    this.historyService.getTransactions().subscribe(result => {
      this.transactionlist = result;
      this.loaderService.hide();
    },
    error => {
      this.loaderService.hide();
      this.toasterService.pop('error', 'Σφάλμα', error);
    });
      console.log(this.transactionlist);
}
}
