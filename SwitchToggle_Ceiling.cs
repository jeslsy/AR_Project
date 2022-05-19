using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DG.Tweening;

public class SwitchToggle_Ceiling : MonoBehaviour
{
    public bool hor = false;
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);


        if (toggle.isOn)
        {
            OnSwitch(true);
        }

    }

    void OnSwitch(bool on)
    {
        //uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; // no anim
        uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);

        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
        backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, .6f);

        //handleImage.color = on ? handleActiveColor : handleDefaultColor ; // no anim
        handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .4f);

        // planemode 수정
        //planeManager.detectionMode = on ? PlaneDetectionMode.Vertical : PlaneDetectionMode.Horizontal;
        //hor = on ? true : false;
        //DetectionMode_text.text = "Detection mode : " + planeManager.detectionMode;
        //Debug.Log("Detection mode = " + planeManager.detectionMode);

		hor = on ? true : false;   // 토글이 on일 경우
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
