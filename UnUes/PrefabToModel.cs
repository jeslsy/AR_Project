using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabToModel : MonoBehaviour
{

    public MyARManager myARManager;
    public GameObject btn_onoff;
    // Start is called before the first frame update
    
    public void PTM()
    {
        btn_onoff.gameObject.SetActive(true);
        if (PlayerPrefs.GetString("pName") == "상디 조명") 
            myARManager.SetSanji();
        else if (PlayerPrefs.GetString("pName") == "불상 조명")
            myARManager.SetBudda();
        else if (PlayerPrefs.GetString("pName") == "한복 조명")
            myARManager.SetHanbok();
        else if (PlayerPrefs.GetString("pName") == "예수 조명")
            myARManager.SetJesus();

    }
}
