using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using SocialConnector;
using System;
using System.IO;

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

        /*文字列のフォーマット指定*/
        String fileName = String.Format("image_{0:yyyyMMdd_Hmmss}.png", DateTime.Now);

        NativeGallery.SaveImageToGallery(ss, "ARMellon", fileName);

        //String imagePath = Application.persistentDataPath + "/ARMellon/" + fileName;
        String imagePath = "/storage/emulated/0/DCIM/ARMellon/" + fileName;

        // 投稿する
        string tweetText = "ツイートテスト";
        string tweetURL = "";
        SocialConnector.SocialConnector.Share(tweetText, tweetURL,imagePath);
    }
}
