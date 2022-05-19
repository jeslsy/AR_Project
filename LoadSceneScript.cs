using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update


    public void PlaneToProductScene()
    {
        
        SceneManager.LoadScene("ProductScene");
    }

    public void LoadPlaneScene()
    {
        //PlayerPrefs.SetInt("BackPlaneIndex", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("PlaneScene");
    }
    public void LoadBackScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("BackIndex"));

    }

    /*public void LoadBackProductScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("BackPlaneIndex"));
    }*/

    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void LoadCategoryScene()
    {
        SceneManager.LoadScene("CategoryScene");
    }
    public void LoadSearchScene()
    {
        SceneManager.LoadScene("SearchScene");
    }
    public void LoadMyPageScene()
    {
        SceneManager.LoadScene("MyPageScene");
    }
    private void Update()
    {
        Debug.Log(PlayerPrefs.GetString("pName"));
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "ProductScene")
                    LoadBackScene();
                else if (SceneManager.GetActiveScene().name == "PlaneScene")
                    SceneManager.LoadScene("ProductScene");
                else
                    Application.Quit();
            }
        }
    }
}
