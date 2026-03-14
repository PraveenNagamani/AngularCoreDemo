// import { Component } from '@angular/core';
import {Component, signal} from '@angular/core';
import { Validators } from '@angular/forms';
import { form, FormField } from '@angular/forms/signals';
import { PaymentDetail } from '../Shared/payment-detail';


export interface IPaymentModal{
   cardNumber: string;
    expiryDate: string;
    cvv: string;
}


@Component({
  selector: 'app-payment-details',
  standalone: true,
  imports: [FormField],
  templateUrl: './payment-details.html',
  styleUrls: ['./payment-details.css'],
})


export class PaymentDetails {
  constructor(private payapi : PaymentDetail) {}
 
  paymentModel = signal<IPaymentModal>({
    cardNumber: '',
    expiryDate: '',
    cvv: ''
  });
  // Group them together if needed
  paymentobj = form(this.paymentModel);

   getpaymenthistory() {
    const data = this.paymentModel();
    console.log('Payment History requested for:', data.cardNumber);
    console.log('Full Form Data:', data);
    this.payapi.getPaymentDetails(data.cardNumber).subscribe({
      next : (resp) => {
         console.log('Response from API: ', resp);
      },
      error : (err) => {
        console.log('Error from API: ', err);
      }
    });
  }

  sendpaymentdetails(){
    const data = this.paymentModel();
    this.payapi.SendPayment(data).subscribe({
      next : (resp) => {
         console.log('Response from API: ', resp);
      },
      error : (err) => {
        console.log('Error from API: ', err);
      }
    });; 

  }

}
