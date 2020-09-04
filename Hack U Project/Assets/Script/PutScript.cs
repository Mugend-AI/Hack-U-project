using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using UnityEngine.UI;
using Photon.Pun;

public class PutScript : MonoBehaviour
{
    private bool putFin;
    private string cloudID;
    ARCoreOrigin arCoreOrigin;
    // Start is called before the first frame update
    void Start()
    {
        arCoreOrigin = GameObject.Find("ARCoreOrigin").GetComponent<ARCoreOrigin>();
       
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
                    /*
                    watermellon.transform.position = hit.Pose.position;
                    watermellon.transform.rotation = hit.Pose.rotation;
                    watermellon.transform.Rotate(0, 0, 0, Space.Self);
                    */
                    //Anchorを設定
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    GameObject.Find("Text").GetComponent<Text>().text = "接続中";
                    XPSession.CreateCloudAnchor(anchor).ThenAction(result =>
                    {
                        if (result.Response != CloudServiceResponse.Success)
                        {
                            Debug.Log(string.Format("Failed to host Cloud Anchor: {0}", result.Response));
                            GameObject.Find("Text").GetComponent<Text>().text = result.Response.ToString();
                            return;
                        }

                        Debug.Log(string.Format(
                            "Cloud Anchor {0} was created and saved.", result.Anchor.CloudId));
                        GameObject.Find("Text").GetComponent<Text>().text = "成功";
                        cloudID = result.Anchor.CloudId;
                        arCoreOrigin.SetWorldOrigin(result.Anchor.transform);
                        var manager = GameObject.Find("HostController").GetComponent<HostManager>();
                        manager.SetAnchorID(cloudID,result.Anchor);
                    });
                    //watermellon.transform.parent = anchor.transform;
                }
            }
        }
    }

    public string CloudID
    {
        get { return cloudID; }
    }
}
