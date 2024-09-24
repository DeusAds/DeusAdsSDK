using UnityEngine;
using System.Collections;

public class CarAd : MonoBehaviour
{
    private AdService adService;
    public string defaultLogoUrl;  // URL for the developer's default logo if no connection or ad fails to load
    public Texture2D placeholderLogo;  // Placeholder image if the server is unreachable
    public string aspectRatio = "1:1";  // Aspect ratio in 'X:Y' format

    private Vector3 originalScale;

    private void Start()
    {
        // Save the original scale of the car object for aspect ratio adjustment
        originalScale = transform.localScale;

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
        string imageUrl = adService.GetCarAdImageUrl();  // Ensure this method is implemented in AdService

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

            // Adjust the aspect ratio of the car ad
            AdjustAspectRatio();
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
        if (placeholderLogo != null)
        {
            SetCarAdImage(placeholderLogo);
        }
        else
        {
            Debug.LogError("Placeholder logo is not set.");
        }
    }

    private void AdjustAspectRatio()
    {
        // Parse the aspect ratio
        string[] ratio = aspectRatio.Trim().Split(':');
        if (ratio.Length == 2 && float.TryParse(ratio[0], out float width) && float.TryParse(ratio[1], out float height) && width > 0 && height > 0)
        {
            float adjustedHeight = originalScale.x * (height / width);
            // Apply the new scale, adjusting the Z (depth) to match the intended ratio
            transform.localScale = new Vector3(originalScale.x, originalScale.y, adjustedHeight);
        }
        else
        {
            Debug.LogError($"Invalid aspect ratio format: '{aspectRatio}'. Please use 'X:Y' format where X and Y are positive numbers.");
        }
    }
}
