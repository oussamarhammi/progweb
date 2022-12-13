import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if( request.url .match("https://localhost:5001/api/photos/.*")){
      request = request.clone({
        setHeaders:{
          'Authorization': 'Bearer ' + localStorage.getItem("authToken")
        }
       })
    }
  else if(request.url !="https://localhost:5001/api/Users/Register" && request.url != "https://localhost:5001/api/Users/Login")
    {
      request = request.clone({
        setHeaders:{
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage.getItem("authToken")
        }
       })
      }
     
    return next.handle(request);
  }
}
