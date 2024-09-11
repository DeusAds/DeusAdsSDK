using UnityEngine;
using System.Collections;

public class BillboardAd : MonoBehaviour
{
    public string adTag;  // A unique tag to identify which ad to show on this billboard
    public string defaultLogoUrl;  // URL for the developer's default logo
    private AdService adService;

    private void Start()
    {
        adService = FindObjectOfType<AdService>();

        if (adService == null)
        {
            Debug.LogError("AdService not found in the scene.");
            LoadDefaultLogo();
            return;
        }

        if (!adService.IsApiKeyValid())
        {
            Debug.LogError("API Key is not valid. Cannot load ads.");
            LoadDefaultLogo();
            return;
        }

        string imageUrl = adService.GetAdImageUrl(adTag);

        if (!string.IsNullOrEmpty(imageUrl))
        {
            StartCoroutine(ImageLoader.LoadImage(imageUrl, SetBillboardImage, LoadDefaultLogo));
        }
        else
        {
            LoadDefaultLogo();
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

    private void LoadDefaultLogo()
    {
        if (!string.IsNullOrEmpty(defaultLogoUrl))
        {
            Debug.Log("Loading default logo from URL.");
            StartCoroutine(ImageLoader.LoadImage(defaultLogoUrl, SetBillboardImage, LoadPlaceholderImage));
        }
        else
        {
            LoadPlaceholderImage();
        }
    }

    private void LoadPlaceholderImage()
    {
        Debug.Log("Loading placeholder image.");
        Texture2D placeholder = Resources.Load<Texture2D>("PlaceholderAd");
        SetBillboardImage(placeholder);
    }
}
