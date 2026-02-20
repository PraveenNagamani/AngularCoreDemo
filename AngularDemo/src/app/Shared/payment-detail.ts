import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PaymentDetail {
  constructor(private http: HttpClient) {}
  url: string = environment.baseurl;
  getPaymentDetails() {
    return this.http.get(this.url + '/api/paymentdetails');
  }
}
