using UnityEngine;
using System.Collections;

public static class ImageLoader
{
    public static IEnumerator LoadImage(string url, System.Action<Texture2D> callback)
    {
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load image: " + www.error);
                callback(null);
            }
            else
            {
                Texture2D texture = ((UnityEngine.Networking.DownloadHandlerTexture)www.downloadHandler).texture;
                callback(texture);
            }
        }
    }
}
