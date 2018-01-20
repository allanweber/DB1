import {
  Headers, Http, RequestOptions, RequestOptionsArgs, Response, ResponseContentType,
} from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class BaseRequests {

  getHeaders(): Headers {
      const headers = new Headers();
      headers.append('Access-Control-Allow-Origin', '*');
      return headers;
  }

  getOptionsHeader(): RequestOptions {
      return new RequestOptions({ headers: this.getHeaders() });
  }

  handleError(error: any) {
      const erro = error.message || 'Server error';
      console.error('Ocorreu um erro', error);
      alert(error);
      return Observable.throw(erro);
  }
}
