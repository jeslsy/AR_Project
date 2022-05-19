using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCar : MonoBehaviour
{
    public float rotSpeed = 200.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                float delta = touch.deltaPosition.x;

                transform.eulerAngles += new Vector3(0, delta, 0);
            }
        }
    }
}
