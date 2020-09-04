using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Trackingtest: MonoBehaviour
{
    [SerializeField] GameObject watermellon;//スイカObj
    [SerializeField] GameObject stick;      //棒Obj
    //検出されたマーカー番号（int）とGameObjectとの対応を記録する辞書
    Dictionary<int,GameObject> markerDic = new Dictionary<int, GameObject>();
    //フレーム毎に認識されたマーカーを一時的に覚えておく
    List<AugmentedImage> markers = new List<AugmentedImage>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //計測情報が更新されたマーカーを取得
        Session.GetTrackables<AugmentedImage>
                                    (markers, TrackableQueryFilter.Updated);
        //マーカーに対してCGとの対応付けor解除を行う
        foreach (var image in markers)
        {
            int index = image.DatabaseIndex;//マーカーの番号
            GameObject obj = null;  //上記indexに対応するCGを格納予定
            markerDic.TryGetValue(index, out obj);  //indexに対応するCGを取得
            if(obj == null && image.TrackingState == TrackingState.Tracking)    //まだ何も描写していないかつトラッキングしているという状態がある場合？
            {
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                switch(index)
                {
                    case 0://スイカマーカー（シシコ）
                        obj = GameObject.Instantiate(watermellon, anchor.transform);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        obj.transform.Rotate(0,0,0,Space.Self);
                        obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        markerDic.Add(index, obj);
                        break;
                    case 1://棒マーカー（ルカリオV）
                        obj = GameObject.Instantiate(stick, anchor.transform);
                        obj.transform.localPosition = new Vector3(0, 0, 0.15f);
                        obj.transform.Rotate(0, 0, 0, Space.Self);
                        obj.transform.localScale = new Vector3(0.05f,0.05f,0.8f);
                        markerDic.Add(index, obj);
                        break;
                }
            }
            else if(obj != null && image.TrackingState == TrackingState.Stopped)//描写中にマーカーのトラッキングが出来なくなった場合 
            {
                markerDic.Remove(index);
                Destroy(obj);
            }
        }
    }
}
