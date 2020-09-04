using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DriverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHost()
    {
        SceneManager.LoadScene("HostScene");
    }

    public void LoadCliant()
    {
        SceneManager.LoadScene("CliantScene");
    }
}
