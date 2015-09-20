using UnityEngine;
using System.Collections;

public class GameDriver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Game.Instance.IsInit()) { 
            Game.Instance.MainLoop();
        }
	}
}
