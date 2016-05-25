using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Car : MonoBehaviour {

    Transform centerOfMass;
    Vector3 zeroAc;
    Vector3 curAc;
    float sensH = 10;
    float sensV = 10;
    float smooth = 0.5f;
    float GetAxisH = 0;
    float GetAxisV = 0;
    int JumpCount = 0;
    Rigidbody rbody;

    private int count;
    public Text countText;



    public float maxTorque = 50f;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];
    public Rigidbody car;
    public int JumpForce;
    public int MoveForce;


    void Start()
    {
        ResetAxes();
        //rbody.centerOfMass = centerOfMass.localPosition;
        count = 0;
        SetCountText();
    }

    void ResetAxes()
    {
        zeroAc = Input.acceleration;
        curAc = Vector3.zero;
    }

    void Update()
    {
        bool Jump = CrossPlatformInputManager.GetButton("Jump");
        UpdateMeshesPositions();
        if (Jump && JumpCount < 3) 
        {
            car.AddForce(0, JumpForce, 0, ForceMode.Impulse);
            JumpCount++;
        }

        for (int i = 0; i < 4; i++)
        {
            if (wheelColliders[i].isGrounded)
            {
                JumpCount = 0;
            }
        }


    }

    void FixedUpdate()
    {

        float steer = /*GetAxisH;*/Input.GetAxis("Horizontal");
        float accelerate = /*GetAxisV;*/Input.GetAxis("Vertical");
        //float steer = CrossPlatformInputManager.GetAxis("Horizontal");
        //float accelerate = (CrossPlatformInputManager.GetButton("Move") ? 1 : 0);

        float finalAngle = steer * 45f;
        wheelColliders[0].steerAngle = finalAngle;
        wheelColliders[1].steerAngle = finalAngle;
        if (accelerate != 0)
        {
                wheelColliders[0].motorTorque = accelerate * MoveForce;
                wheelColliders[1].motorTorque = accelerate * MoveForce;
        }
        else
        {
            wheelColliders[0].motorTorque = 0;
            wheelColliders[1].motorTorque = 0;
        }

    }

    void UpdateMeshesPositions()
    {
        for(int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vartai"))
        {
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        //countText.text = "Palietimų kiekis: " + count.ToString();
        //Debug.Log("Palietimų kiekis: " + count.ToString());
    }

}