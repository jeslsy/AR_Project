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
        if (PlayerPrefs.GetString("pName") == "��� ����") 
            myARManager.SetSanji();
        else if (PlayerPrefs.GetString("pName") == "�һ� ����")
            myARManager.SetBudda();
        else if (PlayerPrefs.GetString("pName") == "�Ѻ� ����")
            myARManager.SetHanbok();
        else if (PlayerPrefs.GetString("pName") == "���� ����")
            myARManager.SetJesus();

    }
}
