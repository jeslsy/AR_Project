using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewProduct : MonoBehaviour
{
    public string pCategory, pName, pPrice;
    public Sprite pSoureImage;
    DontDestroyObject ddo;
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = pCategory;
        transform.GetChild(1).GetComponent<Text>().text = pName;
        transform.GetChild(2).GetComponent<Text>().text = pPrice;
        transform.GetChild(3).GetComponent<Image>().sprite = pSoureImage;
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            ddo = GameObject.Find("DontDestroyObject").GetComponent<DontDestroyObject>();
            ddo.pName = pName;
            ddo.pPrice = pPrice;
            ddo.pCategory = pCategory;
            ddo.pSourceImage = pSoureImage;
            ddo.LoadProductScene();
        });
    }

}
