import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';
import { UserProfileService } from '../../profile/user-profile.service';
import { LoaderService } from '../../loader/loader.service';
import { ToasterService } from 'angular2-toaster';


import { LoginData } from '../auth.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginData: LoginData = new LoginData();

  constructor(
    private authService: AuthService, 
    private router: Router, 
    private userProfileService: UserProfileService,
    private loaderService: LoaderService,
    private toasterService: ToasterService
  ) { }

  ngOnInit() {
  }

  login() {
    this.loaderService.show();
    this.authService.login(this.loginData).subscribe(result => {
      localStorage.setItem('access_token', result.access_token);
      this.router.navigate(['/home']);
      console.log(result);
      this.authService.isLoggedIn = true;
      this.userProfileService.getProfile();
      this.loaderService.hide();
    },
    error => {
      this.toasterService.pop("error", "Σφάλμα", error);
      this.loaderService.hide();
    }
    );
  }

}
