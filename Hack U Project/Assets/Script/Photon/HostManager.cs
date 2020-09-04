using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using UnityEngine.UI;

public class HostManager : RoomManager
{
    [SerializeField] GameObject watermellon;
    [SerializeField] GameObject stickPrefab;
    private bool setAnchor;
    InfoManager info;
    XPAnchor anchor;
    // Start is called before the first frame update
    private void Start()
    {
        //PhotonServerSettingsに設定した内容を使ってサーバに接続する
        PhotonNetwork.ConnectUsingSettings();
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
        var obj = Instantiate(watermellon, new Vector3(100, 100, 100), Quaternion.identity);
        info = PhotonNetwork.Instantiate("InfoObj", new Vector3(100, 100, 100), Quaternion.identity).GetComponent<InfoManager>();
        //StartCoroutine("MakeStick");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    private IEnumerator MakeStick()
    {
        while (!info.Connected)
        {
            yield return null;
        }
        var stick = Instantiate(stickPrefab, anchor.transform);
        stick.transform.localPosition = Vector3.zero;
    }
    */

    public void SetAnchorID(string id,XPAnchor anc)
    {
        if (!setAnchor)
        {
            GameObject.FindWithTag("InfoObj").GetComponent<InfoManager>().SetCloudID(id);
            setAnchor = true;
            anchor = anc;
        }
    }
}
