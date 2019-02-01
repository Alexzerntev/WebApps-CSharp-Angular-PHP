import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../core/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationHistoryService {

  private apiUrl: string = environment.apiUri;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  getHistory() {
    let toSend = {
      jwt: this.authService.getToken()
    };
    return this.http.post<any>(this.apiUrl + "applications/" + "get_history.php", toSend);
  }

}
