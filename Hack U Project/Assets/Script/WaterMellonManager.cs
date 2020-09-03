using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMellonManager : MonoBehaviour
{
    [SerializeField] GameObject hiteffect;
    [SerializeField] GameObject brokenMellon;
    Vector3 pos;
    Quaternion quaternion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stick") //StickSタグの付いたゲームオブジェクトと衝突したか判別
        {
            pos = this.transform.position;
            Instantiate(hiteffect, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            //Destroy(this.gameObject); //衝突したゲームオブジェクトを削除
            Instantiate(brokenMellon,pos,quaternion);

            brokenMellon.transform.SetParent(transform, false); //oya0903　プレハブの大きさ設定

            /*SEを鳴らすためのオブジェクトの設定とか*/
            GameObject SEobj;
            SE SEsc;
            SEobj = GameObject.Find("SEGameObject");
            SEsc = SEobj.GetComponent<SE>();

            SEsc.Onbreak();

            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SS>().ScreenShot();
        }
    }
}
