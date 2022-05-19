using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyObject : MonoBehaviour
{

    public string pCategory, pName, pPrice;
    public Sprite pSourceImage;

    int backIndex;

    // Start is called before the first frame update
    void Awake()
    {
        var objs = FindObjectsOfType<DontDestroyObject>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void LoadProductScene()
    {
        PlayerPrefs.SetInt("BackIndex", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("ProductScene");
    }

}
