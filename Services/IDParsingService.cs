using System.Text.RegularExpressions;
using IDCapture.Models;

public class IDParsingService
{
    public CardData ParseAndVerify(string rawText, bool hasPeople)
    {
        var cardData = new CardData { RawText = rawText, IsFaceDetected = hasPeople };

        // Regex to handle multiple possible labels using the OR | operator
        cardData.LastName = FindValue(rawText, @"(?:LAST NAME|SURNAME):?\s*(\w+)");
        cardData.FirstName = FindValue(rawText, @"(?:FIRST NAME|GIVEN NAME):?\s*(\w+)");
        cardData.DocumentId = FindValue(rawText, @"(?:ID NUMBER|DOC NO):?\s*([A-Z0-9]+)");
        cardData.ExpiryDate = FindValue(rawText, @"(?:EXPIRY DATE|DATE OF EXPIRY):?\s*(\d{2}/\d{2}/\d{4})");

        // Verification Logic
        if (!string.IsNullOrEmpty(cardData.ExpiryDate) && DateTime.TryParse(cardData.ExpiryDate, out DateTime expiry))
        {
            cardData.IsExpiryDateValid = expiry > DateTime.Now;
        }
        else
        {
            cardData.IsExpiryDateValid = false;
        }

        return cardData;
    }


    private string? FindValue(string text, string pattern)
    {
        var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : null;
    }
}