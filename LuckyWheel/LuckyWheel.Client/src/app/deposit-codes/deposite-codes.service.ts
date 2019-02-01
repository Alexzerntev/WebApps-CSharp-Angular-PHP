import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { environment } from '../../environments/environment';

import { DepositeCode } from './deposit-code/deposit-code.model';

@Injectable()
export class DepositeCodesService {

  private apiUrl: string = environment.apiUri + 'User/';
  constructor(private http: HttpClient) { }

  getDepositeCodes(): Observable<Array<DepositeCode>> {
    return this.http.get(this.apiUrl  + 'DepositeCode')
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }
  
  useDepositCode(code :DepositeCode): Observable<any> {
    return this.http.get(this.apiUrl  + 'UseDepositeCode/' + code.UsingCode)
      .catch((error: string) => Observable.throw(error || 'Server error'));
  }

}
