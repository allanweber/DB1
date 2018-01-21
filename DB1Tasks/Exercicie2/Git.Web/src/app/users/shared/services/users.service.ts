import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { User } from '../models/user.model';
import { UserDetail } from '../models/user-detail.model';
import { BaseRequests } from '../../../base-requests';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class UsersService extends BaseRequests {

  private baseUrl: string;

  constructor(private http: Http) {
    super();
    this.baseUrl = 'https://api.github.com/';
  }

  getAllUsers(): Observable<User[]> {
    return this.http
      .get(`${this.baseUrl}users`, this.getOptionsHeader())
      .map(result => result.json())
      .catch(this.handleError);
  }

  getUser(userName: string): Observable<UserDetail> {
    return this.http
      .get(`${this.baseUrl}users/${userName}`, this.getOptionsHeader())
      .map(result => result.json())
      .catch(this.handleError);
  }
}
