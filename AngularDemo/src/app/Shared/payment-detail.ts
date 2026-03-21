import { Injectable , Inject, inject } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { IPaymentModal } from '../payment-details/payment-details';
import { throwError,catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PaymentDetail {
  constructor() {}
  private http =  inject(HttpClient) as HttpClient ;
  
  url: string = environment.baseurl;
  getPaymentDetails(cardNumber : string) : Observable<IPaymentModal[]> {
     
     const params = new HttpParams().set('CardNumber' , cardNumber);
     
      return this.http.get<IPaymentModal[]>(this.url + '/paymentdetails',{params});
    
  }

  SendPayment(payDetails : IPaymentModal ) : Observable<any> {
    console.log(JSON.stringify(payDetails));
    return this.http.post<any>(this.url + '/paymentdetails', payDetails).pipe(
    catchError((err) => {
      if (err.status === 400) {
        console.error('Validation error:', err.error);
      } else {
        console.error('Unexpected error:', err);
      }
      return throwError(() => err); // rethrow so caller can handle
    })
  );

  }

}
