using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour {

    public int numberOfHits = 1;
    public float velocityRequired = 2.0f;
    public float reduceColorBy = .2f;

    void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.tag == "Projectile")
        {

            var projectileRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (projectileRigidbody.velocity.magnitude > velocityRequired)
            {
                Debug.Log("HIT!");
                numberOfHits--;
                Material material = GetComponent<Renderer>().material;
                Color color = material.color;
                color.r -= color.r * reduceColorBy;
                color.g -= color.g * reduceColorBy;
                color.b -= color.b * reduceColorBy;
                material.color = color;
                Debug.Log(color);

                if (numberOfHits == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
