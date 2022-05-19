using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // Start is called before the first frame update
    public void NaverURL()
    {
        Application.OpenURL("https://shopping.naver.com/home/p/index.naver");
    }
}
