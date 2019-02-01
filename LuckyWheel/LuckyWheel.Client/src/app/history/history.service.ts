import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { environment } from '../../environments/environment';

import { Spin } from './spins/spins.model';
import { Transactions } from './transactions/transactions.model';

@Injectable()
export class HistoryService {

  private apiUrl: string = environment.apiUri + 'History/';
  constructor(private http: HttpClient) { }

  getSpins(): Observable<Array<Spin>> {
    console.log(Spin);
    return this.http.get(this.apiUrl  + 'Spins')
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }

  getTransactions(): Observable<Array<Transactions>> {
    console.log(this.apiUrl);
    return this.http.get(this.apiUrl  + 'Transactions')
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }
}
