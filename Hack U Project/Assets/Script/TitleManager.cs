using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        if(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) == false)
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
