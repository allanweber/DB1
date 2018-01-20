import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { AppEnvironment } from '../../../app-environment';
import { Repository } from '../models/repository.model';
import { BaseRequests } from '../../../base-requests';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class RepositoriesService extends BaseRequests {
  constructor(private http: Http, private appEnvironment: AppEnvironment) {
    super();
  }

  getUserRepositories(userName: string): Observable<Repository[]> {
    return this.http
      .get(this.appEnvironment.repoApi.getByUser(userName), this.getOptionsHeader())
      .map(result => result.json())
      .catch(this.handleError);
  }

}
