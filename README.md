# Smart ID Data Capture & Verification

This is a technical demonstration project built for exceet Card AG. It is a .NET 8 Web API with a simple web interface that showcases AI-powered data extraction and verification from identity documents.


## Features

- **Image Upload:** Users can upload an image of a mock ID card.
- **AI-Powered OCR:** Uses Azure AI Vision to perform Optical Character Recognition (OCR) on the image.
- **Automated Data Parsing:** Extracts key fields like Name, Document ID, and Expiry Date using Regular Expressions (Regex).
- **AI-Driven Verification:**
    - Checks for the presence of a human face in the photo area.
    - Validates if the card's expiry date is in the future, providing a clear pass/fail status.

---

## Technology Stack

- **Backend:** .NET 8 Web API (C#)
- **AI Service:** Azure AI Vision
- **Frontend:** HTML5, CSS3, Vanilla JavaScript

---

## How to Run This Project

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An Azure account with an active Azure AI Vision resource.

### 1. Configure Secrets

This project uses .NET User Secrets to securely store API keys and prevent them from being committed to source control.

First, navigate to the project directory in your terminal. Then, initialize the secrets and set your Azure credentials:

```bash
# Initialize user secrets for the project
dotnet user-secrets init

# Set your Azure endpoint URL
dotnet user-secrets set "VisionEndpoint" "YOUR_AZURE_ENDPOINT_URL"

# Set your Azure API Key
dotnet user-secrets set "VisionKey" "YOUR_AZURE_API_KEY"
```

### 2. Run the Application

Once the secrets are configured, you can run the application with a single command:

```bash
dotnet run
```
The application will be available at the `https://localhost:<port>` address shown in your terminal.
