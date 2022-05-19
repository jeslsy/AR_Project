using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInformationFromDontDestroyObject : MonoBehaviour
{
    Button btn;
    DontDestroyObject ddo;
    public GameObject btn_onoff;
    public MyARManager myARManager;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        ddo = GameObject.Find("DontDestroyObject").GetComponent<DontDestroyObject>();
        btn.onClick.AddListener(() =>
        {
            
            btn_onoff.gameObject.SetActive(true);
            if (ddo.pName == "상디 조명")
                myARManager.SetSanji();
            else if (ddo.pName == "불상 조명")
                myARManager.SetBudda();
            else if (ddo.pName == "한복 조명")
                myARManager.SetHanbok();
            else if (ddo.pName == "예수 조명")
                myARManager.SetJesus();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
