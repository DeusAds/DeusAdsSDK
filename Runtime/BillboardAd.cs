using UnityEngine;
using System.Collections;

public class BillboardAd : MonoBehaviour
{
    public string adTag;  // Unique tag to identify which ad to show on this billboard
    public string defaultLogoUrl;  // URL for the developer's default logo
    public Texture2D placeholderLogo;  // Placeholder logo set in the Inspector
    public string aspectRatio = "1:1";  // Desired aspect ratio as a string (e.g., "1:2", "4:3")

    private AdService adService;

    private void Start()
    {
        adService = FindObjectOfType<AdService>();

        // Check if AdService is available
        if (adService == null)
        {
            Debug.LogError("AdService not found in the scene.");
            LoadPlaceholderImage();  // Load placeholder if AdService is not found
            return;
        }

        // Check if the API key is valid
        if (!adService.IsApiKeyValid())
        {
            Debug.LogError("API Key is not valid. Cannot load ads.");
            LoadPlaceholderImage();  // Load placeholder if API key is invalid
            return;
        }

        // Attempt to load the ad image
        string imageUrl = adService.GetAdImageUrl(adTag);

        if (!string.IsNullOrEmpty(imageUrl))
        {
            StartCoroutine(ImageLoader.LoadImage(imageUrl, SetBillboardImage, LoadDefaultLogo));
        }
        else
        {
            LoadDefaultLogo();  // Load default logo if no ad image URL is available
        }
    }

private void SetBillboardImage(Texture2D texture)
{
    Renderer renderer = GetComponent<Renderer>();
    if (renderer != null && texture != null)
    {
        Debug.Log("Texture loaded successfully.");
        
        // Log aspect ratio before parsing
        Debug.Log($"Aspect Ratio input: '{string.Join(", ", aspectRatio.Trim().Split(':'))}'");
        
        if (!string.IsNullOrEmpty(aspectRatio))
        {
            // Parse aspect ratio and trim any whitespace
            string[] ratio = aspectRatio.Trim().Split(':');
            Debug.Log($"Parsed ratio: {string.Join(", ", ratio)}");

            if (ratio.Length == 2 &&
                float.TryParse(ratio[0], out float width) &&
                float.TryParse(ratio[1], out float height) &&
                width > 0 && height > 0) // Ensure both values are positive
            {
                // Calculate the aspect ratio value (width divided by height)
                float aspectRatioValue = width / height;

                // Set the texture as the main material texture
                renderer.material.mainTexture = texture;

                // Get the original object scale
                Vector3 originalScale = renderer.transform.localScale;

                // Adjust the object’s scale according to the aspect ratio
                // Maintain the object's width and adjust its height based on the aspect ratio
                float adjustedHeight = originalScale.x / aspectRatioValue;
                renderer.transform.localScale = new Vector3(originalScale.x, originalScale.y, adjustedHeight);

                Debug.Log($"Billboard {adTag} resized to new scale: {renderer.transform.localScale}");
            }
            else
            {
                Debug.LogError($"Invalid aspect ratio format. Parsed values: {string.Join(", ", ratio)}. Please use 'X:Y' format where X and Y are positive numbers.");
            }
        }
        else
        {
            Debug.LogError("Aspect ratio is null or empty.");
        }
    }
    else
    {
        Debug.LogError("Failed to load texture or renderer is null.");
    }
}






    private void LoadDefaultLogo()
    {
        if (!string.IsNullOrEmpty(defaultLogoUrl))
        {
            Debug.Log("Loading default logo from URL.");
            StartCoroutine(ImageLoader.LoadImage(defaultLogoUrl, SetBillboardImage, LoadPlaceholderImage));
        }
        else
        {
            LoadPlaceholderImage();  // Load placeholder if no default logo URL is provided
        }
    }

    private void LoadPlaceholderImage()
    {
        Debug.Log("Loading placeholder image.");
        
        // Use the placeholder logo set in the Inspector
        if (placeholderLogo != null)
        {
            SetBillboardImage(placeholderLogo);
        }
        else
        {
            // Optionally load a default placeholder from resources if none was provided
            Texture2D defaultPlaceholder = Resources.Load<Texture2D>("PlaceholderAd");
            SetBillboardImage(defaultPlaceholder);
        }
    }
}
