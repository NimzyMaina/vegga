import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators';
import 'rxjs/add/operator/map'


@Injectable()
export class VehicleService {

  private makesUrl = '/api/makes';
  private featuresUrl = 'api/features';
  private vehiclesUrl = 'api/vehicles';

  constructor(private http: Http) { }

  getMakes() {
    return this.http.get(this.makesUrl)
      .map(res => res.json());
  }

  getFeatures() {
    return this.http.get(this.featuresUrl)
    .map(res => res.json());
  }

  addVehicle(vehicle) {
    return this.http.post(this.vehiclesUrl,vehicle)
    .map(res => res.json());
        // .pipe(
        //   catchError(this.handleError)
        // );
  }

  // Implement a method to handle errors if any
  private handleError(err: HttpErrorResponse | any) {
    console.error('An error occurred', err);
    return err;//Observable.throw(err.message || err);
  }

}
