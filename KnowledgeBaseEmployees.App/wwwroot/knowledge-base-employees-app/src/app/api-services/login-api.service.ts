import { Injectable } from '@angular/core';
import { HttpClient, HttpParams , HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {User} from '../models/user.model';
import { Constants } from 'app/shared/constants';
import { SearchQuery } from 'app/models/search-query';
import { Headers } from "@angular/http/src/headers";
import { map } from 'rxjs/operators';
@Injectable()
export class LoginApiService {

  constructor(private http: HttpClient) { }
  
  login(login: User){
    console.log(`${Constants.baseApiUrl}users/authenticate`);
    /*let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': 'http://localhost:4200' });
    let options = { headers: headers };*/
    return this.http.post<any>(`${Constants.baseApiUrl}users/authenticate`, {username: login.username, password: login.password}/*, options*/)
      .pipe(map((user: User) => {
        if (user && user.token){
          localStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      }));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }
}