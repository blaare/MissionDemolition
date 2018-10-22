using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    static public Slingshot S;


    [Header("Set In Inspector")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;
    public Camera mainCamera;
    public GameObject prefabProjectileLine;

    [Header("Set Dynamically <Do Not Set>")]
    public GameObject launchPoint;
    public Vector3 launchPosition;
    public GameObject projectile;
    private Rigidbody projectileRigidbody;
    public bool aimingMode;

    static public Vector3 LAUNCH_POSITION
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPosition;
        }
    }

    void Awake()
    {
        S = this;
        Transform launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
        launchPosition = launchPointTransform.position;
        
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse entered");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        Debug.Log("mouse exited");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPosition;
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("setting new projectile");
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; //Note already set by  getComponent
    }


    void Update()
    {
        if (!aimingMode)
        {
            Debug.Log("NotInAimingMode");
            return;

        }
        Debug.Log("InAimingMode");

        Vector3 mousePosition2D = Input.mousePosition;
        mousePosition2D.z = -Camera.main.transform.position.z;
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);

        Vector3 mouseDelta = mousePosition3D - launchPosition;

        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projectilePosition = launchPosition + mouseDelta;
        projectile.transform.position = projectilePosition;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMultiplier;
            FollowCamera.PointOfInterest = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
            
        }


    }
}
