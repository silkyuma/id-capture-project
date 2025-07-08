using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Exceet.IDCapture.Controllers;

[ApiController]
[Route("api/id")] // <--- CHECK THIS LINE CAREFULLY
public class IDCaptureController : ControllerBase
{
    private readonly ImageAnalysisClient _client;
    private readonly IDParsingService _parsingService; // Make sure this is here

    // Make sure your constructor looks like this, asking for both services
    public IDCaptureController(IConfiguration configuration, IDParsingService parsingService)
    {
        string endpoint = configuration["VisionEndpoint"]!;
        string key = configuration["VisionKey"]!;
        _client = new ImageAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));
        _parsingService = parsingService; // Assign the service
    }

    [HttpPost("extract")] // <--- CHECK THIS LINE CAREFULLY
    public async Task<IActionResult> ExtractData(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        var binaryData = await BinaryData.FromStreamAsync(stream);

        VisualFeatures features = VisualFeatures.Read | VisualFeatures.People;

        ImageAnalysisResult result = await _client.AnalyzeAsync(binaryData, features);

        string extractedText = "";
        if (result.Read != null)
        {
            extractedText = string.Join("\n", result.Read.Blocks.SelectMany(block => block.Lines).Select(line => line.Text));
        }

        bool hasPeople = result.People?.Values.Any() ?? false;

        // Use the parsing service
        var cardData = _parsingService.ParseAndVerify(extractedText, hasPeople);

        return Ok(cardData);
    }
}