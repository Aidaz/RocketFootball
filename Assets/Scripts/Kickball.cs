using UnityEngine;
using System.Collections;

public class Kickball : MonoBehaviour {


    public Rigidbody rb;
    public float bounceFactor = 0.9f; // Determines how the ball will be bouncing after landing. The value is [0..1]
    public float forceFactor = 10f;
    public float tMax = 5f; // Pressing time upper limit

    private float kickStart; // Keeps time, when you press button
    private float kickForce = 1000f; // Keeps time interval between button press and release 
    private Vector3 prevVelocity; // Keeps rigidbody velocity, calculated in FixedUpdate()
    bool t = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            kickForce = rb.velocity.magnitude;
            Vector3 forceVec = other.rigidbody.velocity * kickForce;
            //Vector3 forceVec = rb.position + transform.forward * kickForce;
            //double speed = other.rigidbody.velocity.magnitude;
            if (forceVec.y > 300f) forceVec.y = 400f;
            else forceVec.y = 300f;
            if (forceVec.x > 2000f) forceVec.x = 2000f;
            if (forceVec.z > 2000f) forceVec.z = 2000f;
            if (forceVec.x < -2000f) forceVec.x = -2000f;
            if (forceVec.z < -2000f) forceVec.z = -2000f;

            rb.AddForce(forceVec);

            Debug.Log(forceVec);
        }
        kickForce = 0;
        //if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Siena")) // Do not forget assign tag to the field
        //{
        //    rb.velocity = new Vector3(prevVelocity.x,
        //                                     -prevVelocity.y * Mathf.Clamp01(bounceFactor),
        //                                     prevVelocity.z);
        //}
    }

    void FixedUpdate()
    {
        if (kickForce != 0)
        {
            //float angle = Random.Range(0, 30) * Mathf.Deg2Rad;
            //rb.AddForce(new Vector3(forceFactor * Mathf.Clamp(kickForce, 0.0f, tMax) * Mathf.Cos(angle),
            //                               0.0f,
            //                               forceFactor * Mathf.Clamp(kickForce, 0.0f, tMax) * Mathf.Sin(angle)),
            //                   ForceMode.VelocityChange);
            //kickForce = 0;
        }
        prevVelocity = rb.velocity;

    }

}
