import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { environment } from '../../environments/environment';


@Injectable()
export class SpinGameService {

  private apiUrl: string = environment.apiUri + 'Wheel';

  constructor(private http: HttpClient) { }

  getWheelSettings() {
    return this.http.get(this.apiUrl + "/Settings")
      .map((response: Response) => response);
  }

  getResult(playAmount: number) {
    console.log(playAmount);
    return this.http.post(this.apiUrl + "/SpinResult", playAmount)
      .map((response: Response) => response);
  }

  getMaxMultyplyer() {
    return this.http.get(this.apiUrl + "/Multyplyer")
      .map((response: number) => response);
  }
}
