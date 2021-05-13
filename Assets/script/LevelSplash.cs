using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSplash : MonoBehaviour
{

    public  GameObject Splash;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeOpacity", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOpacity()
    {
        Splash.SetActive(false);
    }
}
