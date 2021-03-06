﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using SocialConnector;
using System;
using System.IO;

/// <summary>
/// スクリーンショットをキャプチャするサンプル
/// </summary>
public class ScreenShotAndro: MonoBehaviour
{
    [SerializeField] Text pathtext;
    String Ntime;  //String型文字列の変換用の変数


    public void Share() {
        Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
        Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        StartCoroutine(_Share());
    }


    public IEnumerator _Share()
    {
        

        /*文字列のフォーマット指定*/
        String fileName = String.Format("image_{0:yyyyMMdd_Hmmss}.png", DateTime.Now);

        /*unityが利用するデータが保存されるパス*/
        /*Andoridの場合…/data/app/xxx.xxx.xxx.apk*/
        /*Andoridで永続性のあるデータを保存するには
          /storage/emulated/0/android/data/{アプリのパッケージ名}/files/任意のファイル名*/

        String imagePath = Application.persistentDataPath + "/" + fileName;

        /*スクリーンショットの保存*/
        ScreenCapture.CaptureScreenshot(imagePath);

        pathtext.text = imagePath;
        Debug.Log(imagePath);

        // 投稿する
        string tweetText = "ツイートテスト";
        string tweetURL = "";
        SocialConnector.SocialConnector.Share(tweetText, tweetURL, imagePath);


        /*再開条件にした関数がtrueを返すと再開する*/
        /*再開条件…imagePathが通ればture*/
        yield return new WaitUntil(() => File.Exists(imagePath));

    }






    /*
    private void Update()
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space))
        {

            
                
                //現在時刻を取得
                DateTime presentTime = DateTime.Now;

                //DateTimeで取得した時刻をString型の文字列に変換
                Ntime = presentTime.ToString("yyyy-MM-dd-HH-mm-ss");

                // スクリーンショットを保存
                CaptureScreenShot("Assets/Screenshot/" + Ntime + ".png");


            

        }
    }






    
    // 画面全体のスクリーンショットを保存する
    private void CaptureScreenShot(string filePath)
    {

        IEnumerator CeateScreenShot()
        {

            ScreenCapture.CaptureScreenshot(Application.persistentDataPath+filePath);

            while (File.Exists(filePath) == false) {

                yield return null;
            
            }

        }
    }
    */

}
