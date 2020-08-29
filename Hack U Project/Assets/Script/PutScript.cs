using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PutScript : MonoBehaviour
{
    [SerializeField] GameObject watermellon;
    private bool putFin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //スイカが置けるのは最初の1回だけ
        if (!putFin)
        {
            Touch touch;
            if (Input.touchCount < 1 ||
                (touch = Input.GetTouch(0)).phase != TouchPhase.Began)//Began→タップ開始時
            {
                return;//画面に触れていないor既に触れている最中なら何もしない
            }

            //タップした座標にスイカを移動。対象は認識した平面
            TrackableHit hit;
            TrackableHitFlags filter = TrackableHitFlags.PlaneWithinPolygon;
            if (Frame.Raycast(touch.position.x, touch.position.y, filter, out hit))
            {
                //平面にヒット＆裏面でなければスイカを置く
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(Camera.main.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) > 0)
                {
                    //スイカの位置・姿勢を指定
                    watermellon.transform.position = hit.Pose.position;
                    watermellon.transform.rotation = hit.Pose.rotation;
                    watermellon.transform.Rotate(0, 0, 0, Space.Self);

                    //Anchorを設定
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    watermellon.transform.parent = anchor.transform;
                }
            }
        }
    }
}
