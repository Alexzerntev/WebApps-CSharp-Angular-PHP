import { Component, OnInit } from '@angular/core';

import { AuthService } from '../auth/services/auth.service';

import { LoginData } from '../auth/auth.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginData = new LoginData();

  constructor(public authService: AuthService) { }

  ngOnInit() {
    this.loginData.email="";
    this.loginData.password="";
  }

}