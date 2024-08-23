using UnityEngine;

public class BillboardAd : MonoBehaviour
{
    public string adTag;  // A unique tag to identify which ad to show on this billboard
    private AdService adService;

    private void Start()
    {
        adService = FindObjectOfType<AdService>();

        if (adService == null)
        {
            Debug.LogError("AdService not found in the scene.");
            return;
        }

        if (!adService.IsApiKeyValid())
        {
            Debug.LogError("API Key is not valid. Cannot load ads.");
            return;
        }

        string imageUrl = adService.GetAdImageUrl(adTag);

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
