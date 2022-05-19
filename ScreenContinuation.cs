using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenContinuation : MonoBehaviour
{
    /*private void Awake()
    {

        var objs = FindObjectsOfType<ScreenContinuation>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
    }*/
    void Start()
    {
        // 핸드폰 화면이 절전 상태가 되지 않도록 유지한다.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    
    void Update()
    {
        
    }
}
