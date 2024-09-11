using UnityEngine;
using System.Collections;

public class CarAd : MonoBehaviour
{
    private AdService adService;
    public string defaultLogoUrl; // URL for the developer's default logo if no connection or ad fails to load

    private void Start()
    {
        // Find the AdService in the scene
        adService = FindObjectOfType<AdService>();

        if (adService == null)
        {
            Debug.LogError("AdService not found in the scene.");
            LoadDefaultLogo();
            return;
        }

        // Check if the API key is valid
        if (!adService.IsApiKeyValid())
        {
            Debug.LogError("API Key is not valid. Cannot load ads.");
            LoadDefaultLogo();
            return;
        }

        // Get the ad image URL for the car
        string imageUrl = adService.GetCarAdImageUrl(); // Ensure this method is implemented in AdService

        if (!string.IsNullOrEmpty(imageUrl))
        {
            // Load car ad image, and on failure, load the default logo
            StartCoroutine(ImageLoader.LoadImage(imageUrl, SetCarAdImage, LoadDefaultLogo));
        }
        else
        {
            LoadDefaultLogo();
        }
    }

    private void SetCarAdImage(Texture2D texture)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && texture != null)
        {
            renderer.material.mainTexture = texture;
        }
    }

    private void LoadDefaultLogo()
    {
        if (!string.IsNullOrEmpty(defaultLogoUrl))
        {
            Debug.Log("Loading default logo for car ad from URL.");
            StartCoroutine(ImageLoader.LoadImage(defaultLogoUrl, SetCarAdImage, LoadPlaceholderImage));
        }
        else
        {
            LoadPlaceholderImage();
        }
    }

    private void LoadPlaceholderImage()
    {
        Debug.Log("Loading placeholder image for car ad.");
        Texture2D placeholder = Resources.Load<Texture2D>("PlaceholderCarAd");
        SetCarAdImage(placeholder);
    }
}

