using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static float currentTime;

	// Use this for initialization
	void Start () {
        currentTime = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        currentTime += Time.deltaTime;
        GetComponent<Text>().text = "Time: " + Mathf.Floor(currentTime);
	}
}
