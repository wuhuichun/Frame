using UnityEngine;
using System;
using System.Collections;


public class UpdateResUI : MonoBehaviour {

    private bool m_isUpdate = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!m_isUpdate)
        {
            StartCoroutine(UpdateResource());
        }
	}

    

    public IEnumerator UpdateResource()
    {
        yield return 0;
        m_isUpdate = true;

        yield return StartCoroutine(LoadConfig());

        Game.Instance.Init();
        Application.LoadLevel("Login");
    }

    public IEnumerator LoadConfig()
    {
         ConfigMgr.Instance.LoadAll();
        yield return 0;
    }

    //public IEnumerator LoadConfigFile(string _file, Action<string> _action)
    //{

    //    WWW www = new WWW(_file);
    //    yield return www;

    //    if (!string.IsNullOrEmpty(www.error))
    //    {
    //        Log.Error(www.error);
    //        yield break;
    //    }

    //    string jsonText = www.text;
    //    _action(jsonText);
    //}

}
