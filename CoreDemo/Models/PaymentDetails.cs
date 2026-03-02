
using System.ComponentModel.DataAnnotations;

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
    
    public CardDetails? cardDetails { get; set; } 

    [Required]
    public decimal Amount { get; set; }

}
 public class PayRepository : IPaymentRepository
 {
     private readonly ICardRepository _cardRepository;
     Dictionary<int,PaymentDetails> Dictpaymentdetails = new Dictionary<int, PaymentDetails>();

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
       int cn = cardDetails1.CardNumber;
    bool checkcaredavail =  _cardRepository.GetCardDetails(cardDetails1);
        if (!checkcaredavail)
        {
            ErrMsg = $"Details Not Found for {cn}"; 
            return false;
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