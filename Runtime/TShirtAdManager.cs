using UnityEngine;
using System.Collections;

public class TShirtAdManager : MonoBehaviour
{
    public string logoUrl; // The URL of the logo image from the SDK
    private Renderer tShirtRenderer;

    private void Start()
    {
        // Find the Renderer component
        tShirtRenderer = GetComponent<Renderer>();

        if (tShirtRenderer == null)
        {
            Debug.LogError("T-shirt Renderer is missing.");
            return;
        }

        // Get the AdService component
        AdService adService = FindObjectOfType<AdService>();

        if (adService == null)
        {
            Debug.LogError("AdService not found in the scene.");
            return;
        }

        // Check if the API key is valid
        if (!adService.IsApiKeyValid())
        {
            Debug.LogError("API Key is not valid. Cannot load ads.");
            return;
        }

        // Get the logo URL for the T-shirt
        logoUrl = adService.GetAdImageUrl();

        if (!string.IsNullOrEmpty(logoUrl))
        {
            StartCoroutine(LoadLogoTexture(logoUrl));
        }
        else
        {
            Debug.LogError("Logo URL is empty.");
        }
    }

    private IEnumerator LoadLogoTexture(string url)
    {
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load texture: " + www.error);
            }
            else
            {
                Texture2D texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(www);

                // Apply the texture to the T-shirt
                if (texture != null)
                {
                    tShirtRenderer.material.mainTexture = texture;

                    // Adjust tiling and offset for placement
                    tShirtRenderer.material.mainTextureScale = new Vector2(0.005f, 0.005f); // Scale down to fit the front
                    tShirtRenderer.material.mainTextureOffset = new Vector2(0.5f, 0.5f); // Position to the center front
                }
            }
        }
    }
}
