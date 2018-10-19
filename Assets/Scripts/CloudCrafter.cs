using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour {
    [Header("SET IN INSPECTOR")]
    public int numberOfClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPositionMinimum = new Vector3(-50, -5, 10);
    public Vector3 cloudPositionMaximum = new Vector3(150, 100, 10);
    public float cloudScaleMinimum = 1f;
    public float cloudScaleMaximum = 3f;
    public float cloudSpeedMultiplier = 0.5f;

    private GameObject[] cloudInstances;

    void Awake()
    {
        cloudInstances = new GameObject[numberOfClouds];

        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;

        for(int i = 0; i < numberOfClouds; i++)
        {
            cloud = Instantiate(cloudPrefab);
            Vector3 cloudPosition = Vector3.zero;
            cloudPosition.x = Random.Range(cloudPositionMinimum.x, cloudPositionMaximum.x);
            cloudPosition.y = Random.Range(cloudPositionMinimum.y, cloudPositionMaximum.y);

            float scale = Random.value;
            float scaleValue = Mathf.Lerp(cloudScaleMinimum, cloudScaleMaximum, scale);

            cloudPosition.y = Mathf.Lerp(cloudPositionMinimum.y, cloudPosition.y, scale);
            cloudPosition.z = 100 - 90 * scale;

            cloud.transform.position = cloudPosition;
            cloud.transform.localScale = Vector3.one * scale;

            cloudInstances[i] = cloud;
        }
    }

	
	// Update is called once per frame
	void Update () {
		foreach(GameObject cloud in cloudInstances)
        {
            float scaleValue = cloud.transform.localScale.x;
            Vector3 cloudPosition = cloud.transform.position;

            cloudPosition.x -= scaleValue * Time.deltaTime * cloudSpeedMultiplier;

            if (cloudPosition.x <= cloudPositionMinimum.x)
                cloudPosition.x = cloudPositionMaximum.x;

            cloud.transform.position = cloudPosition;
        }
	}
}
