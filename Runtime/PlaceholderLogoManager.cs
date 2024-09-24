using UnityEngine;
using System.IO;

public class PlaceholderLogoManager : MonoBehaviour
{
    public string defaultLogoPath;  // Path where the developer can set their own logo
    public Texture2D placeholderLogo;  // A fallback placeholder logo

    public Texture2D GetDefaultLogo()
    {
        if (File.Exists(defaultLogoPath))
        {
            // Load the custom default logo from disk
            byte[] imageData = File.ReadAllBytes(defaultLogoPath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            return texture;
        }

        // If no custom logo, return the placeholder logo
        return placeholderLogo;
    }
}
