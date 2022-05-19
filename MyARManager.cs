/* 유니티 버전 2020.1.3f1 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; // ARFoundation 에셋 받아야함
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class MyARManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject indicator; // 바닥 표시 객체
    public GameObject ARPlane;
    public GameObject Btn_Horizontal;
    public GameObject Btn_Vertical;
	public GameObject Btn_Ceiling;
    public Text DetectionMode_text;
	public Text indicator_text;
    public Text object_text;

    public GameObject[] myObj;
    public GameObject myObj_rot;
    public GameObject MainUI, ArrangeUI, ObjectSelectUI, ChangeUI, PreferenceUI;

    //public GameObject light_onoff;
    //public GameObject OnoffUI;
    public Button btn_arrange, btn_change, btn_onoff;
    
    //public Button btn_Sanji, btn_Strange, btn_objectfbx_back, btn_objectfbx, btn_board, btn_ironman, btn_myCar;
    public bool indicator_act = true;
    public bool arrangetf = false;
    public Material indicator_color, plane_dot;
    // ************Bloom bloomLayer; // bloom 효과
    // ************PostProcessVolume volume;

    ARPlaneManager planeManager;
    ARRaycastManager aManager; // AR Session Origin에 있는 AR 레이캐스트 매니저 : 지면 인식 및 플레인을 생성시키는 매니저 스크립트
    Vector2 screenCenter; // 2차원 벡터와 위치를 표현
    GameObject targetObject; // 보여질 객체를 저장할 게임오브젝트
    GameObject light, light2, unlight;
    GameObject swap;
    bool c_hor, c_ver, c_ceil;

    DontDestroyObject ddo;

    ARPointCloudManager aRPointCloudManager;
    //public GameObject controlLight; 
    List<string> nameList = new List<string>(new string[] { "상디 조명","닥터 스트레인지 조명",
        "마리아 조명", "한복 조명","예수 조명", "불상 조명", });

    void Start()
    {
        aManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        ddo = GameObject.Find("DontDestroyObject").GetComponent<DontDestroyObject>();
        aRPointCloudManager = GetComponent<ARPointCloudManager>();
        swap = myObj[0];
        //child = transform.FindChild("Ironman").gameObject;
        //Debug.Log(child);


        // ********volume = myObj[4].GetComponent<PostProcessVolume>();
        // ********volume.profile.TryGetSettings(out bloomLayer);

        btn_onoff.onClick.AddListener(() =>
        {
            //Destroy(targetObject);

            light.SetActive(!light.activeSelf);
            light2.SetActive(!light2.activeSelf);
            unlight.SetActive(light.activeSelf);

            //volume.enabled = false;
        });
        btn_arrange.onClick.AddListener(() => 
        {   arrangetf = true;
            //ARPPointCloudManager 비활성화
            aRPointCloudManager.enabled = false;
            // 남아있는 점 제거
            foreach (var Point in aRPointCloudManager.trackables) 
            {
                Point.gameObject.SetActive(false);
            }
            plane_dot.color = Color.clear; //뭔지 모르겠음
            ChangeUI.SetActive(true);
            targetObject = Instantiate(myObj[nameList.IndexOf(ddo.pName)], indicator.transform.position, indicator.transform.rotation);
            if (indicator_color.color == Color.blue)
            {
                targetObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            light = targetObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
            

            /*if (ddo.pName == "상디 조명")
                SetSanji();
            else if (ddo.pName == "불상 조명")
                SetBudda();
            else if (ddo.pName == "한복 조명")
                SetHanbok();
            else if (ddo.pName == "예수 조명")
                SetJesus();*/
        });
    }

    void Update()
    {
        // 평면을 추적하여 인디케이터를 표시해주는 함수
        ShowIndicator();
        DetectionMode_Select();

        // 인디케이터가 표시된 상태에서 화면을 터치하면, 자동차 모델링을 표시하겠다.
        if (indicator.activeSelf)
        {
            ArrangeUI.SetActive(true); // 배치 버튼
            
            // new Color(90/255f, 142/155f, 72/255f); new Color(0.3f, 0.4f, 0.7f, 0.2f);
            //btn_arrange.onClick.AddListener(() => { arrangetf = true; plane_dot.color = Color.clear; }); //?
            if(arrangetf == true)
            {
                
                arrangetf = false;
                ArrangeUI.SetActive(arrangetf);
                //ObjectSelectUI.SetActive(true);  // 오브젝트 선택 버튼
                //MainUI.SetActive(false);
                indicator_act = false;

                if (targetObject)
                {
					//인디케이터의 위치, 회전값 세팅
                    targetObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                }
                else
                {
                    /* 오브젝트 선택 버튼 */

                    /*btn_Sanji.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[0], indicator.transform.position, indicator.transform.rotation);
                        targetObject.transform.Rotate(0, 180, 0);
                        targetObject.transform.localPosition += new Vector3(0.1f, 0, 0.1f);
                        light = targetObject.transform.Find("back").gameObject;
                    });
                    btn_Strange.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[1], indicator.transform.position, indicator.transform.rotation);
                        targetObject.transform.Rotate(0, 180, 0);
                        targetObject.transform.localPosition += new Vector3(0.1f, 0, 0.1f);
                        light = targetObject.transform.Find("back").gameObject;
                    });
                    btn_objectfbx_back.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[2], indicator.transform.position, indicator.transform.rotation);
                        targetObject.transform.Rotate(0, 180, 0);
                        targetObject.transform.localPosition += new Vector3(0.1f, 0, 0.1f);
                        light = targetObject.transform.Find("back").gameObject;
                    });
                    btn_objectfbx.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[3], indicator.transform.position, indicator.transform.rotation);
                        targetObject.transform.Rotate(0, 100, 0);
                        targetObject.transform.localPosition += new Vector3(-2, 0, 8); // 오른쪽, 위, 뒤
                        light = targetObject.transform.Find("back").gameObject;
                        light2 = targetObject.transform.Find("back2").gameObject;
                    });
                    btn_board.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[4], indicator.transform.position, indicator.transform.rotation);
                    });
                    btn_ironman.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[5], indicator.transform.position, indicator.transform.rotation);
                        light = targetObject.transform.Find("Ironman").Find("ironman").Find("HD_Ironman:polySurface3869").gameObject;
                    });
                    btn_myCar.onClick.AddListener(() =>
                    {
                        ChangeUI.SetActive(true);
                        ObjectSelectUI.SetActive(false);
                        targetObject = Instantiate(myObj[6], indicator.transform.position, indicator.transform.rotation);
                    });*/
                    /* 오브젝트 변경 버튼 */
                    /*btn_change.onClick.AddListener(() =>
                    {
                        //Destroy(targetObject);
                        ArrangeUI.SetActive(arrangetf);
                        targetObject.SetActive(false);
                        ChangeUI.SetActive(false);
                        indicator_act = true;
                        plane_dot.color = new Color(1.0f, 1.0f, 1.0f, 70/255f);

                    });*/
                    /* 조영 On/Off 버튼 */
                    /*Vector2 posobj = Input.GetTouch(0).position;
                    Vector3 theTouch = new Vector3(posobj.x, posobj.y, 0.0f);

                    Ray rayobj = Camera.main.ScreenPointToRay(theTouch);
                    RaycastHit hit;
                    if (Physics.Raycast(rayobj, out hit, Mathf.Infinity))    // 레이저를 쏴서 충돌 할 경우 return true
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 처음 터치 시 발생
                        {
                            light.SetActive(!light.active);
                            light2.SetActive(!light2.active);
                        }              
                    }*/
                    /*btn_onoff.onClick.AddListener(() =>
                    {
                        //Destroy(targetObject);

                        Debug.Log("!");
                        light.SetActive(!light.activeSelf);
                        light2.SetActive(!light2.active);
                        unlight.SetActive(light.active);

                        //volume.enabled = false;
                    });*/
                }
                //Rotation_text.text = targetObject.transform.rotation.x + " " + targetObject.transform.rotation.y + " " + targetObject.transform.rotation.z;
                //Rotation_text.text = targetObject.transform.eulerAngles;
                indicator.SetActive(false);
            }
        }
    }
    public void SetSanji()
    {
        ChangeUI.SetActive(true);
        //ObjectSelectUI.SetActive(false);
        //MainUI.SetActive(true);
        //targetObject 생성 
        targetObject = Instantiate(myObj[0], indicator.transform.position, indicator.transform.rotation);
        //targetObject.transform.Rotate(0, 180, 0);
        //targetObject.transform.localPosition += new Vector3(0.1f, 0, 0.1f);
		// 테스트용
        /*if (indicator_color.color == Color.blue){
			if(mainCamera.transform.position.x >= targetObject.transform.position.x){
				targetObject.transform.eulerAngles = new Vector3(0, 90, 0);
			}
			else{
				targetObject.transform.eulerAngles = new Vector3(0, 270, 0);
			}
		}
		else if(indicator_color.color == Color.red){
			targetObject.transform.eulerAngles = new Vector3(180, 0, 0); // x축 180도 반대로
		}*/
        light = targetObject.transform.GetChild(2).gameObject;

		//test presenting text(t1)
		//indicator_text.text = "indi_pos: \nx: " + indicator.transform.position.x +"\ny: "+ indicator.transform.position.y + "\nz: " + indicator.transform.position.z + "\n"
		// 						   + "indi_rotation: \nx: " + indicator.transform.rotation.x +"\ny: "+ indicator.transform.rotation.y + "\nz: " + indicator.transform.rotation.z;
		
		//object_text.text = "obj_pos: \nx: " + targetObject.transform.position.x +"\ny: "+ targetObject.transform.position.y + "\nz: " + targetObject.transform.position.z + "\n"
		//						   + "obj_rotation: \nx: " + targetObject.transform.rotation.x +"\ny: "+ targetObject.transform.rotation.y + "\nz: " + targetObject.transform.rotation.z;
    }


    public void SetStrange()
    {
        ChangeUI.SetActive(true);
        ObjectSelectUI.SetActive(false);
        MainUI.SetActive(true);
        targetObject = Instantiate(myObj[1], indicator.transform.position, indicator.transform.rotation);
        targetObject.transform.Rotate(0, 180, 0);
        targetObject.transform.localPosition += new Vector3(0.1f, 0, 0.1f);
		if (indicator_color.color == Color.blue){
            targetObject.transform.rotation = Quaternion.Euler(90, 0, 0);
		}
        //targetObject.transform.localPosition += new Vector3(0, 0, 0.5f);
        light = targetObject.transform.GetChild(2).gameObject;

        //test presenting text(t1)
        /*indicator_text.text = "indi_pos: \nx: " + indicator.transform.position.x +"\ny: "+ indicator.transform.position.y + "\nz: " + indicator.transform.position.z + "\n"
								   + "indi_rotation: \nx: " + indicator.transform.rotation.x +"\ny: "+ indicator.transform.rotation.y + "\nz: " + indicator.transform.rotation.z;
		
		object_text.text = "obj_pos: \nx: " + targetObject.transform.position.x +"\ny: "+ targetObject.transform.position.y + "\nz: " + targetObject.transform.position.z + "\n"
								   + "obj_rotation: \nx: " + targetObject.transform.rotation.x +"\ny: "+ targetObject.transform.rotation.y + "\nz: " + targetObject.transform.rotation.z;*/
    }

	/*
    public void SetMaria()
    {
        ChangeUI.SetActive(true);
        ObjectSelectUI.SetActive(false);
        MainUI.SetActive(true);
        targetObject = Instantiate(myObj[2], indicator.transform.position, indicator.transform.rotation);
        targetObject.transform.Rotate(0, 180, 0);
        targetObject.transform.localPosition += new Vector3(0.1f, 0, 0.1f);
		if (indicator_color.color == Color.blue)
            targetObject.transform.eulerAngles = new Vector3(0, 40, 0);
        light = targetObject.transform.Find("back").gameObject;
    }
	*/

    public void SetHanbok()
    {
        ChangeUI.SetActive(true);
        //ObjectSelectUI.SetActive(false);
        //MainUI.SetActive(true);
        targetObject = Instantiate(myObj[3], indicator.transform.position, indicator.transform.rotation);
        targetObject.transform.Rotate(0, 100, 0);
        targetObject.transform.localPosition += new Vector3(-2, 0, 8); // 오른쪽, 위, 뒤
        if (indicator_color.color == Color.blue)
            targetObject.transform.eulerAngles = new Vector3(0, 40, 0);
        light = targetObject.transform.GetChild(2).gameObject;
        light2 = targetObject.transform.GetChild(3).gameObject;
    }
    public void SetJesus()
    {
        ChangeUI.SetActive(true);
        //ObjectSelectUI.SetActive(false);
        //MainUI.SetActive(true);
        targetObject = Instantiate(myObj[4], indicator.transform.position, indicator.transform.rotation);
        targetObject.transform.Rotate(0, 180, 0);
        if (indicator_color.color == Color.blue)
            targetObject.transform.eulerAngles = new Vector3(0, 40, 0);
        light = targetObject.transform.GetChild(2).gameObject;
    }

	
    public void SetBudda()
    {
        ChangeUI.SetActive(true);
        //ObjectSelectUI.SetActive(false);
        //MainUI.SetActive(true);
        targetObject = Instantiate(myObj[5], indicator.transform.position, indicator.transform.rotation);
        targetObject.transform.Rotate(0, 180, 0);
        if (indicator_color.color == Color.blue)
            targetObject.transform.eulerAngles = new Vector3(0, 40, 0);
        //targetObject.transform.localPosition += new Vector3(0, 0, 0.5f);
        light = targetObject.transform.GetChild(2).gameObject;
    }
	

    public void Setlight()
    {
        ChangeUI.SetActive(true);
        ObjectSelectUI.SetActive(false);
        MainUI.SetActive(true);
        targetObject = Instantiate(myObj[6], indicator.transform.position, indicator.transform.rotation);
        light = targetObject.transform.Find("light").gameObject;
        unlight = targetObject.transform.Find("unlight").gameObject;
    }

    public void SetPreference() // 환경설정 버튼
    {
        PreferenceUI.SetActive(true);
    }

    public void CancelPreference() // 환경설정 뒤로가기 버튼
    {
        PreferenceUI.SetActive(false);
    }
    /*public void LightOnOff()
    {
        bloomLayer.enabled.value = false;
    }*/
    void ShowIndicator()
    {
        // 1. 카메라로 땅이나 책상 등 평평한 바닥면이 존재하는 곳을 비추고 있다면, 그곳에 인디케이터를 표시하고 싶다.

        //화면의 중앙
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); 
        // AR 레이에 부딪힌 대상을 저장할 리스트 변수
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();
        // Ray를 화면 중앙에서 쏴서 평면을 찾고 hitInfos에 저장
        if (aManager.Raycast(screenCenter, hitInfos, UnityEngine.XR.ARSubsystems.TrackableType.Planes) && indicator_act == true) 
        {
            //인디케이터의 위치와 회전값을 추적된 대상의 위치와 회전값으로 놓는다.

            Pose pose = hitInfos[0].pose; // 첫 번째로 감지된 평면 Pose는 Raycast를 통해 탐지된 대상의 방향, 거리 등을 저장
            indicator.transform.SetPositionAndRotation(pose.position, pose.rotation); // 받아온 pose의 위치와 로테이션을 바탕으로 인디케이터 설정

            Vector3 dir = Camera.main.transform.forward;
            /*var cameraForward = mainCamera.transform.TransformDirection(Vector3.forward); // 카메라 정면 방향 설정
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            pose.rotation = Quaternion.LookRotation(cameraBearing);*/
            //dir.y = 0;
            //Quaternion rot = Quaternion.LookRotation(dir);
            //dir.x = pose.rotation.x;
            //dir.z = pose.rotation.z;
            //ScriptText.text = pose.rotation.x +" "+ pose.rotation.y +" "+ pose.rotation.z;

			// 인디케이터 rotation.x가 0일 경우 바닥으로 인식
            if (pose.rotation.x == 0) 
            {
                indicator_color.color = Color.white;
                //myObj[0] = swap;
            }
			// 인데케이터 rotation.x 가 음수이면 천장(인디케이터 빨간색)
			else if(pose.rotation.y == 0) 
			{
				indicator_color.color = Color.red;
			}
			// 인디케이터 rotation.x가 양수이면 바닥(인지케이터 파란색)
            else
            {
                indicator_color.color = Color.blue;
                //myObj[0] = myObj_rot;
            }

            // 인디케이터를 활성화한다.
            indicator.SetActive(true);

			/*indicator_text.text = "indi_pos: \nx: " + indicator.transform.position.x +"\ny: "+ indicator.transform.position.y + "\nz: " + indicator.transform.position.z + "\n"
								   + "indi_rotation: \nx: " + indicator.transform.rotation.x +"\ny: "+ indicator.transform.rotation.y + "\nz: " + indicator.transform.rotation.z;
		*/
			// object랑 잘 연결되었는지 확인용(t2)
			//object_text.text = "obj_pos: \nx: " + targetObject.transform.position.x +"\ny: "+ targetObject.transform.position.y + "\nz: " + targetObject.transform.position.z + "\n"
			//					   + "obj_rotation: \nx: " + targetObject.transform.rotation.x +"\ny: "+ targetObject.transform.rotation.y + "\nz: " + targetObject.transform.rotation.z;
    
        }
        else
        {
            // 바닥 인식을 할 때까지 인디케이터 비활성화
            indicator.SetActive(false);

			//indicator_text.text = "";
        }
    }

    // 환경설정 DetectionMode 변경
    void DetectionMode_Select()
    {
        c_hor = Btn_Horizontal.GetComponent<SwitchToggle>().hor; // SwitchToggle스크립트의 hor 변수 값 사용
        c_ver = Btn_Vertical.GetComponent<SwitchToggle_Vertical>().ver; // SwitchToggle스크립트의 ver 변수 값 사용
		c_ceil = Btn_Ceiling.GetComponent<SwitchToggle_Ceiling>().hor; //SwitchToggle스크립트의 hor 변수 값 사용
		
		// 전부 활성화
		if(c_hor == true && c_ver == true && c_ceil == true) 
		{
			planeManager.detectionMode = PlaneDetectionMode.Horizontal | PlaneDetectionMode.Vertical;
			DetectionMode_text.text = "인식 모드 : " + planeManager.detectionMode;
		}
		// 바닥만 활성화
		else if(c_hor == true && c_ver == false && c_ceil == false)  
		{
			planeManager.detectionMode = PlaneDetectionMode.Horizontal;
			DetectionMode_text.text = "인식 모드 : " + planeManager.detectionMode;
			if(indicator.transform.rotation.y == 0)
			{
				indicator.SetActive(false);
			}
		}
		// 벽면만 활성화
        else if (c_hor == false && c_ver == true && c_ceil == false) 
        {
			//myObj[0] = GameObject.FindWithTag("Sanji_rot");
			planeManager.detectionMode = PlaneDetectionMode.Vertical;
			DetectionMode_text.text = "인식 모드 : " + planeManager.detectionMode;
            
        }
		// 천장만 활성화
		else if(c_hor == false && c_ver == false && c_ceil == true)  
		{
			planeManager.detectionMode = PlaneDetectionMode.Horizontal;
			DetectionMode_text.text = "인식 모드 : " + planeManager.detectionMode;
			if(indicator.transform.rotation.x == 0)
			{
				indicator.SetActive(false);
			}
		}
		// 전부 비활성화
        else if(c_hor == false && c_ver == false && c_ceil == false)
        {
            planeManager.detectionMode = PlaneDetectionMode.None;
            DetectionMode_text.text = "하나 이상의 인식모드가 필요합니다. ";
        }
		else
		{
			planeManager.detectionMode = PlaneDetectionMode.None;
            DetectionMode_text.text = "";
		}
        
        Debug.Log("Detection mode = " + planeManager.detectionMode);
    }

    void lightOnOff()
    {

    }
}