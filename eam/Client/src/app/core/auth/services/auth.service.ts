import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../../environments/environment';

import { LoginData, RegisterData } from '../auth.model';

import { ToastrService } from 'ngx-toastr';
import { LoaderService } from '../../loader/loader.service';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private apiUrl: string = environment.apiUri + "auth/";

  firstName: string;
  lastName: string;
  userId: string;
  roleId: string;
  departmentId: string;
  Year: number;
  phoneNumber: string;


  isLoggedIn: boolean;
  redirectUrl: string = "";

  jwtHelper = new JwtHelperService();

  constructor(
    private http: HttpClient,
    private router: Router,
    private loaderService: LoaderService,
    private toastrService: ToastrService
  ) {
    this.isLoggedIn = this.isAuthenticated();
    this.firstName = this.getFirstName();
    this.lastName = this.getLastName();
    this.userId = this.getId();
    this.departmentId = this.getDepartmentId();
    this.Year = this.getYear();
  }

  public login(loginData: LoginData) {
    this.loaderService.show();
    return this.http
      .post<any>(this.apiUrl + "login.php", JSON.stringify(loginData))
      .subscribe(data => {
        localStorage.setItem('access_token', data.jwt);
        this.setFirstName(data.jwt);
        this.setLastName(data.jwt);
        this.setUserId(data.jwt);
        this.setRoleId(data.jwt);
        this.setDepartmentId(data.jwt);
        this.setYear(data.jwt);
        this.setPhoneNumber(data.jwt);
        this.setEmail(data.jwt);
        this.isLoggedIn = true;
        this.loaderService.hide();
        if (this.redirectUrl != "") {
          this.router.navigate([this.redirectUrl]);
        }
        else {
          this.router.navigate(['/home']);
        }
      }, error => {
        this.toastrService.error("Η σύνδεση απέτυχε, Λάθως Όνομα Χρήστη ή Κωδικός", "Σφάλμα");
        this.loaderService.hide();
      }
      );
  };

  public register(registerData: RegisterData) {
    this.loaderService.show();
    return this.http
      .post<any>(this.apiUrl + "create_user.php", registerData)
      .subscribe(data => {
        let loginData: LoginData = new LoginData();
        loginData.email = registerData.email;
        loginData.password = registerData.password;
        this.login(loginData);
        this.toastrService.success("Συγχαρητήρια! Ο λογαριασμός σας δημιουργήθηκε με επιτυχια!", "Επιτυχία", { timeOut: 20000 });
      }, error => {
        this.toastrService.error(error.error, "Error");
        this.loaderService.hide();
      });
  }

  public logout() {
    this.isLoggedIn = false;
    if (localStorage.getItem("access_token")) {
      localStorage.removeItem("access_token");
    }
    this.router.navigate(['/home']);
  }

  public getToken(): string {
    return localStorage.getItem('access_token');
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token || token == undefined) {
      return false;
    }
    return true;
  }

  setFirstName(token: string) {
    this.firstName = this.jwtHelper.decodeToken(token).data.first_name;
  }

  setLastName(token: string) {
    this.lastName = this.jwtHelper.decodeToken(token).data.last_name;
  }

  setUserId(token: string) {
    this.userId = this.jwtHelper.decodeToken(token).data.id;
  }

  setRoleId(token: string) {
    this.userId = this.jwtHelper.decodeToken(token).data.role_id;
  }

  setDepartmentId(token: string) {
    this.userId = this.jwtHelper.decodeToken(token).data.department_id;
  }

  setYear(token: string) {
    this.userId = this.jwtHelper.decodeToken(token).data.year;
  }

  setPhoneNumber(token: string) {
    this.userId = this.jwtHelper.decodeToken(token).data.phone_number;
  }

  setEmail(token: string) {
    this.userId = this.jwtHelper.decodeToken(token).data.email;
  }

  //===========================================================================================F

  getFirstName(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.firstname;
    }
    this.logout();
  }

  getLastName(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.lastname;
    }
    this.logout();
  }

  getId(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.id;
    }
    this.logout();
  }

  getRoleId(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.role_id;
    }
    this.logout();
  }

  getDepartmentId(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.department_id;
    }
    this.logout();
  }

  getYear(): number {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.year;
    }
    this.logout();
  }

  getEmail(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.email;
    }
    this.logout();
  }

  getPhoneNumber(): string {
    if (this.isLoggedIn) {
      return this.jwtHelper.decodeToken(this.getToken()).data.phone_number;
    }
    this.logout();
  }

}
