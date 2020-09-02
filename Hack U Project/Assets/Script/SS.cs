using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using SocialConnector;
using System;
using System.IO;

public class SS : MonoBehaviour
{
    public void ScreenShot()
    {
        StartCoroutine(TakeSS());
    }

    private IEnumerator TakeSS()
    {
        yield return new WaitForSeconds(2f);

        var w = Screen.width;
        var h = Screen.height;
        var ss = new Texture2D(w, h, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        ss.Apply();

        /*文字列のフォーマット指定*/
        String fileName = String.Format("image_{0:yyyyMMdd_Hmmss}.png", DateTime.Now);

        NativeGallery.SaveImageToGallery(ss, "ARMellon", fileName);

        //String imagePath = Application.persistentDataPath + "/ARMellon/" + fileName;
        //String imagePath = "/storage/emulated/0/DCIM/ARMellon/" + fileName;




        string imgPath = Application.persistentDataPath + "/image.png";

        // 前回のデータを削除
        File.Delete(imgPath);

        //スクリーンショットを撮影
        ScreenCapture.CaptureScreenshot("image.png");

        // 撮影画像の保存が完了するまで待機
        while (true)
        {
            if (File.Exists(imgPath)) break;
            yield return null;
        }

        
        //yield return new WaitForSeconds(2f);

        // 投稿する
        string tweetText = "てすと";
        string tweetURL = "";
        SocialConnector.SocialConnector.Share(tweetText, tweetURL, imgPath);



    }
}
