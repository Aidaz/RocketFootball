using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour {

    //public float speed;
    //private Rigidbody rb;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    //void FixedUpdate()
    //{


    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //    rb.AddForce(movement * speed);


    //}

    //void OnTriggerEnter(Collider other)
    //{

    //}

    int JumpCount = 0;
    public int JumpForce;
    public int MoveForce;
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];

    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    //public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    //public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    // public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    // public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.


    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void Start()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";// + m_PlayerNumber;
        m_TurnAxisName = "Horizontal";// + m_PlayerNumber;
    }


    private void Update()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        if (Input.GetKeyDown("space") && m_Rigidbody.transform.position.y <= 0.5f)
        {
            Vector3 jump = new Vector3(0.0f, 2000.0f, 0.0f);

            m_Rigidbody.AddForce(jump);
        }
    }



    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }


    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        if (movement != null)
        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }

}