using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentDetailsController : ControllerBase
{
    private readonly IPaymentRepository paymentRepository;
    private readonly ICardRepository cardRepository;
    public PaymentDetailsController(IPaymentRepository paymentRepository, ICardRepository cardRepository)
    {
        this.paymentRepository = paymentRepository;
        this.cardRepository = cardRepository;
    }
    [HttpPost]
    public async Task<IActionResult>  ProcessPayment(PaymentDetails paymentDetails)
    {
        // if (paymentDetails.valid)
        // {
            
        // }
       
        string errmsg=string.Empty;
        paymentRepository.Pay(paymentDetails,ref errmsg);
       
       CardDetails cardDetails =  paymentDetails.cardDetails;
       bool checkcaredavail =  cardRepository.GetCardDetails(cardDetails);
        if (!checkcaredavail)
        {
            //ErrMsg = $"Details Not Found for {cardNumber}"; 
            return new JsonResult(new { status = false });
        }
        return  new JsonResult(new { status = true });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentDetails(int cardNumber)
    {

        return new JsonResult( new {});
        
    }

  
}