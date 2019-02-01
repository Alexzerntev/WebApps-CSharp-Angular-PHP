import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { UserInfo } from './user.model';

import { environment } from '../../environments/environment';

@Injectable()
export class UserInfoService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = environment.apiUri + "User";

  getUserInfo() : Observable<UserInfo>{
    return this.http.get(this.apiUrl + "/UserInfo")
    .map((res: Response) => res)
    .catch((error: string) => Observable.throw(error || 'Server error'));
  }

  downloadPhoto(formData: FormData) : Observable<any>{
    return this.http.get(this.apiUrl + "/downloadPhoto")
    .map(res => res)
    .catch(error => Observable.throw(error));
  }

}
