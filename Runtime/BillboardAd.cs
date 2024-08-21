using UnityEngine;

public class BillboardAd : MonoBehaviour
{
    private AdService adService;
    private string imageUrl; // The URL of the image

    private void Start()
    {
        // Find the AdService in the scene
        adService = FindObjectOfType<AdService>();

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

        // Get the ad image URL
        imageUrl = adService.GetBillboardAdImageUrl();

        if (!string.IsNullOrEmpty(imageUrl))
        {
            StartCoroutine(ImageLoader.LoadImage(imageUrl, SetBillboardImage));
        }
        else
        {
            // Load a placeholder image if no URL is provided
            Texture2D placeholder = Resources.Load<Texture2D>("PlaceholderAd");
            SetBillboardImage(placeholder);
        }
    }

    private void SetBillboardImage(Texture2D texture)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && texture != null)
        {
            renderer.material.mainTexture = texture;
        }
    }
}

