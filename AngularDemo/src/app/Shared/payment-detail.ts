import { Injectable , Inject, inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { IPaymentModal } from '../payment-details/payment-details';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class PaymentDetail {
  constructor() {}
  private http =  Inject(HttpClient) as HttpClient ;
  private httpresp = Inject(HttpResponse) as HttpResponse<IPaymentModal>;
  url: string = environment.baseurl;
  getPaymentDetails(cardNumber : string) : Observable<IPaymentModal[]> {
     
    //try{
      return this.http.get<IPaymentModal[]>(this.url + '/paymentdetails');
    // }catch{

    // }
    // return null;
  }

  SendPayment(payDetails : IPaymentModal ) : Observable<any> {
    return this.http.post<IPaymentModal[]>(this.url + '/paymentdetails',payDetails);
  }

}
