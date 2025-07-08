namespace IDCapture.Models
{
    public class CardData
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DocumentId { get; set; }
        public string? DateOfBirth { get; set; }
        public string? ExpiryDate { get; set; }
        public bool IsExpiryDateValid { get; set; }
        public bool IsFaceDetected { get; set; }
        public string? RawText { get; set; } // To store the full OCR result for debugging
    }
}
