using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// スクリーンショットをキャプチャするサンプル
/// </summary>
public class ScreenShotAndro: MonoBehaviour
{
    String Ntime;  //String型文字列の変換用の変数



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
}
