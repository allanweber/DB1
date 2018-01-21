import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Repository } from '../models/repository.model';
import { BaseRequests } from '../../../base-requests';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class RepositoriesService extends BaseRequests {

  private baseUrl: string;

  constructor(private http: Http) {
    super();
    this.baseUrl = 'https://api.github.com/';
  }

  getUserRepositories(userName: string): Observable<Repository[]> {
    return this.http
      .get(`${this.baseUrl}users/${userName}/repos`, this.getOptionsHeader())
      .map(result => result.json())
      .catch(this.handleError);
  }

}
