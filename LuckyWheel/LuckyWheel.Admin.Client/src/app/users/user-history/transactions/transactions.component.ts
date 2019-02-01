import { Component, OnInit, AfterViewInit, Input} from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';

import { UserHistoryService } from '../user-history.service';

import { Transactions} from './transaction.model';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit  {
  @Input() transactionlist: Array<Transactions>;
  id = '';

  constructor(private userHistoryService: UserHistoryService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    console.log(this.transactionlist);
    console.log("klsfdjhg;iosdfgu;");
  //   this.route.params.subscribe((params: Params) => {
  //     const id = params['UserId'];
  // });
  }
}
