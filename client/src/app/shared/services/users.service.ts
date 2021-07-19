import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { User } from '../models/user';
import { ServerConfig } from './server-config';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private readonly _apiURL = "users";
  constructor(private _http: HttpClient, private _serverConfig: ServerConfig) {}

  getUsers(): Observable<any> {
    return this._http
      .get<User[]>(
        this._serverConfig.getBaseUrl() + this._apiURL,
        this._serverConfig.getRequestOptions()
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  getUser(userId: string): Observable<any> {
    return this._http
      .get<User>(
        this._serverConfig.getBaseUrl() + this._apiURL + "/" + userId,         
        this._serverConfig.getRequestOptions()
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  addUser(user: User): Promise<any> {
    return this._http
      .post(
        this._serverConfig.getBaseUrl() + this._apiURL,
        user,
        this._serverConfig.getRequestOptions()
      )
      .toPromise();
  }

  updateUser(user: User) {
    return this._http
      .put(
        this._serverConfig.getBaseUrl() + this._apiURL + "/",
        user,
        this._serverConfig.getRequestOptions()
      )
      .toPromise();
  }

  deleteUser(user: User) {
    return this._http
      .delete<User>(
        this._serverConfig.getBaseUrl() +
          this._apiURL +
          "/" +
          user.userId,
        this._serverConfig.getRequestOptions()
      )
      .toPromise();
  }

  private handleError(error: any) {
    return Observable.throw(error);
  }
}
