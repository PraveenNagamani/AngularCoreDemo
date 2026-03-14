using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

public interface ICardRepository
{
    public bool AddCardDetails(CardDetails cardDetails);
    public bool GetCardDetails(CardDetails cardDetails);
    public CardDetails GetCardDetails(string CardNumber);
}
public class CardDetails
{
    [Required]
    [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be exactly 16 digits.")]
    public string CardNumber { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Card type must be either 'Debit Card' or 'Credit Card'")]
    public  string CardType { get; set;} 
    [Required]
    [StringLength(50, ErrorMessage = "Name on card must be between 1 and 50 characters", MinimumLength = 1)]
    public string NameonCard { get; set; }
    [Required]
    public string ExpiryDate { get; set; }
    [Required]
    public int CVV { get; set; }

}

public class cardRepository : ICardRepository
{
    private readonly ILogger<cardRepository> _logger; 
    public cardRepository(ILogger<cardRepository> logger)
    { 
        _logger = logger; 
    }

    public Dictionary<string,CardDetails> cardDictionary = new Dictionary<string,CardDetails> ();

    public bool AddCardDetails(CardDetails cardDetails)
    {
        
        cardDictionary.Add(cardDetails.CardNumber, cardDetails);
        return true;
    }

    public bool  GetCardDetails(CardDetails cardDetails)
    {
        return cardDictionary.ContainsKey(cardDetails.CardNumber);
        
    }

    public CardDetails GetCardDetails(string CardNumber)
    {
        return cardDictionary.ContainsKey(CardNumber) ? cardDictionary[CardNumber] : null;
        
    }

}

