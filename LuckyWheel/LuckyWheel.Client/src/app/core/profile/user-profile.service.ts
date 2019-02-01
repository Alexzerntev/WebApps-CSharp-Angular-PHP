import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { UserProfile } from './user-profile.model';
import { Spin } from '../../spin-game/spin-game.model';

import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/services/auth.service';


@Injectable()
export class UserProfileService {

  private apiUrl: string = environment.apiUri + 'User';
  userProfile: UserProfile = new UserProfile();

  constructor(private http: HttpClient, private authService: AuthService) {
    if (localStorage.getItem('FirstName')) {
      this.getProfileFromStorage();
    }
  }

  getProfile() {
    this.http.get(this.apiUrl + '/Profile')
      .map((userProfile: UserProfile) => userProfile)
      .catch((error: string) => Observable.throw(error || 'Server error'))
      .subscribe(result => {
        this.userProfile.Balance = result.Balance;
        this.userProfile.FirstName = result.FirstName;
        this.userProfile.LastName = result.LastName;
        localStorage.setItem('FirstName', result.FirstName);
        localStorage.setItem('LastName', result.LastName);
      });
  }

  getProfileFromStorage(){
    this.userProfile.FirstName = localStorage.getItem('FirstName');
    this.userProfile.LastName = localStorage.getItem('LastName');
    this.getBalance();
  }

  getBalance() {
    this.http.get(this.apiUrl + '/Balance')
      .map((response: Response) => response)
      .catch((error: string) => Observable.throw(error || 'Server error'))
      .subscribe(result => {
        this.userProfile.Balance = result;
      },
      err => {
        this.authService.logout();
      }
    );
  }
  
  getBalanceExposed() {
    return this.http.get(this.apiUrl + '/Balance')
      .map((response: Response) => response)
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  updateBalance(amountSigned: number) {
    this.http.post(this.apiUrl + '/Balance', amountSigned)
      .map((response: Response) => response)
      .catch((error: string) => Observable.throw(error || 'Server error'))
      .subscribe(result => {
        this.userProfile.Balance = result;
      });
  }

  insertSpin(spin: Spin){
    this.http.post(this.apiUrl + '/Spin', spin)
    .map((response: Response) => response)
    .catch((error: string) => Observable.throw(error || 'Server error'))
    .subscribe(result => {
      //this.userProfile.Balance = result;
    });
  }

}
