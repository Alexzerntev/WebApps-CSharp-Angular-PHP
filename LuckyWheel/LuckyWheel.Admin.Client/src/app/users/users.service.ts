import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { environment } from '../../environments/environment';
import { User } from './users.model';
import { Spin } from './user-history/spins/spins.model';
import { Transactions } from './user-history/transactions/transaction.model';
import { Wheel } from '../wheel/wheel.model';

@Injectable()
export class UsersService {

  private apiUrl: string = environment.apiUri;
  constructor(private http: HttpClient) { }


  getUsers(): Observable<Array<User>> {
    return this.http.get(this.apiUrl + 'Users' + '/GetUsers/' )
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }

  getUser(userId: string): Observable<User> {
    return this.http.get(this.apiUrl   + 'Users' + '/GetUser/' + userId)
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }
  getSpins(userId: string): Observable<Array<Spin>> {
    console.log(this.apiUrl);
    return this.http.get(this.apiUrl  + 'History' + '/GetSpins/' + userId)
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }

  getTransactions(userId: string): Observable<Array<Transactions>> {
    console.log(this.apiUrl);
    return this.http.get(this.apiUrl  + 'History' + '/GetTransactions/' + userId)
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }
  getWheels(): Observable<Array<Wheel>> {
    return this.http.get(this.apiUrl + 'Users' + '/GetWheels/' )
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }
}
