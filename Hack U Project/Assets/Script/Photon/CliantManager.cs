using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using UnityEngine.UI;

public class CliantManager : MonoBehaviourPunCallbacks
{
    string cloudID;
    XPAnchor anchor;
    ARCoreOrigin arCoreOrigin;

    [SerializeField] GameObject stickPrefab;
    GameObject stick;
    Text text;
    // Start is called before the first frame update
    private void Start()
    {
        //PhotonServerSettingsに設定した内容を使ってサーバに接続する
        PhotonNetwork.ConnectUsingSettings();
        arCoreOrigin = GameObject.Find("ARCoreOrigin").GetComponent<ARCoreOrigin>();
        text = GameObject.Find("Text").GetComponent<Text>();
    }

    //マスターサーバへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //roomという名前のルームに参加する。ルームがなければ作成してから参加する
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //マッチングが成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //マッチング後、ランダムな位置に自分自身のネットワークオブジェクトを生成する
        var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        stick = PhotonNetwork.Instantiate("StickVariant",new Vector3(0,0,0),Quaternion.identity);
        text.text = "a";
        StartCoroutine("SetCloudID");
    }

    private IEnumerator SetCloudID()
    {
        yield return new WaitForSeconds(0.5f);
        var obj = GameObject.FindWithTag("InfoObj");
        while (!obj)
        {
            obj = GameObject.FindWithTag("InfoObj");
            yield return new WaitForSeconds(0.5f);
        }
        var info = obj.GetComponent<InfoManager>();
        while (!info.ExAnchor)
        {
            yield return new WaitForSeconds(0.5f);
        }
        cloudID = info.CloudID;
        XPSession.ResolveCloudAnchor(cloudID).ThenAction((System.Action<CloudAnchorResult>)
        (result =>
             {
                 if (result.Response != CloudServiceResponse.Success)
                 {
                    text.text = result.Response.ToString();
                     return;
                 }
                 text.text = "成功";
                 arCoreOrigin.SetWorldOrigin(result.Anchor.transform);
                 anchor = result.Anchor;
                 stick.transform.parent = anchor.transform;
                 var camera = GameObject.FindWithTag("CliantCamera");
                 stick.GetComponent<StickManager>().SetCamera(camera,anchor);
                 text.text = anchor.transform.position.ToString();
             }));
        info.ConnectFinish();
    }
}
