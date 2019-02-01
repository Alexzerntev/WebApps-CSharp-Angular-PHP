import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import { UsersService } from '../users.service';

import { environment } from '../../../environments/environment';
import { User } from '../users.model';
import { Spin } from '../user-history/spins/spins.model';
import { Transactions } from '../user-history/transactions/transaction.model';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  private apiUrl: string = environment.apiUri;
  id = '';
  user: User = new User;
  spinList: Array<Spin>;
  transactionlist: Array<Transactions>;
  constructor(private usersService: UsersService, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      const id = params['UserId'];
      this.usersService.getUser(id).subscribe(result => {
        this.user = result;
      });
      this.usersService.getSpins(id).subscribe(result => {
        this.spinList = result;
        console.log(this.spinList);
      });
      this.usersService.getTransactions(id).subscribe(result => {
        this.transactionlist = result;
        console.log(this.transactionlist);
      });
    });
  }
}
