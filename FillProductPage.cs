using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillProductPage : MonoBehaviour
{
    DontDestroyObject ddo;

    // Start is called before the first frame update
    void Start()
    {
        ddo = GameObject.Find("DontDestroyObject").GetComponent<DontDestroyObject>();
        transform.GetChild(0).GetComponent<Text>().text = ddo.pCategory;
        transform.GetChild(1).GetComponent<Text>().text = ddo.pName;
        transform.GetChild(2).GetComponent<Text>().text = ddo.pPrice;
        transform.GetChild(3).GetComponent<Image>().sprite = ddo.pSourceImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
