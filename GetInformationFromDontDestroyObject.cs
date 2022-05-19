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
            if (ddo.pName == "��� ����")
                myARManager.SetSanji();
            else if (ddo.pName == "�һ� ����")
                myARManager.SetBudda();
            else if (ddo.pName == "�Ѻ� ����")
                myARManager.SetHanbok();
            else if (ddo.pName == "���� ����")
                myARManager.SetJesus();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
