using UnityEngine;
using System.Collections;
using System;

public class Common : MonoBehaviour {

    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public static GameObject CreateGO(Transform _TfParent,string _prefabName){
        var prefab = Resources.Load("Prefab/Widget/" + _prefabName);
        if (prefab == null)
        {
            Log.Error("prefab" + prefab);
            return null;
        }
        GameObject go = (GameObject)GameObject.Instantiate(prefab); //, _TfParent.localPosition, _TfParent.rotation
        //go.transform.setp = _TfParent;

        go.transform.SetParent(_TfParent, false);
        //go.transform.localPosition = _TfParent.localPosition;
        //go.transform.localScale = _TfParent.localScale;
        return go;
    }

    //public static T Create<T>(Transform _TfParent)
    //{
    //    string scriptName = typeof(T).Name.Trim();
    //    GameObject go = CreateGO(_TfParent, scriptName);
    //    var ss = FindObjectOfType(typeof(T));
    //    T script = go.AddComponent<typeof(T)>();

    //    return script;
    //}
}
