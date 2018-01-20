import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { AppEnvironment } from '../../../app-environment';
import { User } from '../models/user.model';
import { BaseRequests } from '../../../base-requests';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class UsersService extends BaseRequests {
  constructor(private http: Http, private appEnvironment: AppEnvironment) {
    super();
  }

  getAllUsers(): Observable<User[]> {
    return this.http
      .get(this.appEnvironment.userApi.getAll(), this.getOptionsHeader())
      .map(result => result.json())
      .catch(this.handleError);
  }
}
