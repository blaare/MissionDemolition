using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            goalMet = true;
            Material material = GetComponent<Renderer>().material;
            Color color = material.color;
            color.a = 1;
            material.color = color;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
