using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchScripts : MonoBehaviour
{
    public List<GameObject> ListGameObject;
    public Text stext;
    public GameObject bar;
    int cnt;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SearchResult()
    {

        if (stext.text == "" || stext.text == " ")
        {
            return;
        }
        cnt = 0;
        for (int i=0;i<ListGameObject.Count;i++)
        {
            if (ListGameObject[i].GetComponent<PreviewProduct>().pName.Contains(stext.text))
            {
                ListGameObject[i].gameObject.SetActive(true);
                cnt++;
            }
            else
                ListGameObject[i].gameObject.SetActive(false);
        }
    }

    
}
