export class ChangePassword {
  OldPassword: string;
  NewPassword: string;
}

import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/auth/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from '../core/loader/loader.service';
import { ProfileService } from './profile.service';
import { LoginData } from '../core/auth/auth.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  isChangingPassword = false;
  pass = "";
  canEdit = false;

  changePassword: ChangePassword = new ChangePassword();
  confirmPassword: string = "";


  constructor(
    public authService: AuthService,
    public toastrService: ToastrService,
    public loaderService: LoaderService,
    public profileService: ProfileService
  ) { }

  ngOnInit() {
  }

  validate() {
    if (this.isChangingPassword) {
      if (this.changePassword.NewPassword != this.confirmPassword) {
        this.toastrService.error("Οι κωδικοί διαφέρουν", "Σφάλμα");
        return;
      }
    }
    if (this.changePassword && this.pass == "") {
      this.toastrService.error("Πρέπει να πληκτρολογίσετε τον κωδικό σας για να κάνετε αλλαγές", "Σφάλμα");
    }
    let obj = {
      firstname: this.authService.getFirstName(),
      lastname: this.authService.getLastName(),
      email: this.authService.getEmail(),
      password: this.isChangingPassword == false ? this.pass : this.changePassword.NewPassword,
      id: this.authService.getId(),

    }
    this.profileService.update(obj).subscribe(
      result => {
        this.toastrService.success("Οι αλλαγές πραγματοποιήθηκαν με Επιτυχία", 'Επιτυχία');

        let loginData = new LoginData();
        loginData.email = this.authService.getEmail();
        loginData.password = this.isChangingPassword == false ? this.pass : this.changePassword.NewPassword;

        this.isChangingPassword = false;
        this.canEdit = false;
        this.loaderService.hide();


        this.authService.login(loginData);
      },
      error => {
        this.isChangingPassword = false;
        this.loaderService.hide();
        this.toastrService.error(error.error, 'Error');
      }
    );
  }

}

