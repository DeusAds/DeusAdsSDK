using UnityEngine;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class AdService : MonoBehaviour
{
    public string apiKey;  // The API key provided to the client
    private bool apiKeyValidated = true; // Flag to track API key validation status
    private ConcurrentDictionary<string, string> adUrls = new ConcurrentDictionary<string, string>();
    private readonly object adUrlsLock = new object(); // Lock object for thread safety

    private void Start()
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API Key is required to use this service.");
            return;
        }

        StartCoroutine(ValidateApiKey(apiKey));

        // Mocked ad URLs
        adUrls["billboard1"] = "https://drive.google.com/uc?export=view&id=102Qs4Ii8GGjc4ba-UztmBgC0mUvqtlTZ";
        adUrls["billboard2"] = "https://drive.google.com/uc?export=view&id=1hK1Rnop6LVPkKgG0Vtj9PGUG2t6Cvyg5";
        adUrls["billboard3"] = "https://drive.google.com/uc?export=view&id=17ite1RM7b2oM8HfB_pSxvo4p2jtwZIo6";

        // Debug dictionary contents
        foreach (var kvp in adUrls)
        {
            Debug.Log($"Key: {kvp.Key}, URL: {kvp.Value}");
        }
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
    public string GetAdImageUrl(string adTag)
    {
        lock (adUrlsLock)
        {
            if (!apiKeyValidated)
            {
                Debug.LogError("API Key has not been validated. Cannot retrieve ad image URL.");
                return null;
            }
            else if (adTag == "billboard1")
            {
                return "https://drive.google.com/uc?export=view&id=102Qs4Ii8GGjc4ba-UztmBgC0mUvqtlTZ";
            }
            else if (adTag == "billboard2")
            {
                return "https://drive.google.com/uc?export=view&id=1hK1Rnop6LVPkKgG0Vtj9PGUG2t6Cvyg5";
            }
            else if (adTag == "billboard3")
            {
                return "https://drive.google.com/uc?export=view&id=17ite1RM7b2oM8HfB_pSxvo4p2jtwZIo6";
            }
            else
            {
                Debug.LogError("Ad URL not found for the given tag: " + adTag);
                return null;
            }
        }
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

    // Method to get the ad image URL (calls backend or returns a mock URL for MVP)
    public string GetAdImageUrl()
    {
        if (!apiKeyValidated)
        {
            Debug.LogError("API Key has not been validated. Cannot retrieve ad image URL.");
            return null;
        }

        // Normally, you would call a backend service here
        // For MVP, returning a mocked URL
        return "https://drive.google.com/uc?export=view&id=1OciTuTcEbI-DvHjqA06MXHYfXPQ6rd_-";
    }

    // Helper method to check if the API key is valid
    public bool IsApiKeyValid()
    {
        return apiKeyValidated;
    }
}
