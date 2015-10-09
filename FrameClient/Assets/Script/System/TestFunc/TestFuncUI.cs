using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestFuncUI : MonoBehaviour {

    private Button BtnFunc01;
    private Button BtnFunc02;
    private Button BtnFunc03;
    private Button BtnFunc04;


    void Awake()
    {
        BtnFunc01 = transform.FindChild("BtnFunc01").GetComponent<Button>();
        BtnFunc02 = transform.FindChild("BtnFunc02").GetComponent<Button>();
        BtnFunc03 = transform.FindChild("BtnFunc03").GetComponent<Button>();
        BtnFunc04 = transform.FindChild("BtnFunc04").GetComponent<Button>();

        BtnFunc01.onClick.AddListener(OnBtnFunc01Click);
        BtnFunc02.onClick.AddListener(OnBtnFunc02Click);
        BtnFunc03.onClick.AddListener(OnBtnFunc03Click);
        BtnFunc04.onClick.AddListener(OnBtnFunc04Click);
    }

	// Use this for initialization
	void Start () {
	
	}

    private void OnBtnFunc01Click()
    {
        Debug.Log("Click BtnFunc01");
        Application.LoadLevel("World");
    }

    private void OnBtnFunc02Click()
    {
        Debug.Log("Click BtnFunc02");
        Application.LoadLevel("UITest");
    }

    private void OnBtnFunc03Click()
    {
        Debug.Log("Click BtnFunc03");
        Application.LoadLevel("Create");
    }

    private void OnBtnFunc04Click()
    {
        Debug.Log("Click BtnFunc04");
    }
}
