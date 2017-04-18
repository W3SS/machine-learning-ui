using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return42Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log(MyFirstDLLWrapper.return42());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
