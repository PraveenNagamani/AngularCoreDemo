// import { Component } from '@angular/core';
import {Component, signal} from '@angular/core';
import { Validators } from '@angular/forms';
import { form, FormField } from '@angular/forms/signals';
import { PaymentDetail } from '../Shared/payment-detail';
import { CommonModule } from '@angular/common';

export interface ICardModal{
    cardNumber: string;
   cardType : string,
   nameOnCard : string,
    expiryDate: string;
    cvv: string;
}

export interface IPaymentModal{
    cardDetails : ICardModal;
    amount : string;
}


@Component({
  selector: 'app-payment-details',
  standalone: true,
  imports: [FormField ,CommonModule],
  templateUrl: './payment-details.html',
  styleUrls: ['./payment-details.css'],
})


export class PaymentDetails {
  constructor(private payapi : PaymentDetail) {}
 
  paymentModel = signal<IPaymentModal>({
      cardDetails: {
        cardNumber: '',
        cardType: '',
        nameOnCard: '',
        expiryDate: '',
        cvv: ''
    },
    amount: '0'
  });
  // Group them together if needed
  paymentobj = form(this.paymentModel);

   respobj = signal< IPaymentModal[] >([]);

   getpaymenthistory() {
    const data = this.paymentModel();
    console.log('Payment History requested for:', data.cardDetails.cardNumber);
    console.log('Full Form Data:', data);
    this.payapi.getPaymentDetails(data.cardDetails.cardNumber).subscribe({
      next : (resp) => {
         console.log('Response from API: ', resp);
         this.respobj.set(resp);
      },
      error : (err) => {
        console.log('Error from API: ', err);
      }
    });

  }

  sendpaymentdetails(){
    const data = this.paymentModel();
    try{
      
      this.payapi.SendPayment(data).subscribe({
        next : (resp) => {
          console.log('Response from API: ', resp);
        },
        error : (err) => {
          console.log('Error from API: ', err);
        }
      });
    }catch(errormsg){
      console.error(errormsg);
      
    }
     

  }

}
