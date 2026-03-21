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
    public async Task<IActionResult>  ProcessPayment([FromBody] PaymentDetails paymentDetails)
    {
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
       
        string errmsg=string.Empty;
        paymentRepository.Pay(paymentDetails,ref errmsg);

        if (!string.IsNullOrEmpty(errmsg)) { return new JsonResult(new { status = false, error = errmsg }); }
       
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
    public async Task<ActionResult<List<PaymentDetails>>> GetPaymentDetails(string cardNumber)
    {
        CardDetails _cardDetails =  cardRepository.GetCardDetails(cardNumber);
        var paymentDetails =  paymentRepository.GetPaymentDetails(_cardDetails);
       //return new JsonResult( new { paymentDetails });
       return Ok(paymentDetails);        
    }

  
}