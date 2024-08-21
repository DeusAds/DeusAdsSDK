using UnityEngine;
using System.Collections;

public class AdService : MonoBehaviour
{
    public string apiKey;  // The API key provided to the client

    private bool apiKeyValidated = true; // Flag to track API key validation status

    private void Start()
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API Key is required to use this service.");
            return;
        }

        StartCoroutine(ValidateApiKey(apiKey));
    }

    // Method to validate the API key
    private IEnumerator ValidateApiKey(string key)
    {
        string validationUrl = $"https://your-backend.com/validate?apiKey={key}";
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(validationUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Invalid API Key: " + www.error);
                apiKeyValidated = false;
            }
            else
            {
                apiKeyValidated = www.downloadHandler.text == "true";
                if (apiKeyValidated)
                {
                    Debug.Log("API Key validated successfully.");
                }
                else
                {
                    Debug.LogError("API Key validation failed.");
                }
            }
        }
    }

    // Method to get the ad image URL for billboards
    public string GetBillboardAdImageUrl()
    {
        if (!apiKeyValidated)
        {
            Debug.LogError("API Key has not been validated. Cannot retrieve billboard ad image URL.");
            return null;
        }

        // Normally, you would call a backend service here
        // For MVP, returning a mocked URL
        return "https://fastly.picsum.photos/id/1/5000/3333.jpg?hmac=Asv2DU3rA_5D1xSe22xZK47WEAN0wjWeFOhzd13ujW4";
    }

    // Method to get the ad image URL for cars
    public string GetCarAdImageUrl()
    {
        if (!apiKeyValidated)
        {
            Debug.LogError("API Key has not been validated. Cannot retrieve car ad image URL.");
            return null;
        }

        // Normally, you would call a backend service here
        // For MVP, returning a mocked URL for a car ad
        return "https://drive.google.com/uc?export=view&id=1OciTuTcEbI-DvHjqA06MXHYfXPQ6rd_-";
    }

    // Helper method to check if the API key is valid
    public bool IsApiKeyValid()
    {
        return apiKeyValidated;
    }
}
