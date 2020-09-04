using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SampleScene : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
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
        var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("GamePlayer", v, Quaternion.identity);
        PhotonNetwork.Instantiate("InfoObj", Vector3.zero,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
