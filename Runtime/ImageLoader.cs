using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ImageLoader
{
    private static readonly Dictionary<string, Texture2D> imageCache = new Dictionary<string, Texture2D>();

    public static IEnumerator LoadImage(string url, System.Action<Texture2D> onSuccess, System.Action onFailure)
    {
        if (imageCache.TryGetValue(url, out Texture2D cachedTexture))
        {
            // If the image is in the cache, return it immediately
            onSuccess?.Invoke(cachedTexture);
            yield break;
        }

        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load image: " + www.error);
                onFailure?.Invoke();  // Call the fallback method if loading fails
            }
            else
            {
                Texture2D texture = ((UnityEngine.Networking.DownloadHandlerTexture)www.downloadHandler).texture;

                // Add the loaded texture to the cache
                imageCache[url] = texture;

                onSuccess?.Invoke(texture);  // Call the success callback
            }
        }
    }

    public static void ClearCache()
    {
        imageCache.Clear();
    }
}
