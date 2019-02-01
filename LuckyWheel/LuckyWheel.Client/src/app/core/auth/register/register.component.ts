import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';
import { UserProfileService } from '../../profile/user-profile.service'
import { ToasterService } from 'angular2-toaster';
import { LoaderService } from '../../loader/loader.service';

import { RegisterData } from "../auth.model"

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerData: RegisterData = new RegisterData();
  repeatPassword: string;
  constructor(private authService: AuthService,
    private userProfileService: UserProfileService,
    private router: Router,
    private loaderService: LoaderService,
    private toasterService: ToasterService
  ) { }

  ngOnInit() {
  }

  register() {
    this.loaderService.show();
    this.authService.register(this.registerData).subscribe(result => {
      localStorage.setItem("access_token", result.access_token);
      this.router.navigate(['/home']);
      this.authService.isLoggedIn = true;
      this.userProfileService.getProfile();
      this.loaderService.hide();
    },
      error => {
        this.toasterService.pop("error", "Σφάλμα", error);
        this.loaderService.hide();
      }
    );;
  }

  validate() {
    if (!this.registerData.Email ||
      !this.registerData.FirstName ||
      !this.registerData.LastName ||
      !this.registerData.Password) {
      this.toasterService.pop("error", "Σφάλμα", "Όλα τα παιδία είναι υποχρεωτικά");
      return;
    }
    if (this.registerData.Password != this.repeatPassword) {
      this.toasterService.pop("error", "Σφάλμα", "Οι κωδικοί διαφέρουν");
      return;
    }
    this.register();
  }
}
