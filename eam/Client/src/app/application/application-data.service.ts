import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../core/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationDataService {

  private apiUrl: string = environment.apiUri;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  getLessons(department_id) {
    return this.http.post<any>(this.apiUrl + "lessons/" + "get_lessons.php", department_id);
  }

  postApplication(obj) {
    obj.jwt = this.authService.getToken();
    return this.http.post<any>(this.apiUrl + "applications/" + "add_application.php", obj);
  }

}
