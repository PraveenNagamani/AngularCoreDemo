
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;


public interface IPaymentDetails
{
    
    CardDetails? cardDetails { get; set; }
     decimal Amount { get; set; }
}

public interface IPaymentRepository
{
    public bool Pay(PaymentDetails paymentDetails, ref string ErrMsg);
    public List<PaymentDetails> GetPaymentDetails(CardDetails cardDetails);

}
public class PaymentDetails : IPaymentDetails
{
    [Required]
   
    public CardDetails? cardDetails { get; set; } 

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

}
 public class PayRepository : IPaymentRepository
 {
     private readonly ICardRepository _cardRepository;
     Dictionary<string,PaymentDetails> Dictpaymentdetails = new Dictionary<string, PaymentDetails>();

     public PayRepository(ICardRepository cardRepository)
     {
         this._cardRepository = cardRepository;
     }
    public bool Pay(PaymentDetails paymentDetails, ref string ErrMsg)
    {
        
        CardDetails? cardDetails1 = paymentDetails.cardDetails;
        if (cardDetails1 == null)
        {
            ErrMsg = "Card Details are Mandatory" ; return false;
        }      
       string cn = cardDetails1.CardNumber;
        bool checkcaredavail =  _cardRepository.GetCardDetails(cardDetails1);
        if (!checkcaredavail)
        {
          _cardRepository.AddCardDetails(cardDetails1);
        }
        
         Dictpaymentdetails.Add(cn, paymentDetails);        
        return true;

    }

    
    public List<PaymentDetails> GetPaymentDetails(CardDetails cardDetails)
    {      
        if (Dictpaymentdetails.TryGetValue(cardDetails.CardNumber, out var paymentDetail))
        {
            return new List<PaymentDetails> { paymentDetail };
        }
        return new List<PaymentDetails>();
        //return  Dictpaymentdetails.Keys(cardNumber).values.ToList();        
        
    }

  
}