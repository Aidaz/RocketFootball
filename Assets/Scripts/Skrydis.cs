using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skrydis : MonoBehaviour {
    public Rigidbody rb;
    public float strength;
	
    private int count;
    public Text countText;
    public Text countText1;

    public Quaternion originalRotationValue; // declare this as a Quaternion
    public Vector3 originalPositionValue; // declare this as a Quaternion
    float rotationResetSpeed = 1.0f;

    void Start()
    {
        count = 0;
        countText.text = "";
        countText1.text = "";
        originalRotationValue = transform.rotation; // save the initial rotation
        originalPositionValue = transform.position; // save the initial rotation
}
    // Update is called once per frame
    void Update () {
        rb.velocity = new Vector3(0, -20, 0);
        //rb.AddForce(transform.forward * strength);
        if (Input.GetKeyDown("p") && countText.text.CompareTo("") != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed);
            transform.position = Vector3.Slerp(transform.position, originalPositionValue, Time.time * rotationResetSpeed);
            countText.text = "";
            countText1.text = "";
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Vartai"))
        {
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Car"))
        {
            float liftValue = 100;  // Tweak this to get the right effect for the game.
            float liftForce = liftValue * -Vector3.Dot(transform.up, rb.velocity);
            rb.AddRelativeForce(Vector3.up * liftForce);
        }
    }
    void SetCountText()
    {
        countText.text = "Įvartis!!!!";
        countText1.text = "Spauskite P, kad kamuolį gražintumėte į vietą.";
        //Debug.Log(count.ToString());
    }
}
