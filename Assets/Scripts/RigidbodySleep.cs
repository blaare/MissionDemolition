﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodySleep : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if(rigidbody != null) { rigidbody.Sleep(); }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
