document.getElementById('uploadButton').addEventListener('click', async () => {
    const fileInput = document.getElementById('fileInput');
    if (fileInput.files.length === 0) {
        alert('Please select a file first.');
        return;
    }

    const loader = document.getElementById('loader');
    const resultsDiv = document.getElementById('results');
    const formData = new FormData();
    formData.append('file', fileInput.files[0]);

    loader.classList.remove('hidden');
    resultsDiv.classList.add('hidden');
    resultsDiv.innerHTML = '';

    try {
        const response = await fetch('/api/id/extract', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) throw new Error('Network response was not ok.');

        const data = await response.json();
        displayResults(data);
    } catch (error) {
        resultsDiv.innerHTML = `<p class="error">Error: ${error.message}</p>`;
    } finally {
        loader.classList.add('hidden');
        resultsDiv.classList.remove('hidden');
    }
});

function displayResults(data) {
    const resultsDiv = document.getElementById('results');

    resultsDiv.innerHTML = `
        <h2>Extracted Data</h2>
        <p><strong>First Name:</strong> ${data.firstName || 'N/A'}</p>
        <p><strong>Last Name:</strong> ${data.lastName || 'N/A'}</p>
        <p><strong>Document ID:</strong> ${data.documentId || 'N/A'}</p>
        <p><strong>Expiry Date:</strong> ${data.expiryDate || 'N/A'}</p>
        <hr>
        <h2>Verification Checks</h2>
        <p><strong>Face Detected in Photo:</strong> <span class="${data.isFaceDetected ? 'valid' : 'invalid'}">${data.isFaceDetected}</span></p>
        <p><strong>Card is not Expired:</strong> <span class="${data.isExpiryDateValid ? 'valid' : 'invalid'}">${data.isExpiryDateValid}</span></p>
        
        <details>
            <summary>Show Raw OCR Text</summary>
            <pre>${data.rawText || 'No text detected.'}</pre>
        </details>
    `;
}
