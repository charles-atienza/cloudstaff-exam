import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

var defaultHeader = {
  'Content-Type': 'application/json; charset=UTF-8'
}

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  constructor(private http: HttpClient) { }
  getUsers(): Observable<any> {
    return this.http.get("https://localhost:44375/api/User/GetUsers");
  }

  login(pin: string, password: string): Observable<any> {
    let body = {
      pin: pin,
      password: password
    }

    return this.http.post("https://localhost:44375/api/User/Login", body, { headers: defaultHeader });
  }

  creditBalance(userId: string, amount: number): Observable<any> {
    let body = {
      userId,
      amount,
    }

    return this.http.post("https://localhost:44375/api/User/CreditBalance", body, { headers: defaultHeader });
  }
  debitBalance(userId: string, amount: number): Observable<any> {
    let body = {
      userId,
      amount,
    }

    return this.http.post("https://localhost:44375/api/User/DebitBalance", body, { headers: defaultHeader });
  }
}
