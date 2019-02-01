import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../core/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class LessonsDataService {

  private apiUrl: string = environment.apiUri;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }


  search(search_string: string) {
    search_string = search_string.toUpperCase();
    let toSend = {
      search_string: search_string,
      jwt: this.authService.getToken()
    };
    return this.http.post<any>(this.apiUrl + "lessons/" + "get_secretary_lessons.php", toSend);
  }

  addRelation(obj) {
    let toSend = {
      data: obj,
      jwt: this.authService.getToken()
    };
    return this.http.post<any>(this.apiUrl + "lessons/" + "add_lesson_book.php", toSend);
  }

}
