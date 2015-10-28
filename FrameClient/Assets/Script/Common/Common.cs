using UnityEngine;
using System.Collections;
using System;

public class Common : MonoBehaviour {

    public static GameObject CreateGO(Transform _TfParent,string _prefabName){
        var prefab = Resources.Load("Prefab/Widget/" + _prefabName);
        if (prefab == null)
        {
            Log.Error("prefab" + prefab);
            return null;
        }
        GameObject go = (GameObject)GameObject.Instantiate(prefab);
        go.transform.SetParent(_TfParent, false);
        return go;
    }

    public static T Create<T>(Transform _TfParent) where T:Component
    {
        string scriptName = typeof(T).Name.Trim();
        //Debug.Log("scriptName:" + scriptName);
        GameObject go = CreateGO(_TfParent, scriptName);
        T script = go.AddComponent<T>();

        return script;
    }
}
