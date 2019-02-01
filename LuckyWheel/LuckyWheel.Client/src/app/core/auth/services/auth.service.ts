import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers } from '@angular/http';
import { JwtHelper } from 'angular2-jwt';
import 'rxjs/add/operator/map';

import { environment } from '../../../../environments/environment';

import { LoginData, RegisterData } from '../auth.model';

@Injectable()
export class AuthService {

  private apiUrl: string = environment.apiUri + "Account/";
  jwtHelper: JwtHelper = new JwtHelper();

  isLoggedIn: boolean;
  redirectUrl: string;


  constructor(private http: Http, private router: Router) {
    this.isLoggedIn = this.isAuthenticated();
  }



  register(registerData: RegisterData) {
    return this.http.post(this.apiUrl + "Register", registerData)
      .map(response => response.json());
  }


  login(loginData: LoginData) {
    return this.http.post(this.apiUrl + "Login", loginData)
      .map(response => response.json());
  }

  logout() {
    this.isLoggedIn = false;
    if(localStorage.getItem("access_token")){
      localStorage.removeItem("access_token");
    }
    if(localStorage.getItem("FirstName")){
      localStorage.removeItem("FirstName");
      localStorage.removeItem("LastName");
    }
    this.router.navigate(['/home']);
    
  }

  public getToken(): string {
    return localStorage.getItem('access_token');
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    if(!token){
      return false;
    }
    return !this.jwtHelper.isTokenExpired(token);
  }

}
