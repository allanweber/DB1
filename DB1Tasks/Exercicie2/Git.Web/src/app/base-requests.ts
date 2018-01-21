import {
  Headers,
  Http,
  RequestOptions,
  RequestOptionsArgs,
  Response,
  ResponseContentType
} from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class BaseRequests {
  getHeaders(): Headers {
    const headers = new Headers();
    headers.append('User-Agent', 'Other');
    return headers;
  }

  getOptionsHeader(): RequestOptions {
    return new RequestOptions({ headers: this.getHeaders() });
  }

  handleError(error) {
    const errorObj = error.json();
    console.error('Ocorreu um erro', errorObj);
    alert(errorObj.Message);
    return Observable.throw(error);
  }
}
