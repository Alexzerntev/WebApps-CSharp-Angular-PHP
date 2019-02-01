import { Injectable } from '@angular/core';

import { environment } from '../../environments/environment'
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HomeDataService {
  private apiUrl: string = environment.apiUri + "Home/";

  constructor(private http: HttpClient) { }


}
