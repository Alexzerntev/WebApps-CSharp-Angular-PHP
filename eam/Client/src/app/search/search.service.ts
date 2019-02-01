import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../core/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private apiUrl: string = environment.apiUri;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  search(search_string: string) {
    search_string = search_string.toUpperCase();
    let toSend = {
      search_string: search_string
    };
    return this.http.post<any>(this.apiUrl + "search/" + "search.php", toSend);
  }
}
