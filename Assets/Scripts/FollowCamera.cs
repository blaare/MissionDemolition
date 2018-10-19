using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [Header("Set In Inspector")]
    public float easing = 0.5f;
    public Vector2 minXY = Vector2.zero;

    static public GameObject PointOfInterest;

    [Header("SET DYNAMICALLY")]
    public float cameraZ;

    void Awake()
    {
        cameraZ = transform.position.z;
    }

    void FixedUpdate()
    {

        Vector3 destination;
        if(PointOfInterest == null)
        {
            destination = Vector3.zero;
        } else
        {
            destination = PointOfInterest.transform.position;
            if(PointOfInterest.tag == "Projectile")
            {
                if (PointOfInterest.GetComponent<Rigidbody>().IsSleeping())
                {
                    PointOfInterest = null;
                    return;
                }
            }
        }
        
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = cameraZ;

        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
