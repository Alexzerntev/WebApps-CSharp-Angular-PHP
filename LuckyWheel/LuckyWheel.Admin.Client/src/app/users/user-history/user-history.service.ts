import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { environment } from '../../../environments/environment';

import { Spin } from './spins/spins.model';
import { Transactions } from './transactions/transaction.model';


@Injectable()
export class UserHistoryService {

  private apiUrl: string = environment.apiUri + 'History';
  constructor(private http: HttpClient) { }

  getSpins(userId: string): Observable<Array<Spin>> {
    return this.http.get(this.apiUrl + '/GetSpins/' + userId)
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }

  getTransactions(userId: string): Observable<Array<Transactions>> {
    return this.http.get(this.apiUrl + '/GetTransactions/' + userId)
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }
}
