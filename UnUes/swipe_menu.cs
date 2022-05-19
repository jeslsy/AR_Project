using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe_menu : MonoBehaviour
{
    public GameObject scrollbar;
    public Text imageCount;
    float[] pos;
    float speed = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //transform.childCount : 스크립트가 들어가는 오브젝트의 자식오브젝트의 개수
        pos = new float[transform.childCount];
        //pos.Length : pos 배열의 크기
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        //마우스 버튼 0 : 왼쪽, 1 : 오른쪽, 2 : 가운데
        if (Input.GetMouseButton(0)) 
        {
           
            if (scrollbar.GetComponent<Scrollbar>().value < 0)
            {
                scrollbar.GetComponent<Scrollbar>().value = 0;
            }
            else if (scrollbar.GetComponent<Scrollbar>().value > 1)
            {
                scrollbar.GetComponent<Scrollbar>().value = 1;
            }
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollbar.GetComponent<Scrollbar>().value < pos[i] + (distance / 2) &&
                    scrollbar.GetComponent<Scrollbar>().value > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], Time.deltaTime * speed);
                    imageCount.text = (i + 1) + "/" + pos.Length;
                }
            }
        }
    }
}
