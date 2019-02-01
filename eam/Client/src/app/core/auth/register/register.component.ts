import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

import { RegisterData } from "../auth.model"

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerData: RegisterData = new RegisterData();
  repeatPassword: string;
  roles = ["Φοιτητής", "Γραμματεία"];

  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
    if (this.authService.isLoggedIn) {
      this.router.navigate([`/home`]);
    }
  }

  validate() {
    if (!this.registerData.email ||
      !this.registerData.firstname ||
      !this.registerData.lastname ||
      !this.registerData.password) {
      this.toastrService.error("Όλα τα παιδία είναι υποχρεωτικά", "Σφάλμα");
      return;
    }
    if (this.registerData.password != this.repeatPassword) {
      this.toastrService.error("Οι κωδικοί διαφέρουν", "Σφάλμα");
      return;
    }
    this.authService.register(this.registerData);
  }

}
