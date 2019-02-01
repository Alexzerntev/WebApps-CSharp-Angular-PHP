import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../core/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class SecretaryHistoryService {

  private apiUrl: string = environment.apiUri;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  getHistory() {
    let toSend = {
      jwt: this.authService.getToken()
    };
    return this.http.post<any>(this.apiUrl + "lessons/" + "get_secretary_history.php", toSend);
  }

  deleteEntry(id) {
    let toSend = {
      id: id,
      jwt: this.authService.getToken()
    };
    return this.http.post<any>(this.apiUrl + "lessons/" + "delete_book_lesson.php", toSend);
  }

}
