import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import 'rxjs/add/operator/do';
import { Observable } from 'rxjs/Observable';

import { AuthService } from './auth.service';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(public auth: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.auth.getToken()}`
      }
    });

    return next.handle(request).do((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        
      }
    }, (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 401) {
          this.auth.logout();          
        }
      }
    });
  }
}
