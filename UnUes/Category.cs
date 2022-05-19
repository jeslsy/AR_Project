using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    public GameObject FigureUI, ReligionUI, LifeUI, EtcUI;
    public Button btn_figure, btn_religion, btn_life, btn_etc;
    void Update()
    {
        btn_figure.onClick.AddListener(() => {
            FigureUI.SetActive(true);
            ReligionUI.SetActive(false);
            LifeUI.SetActive(false);
            EtcUI.SetActive(false);
        });

        btn_religion.onClick.AddListener(() => {
            FigureUI.SetActive(false);
            ReligionUI.SetActive(true);
            LifeUI.SetActive(false);
            EtcUI.SetActive(false);
        });

        btn_life.onClick.AddListener(() => {
            FigureUI.SetActive(false);
            ReligionUI.SetActive(false);
            LifeUI.SetActive(true);
            EtcUI.SetActive(false);
        });

        btn_etc.onClick.AddListener(() => {
            FigureUI.SetActive(false);
            ReligionUI.SetActive(false);
            LifeUI.SetActive(false);
            EtcUI.SetActive(true);
        });
    }
}
