using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public Quaternion originalRotationValue; // declare this as a Quaternion
    public Vector3 originalPositionValue; // declare this as a Quaternion
    float rotationResetSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {
        originalRotationValue = transform.rotation; // save the initial rotation
        originalPositionValue = transform.position; // save the initial rotation
    }
    void Update()
    {
        //rotate selected piece
        if (Input.GetKeyDown("r"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed);
            transform.position = Vector3.Slerp(transform.position, originalPositionValue, Time.time * rotationResetSpeed);
        }
        
    }
}
