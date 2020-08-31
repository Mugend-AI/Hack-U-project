using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocialConnector;

public class SSkinoko : MonoBehaviour
{
    private void Update()
    {
        
    }
    public void ScreenShot()
    {
        StartCoroutine(TakeSS());
    }

    private IEnumerator TakeSS()
    {
        yield return new WaitForEndOfFrame();

        var w = Screen.width;
        var h = Screen.height;
        var ss = new Texture2D(w, h, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        ss.Apply();

        NativeGallery.SaveImageToGallery(ss, "GalleryTest", "My img {0}.png");

        // 投稿する
        string tweetText = "ツイートテスト";
        string tweetURL = "";
        SocialConnector.SocialConnector.Share(tweetText, tweetURL);
    }
}
