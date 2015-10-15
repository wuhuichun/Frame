using UnityEngine;
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

        Application.LoadLevel("Login");
    }

    public IEnumerator LoadConfig()
    {
        ConfigMgr.Instance.Load();
        yield return 0;
    }

}
