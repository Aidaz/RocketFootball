using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    //public GameObject player;

    //private Vector3 offset;

    //void Start()
    //{
    //    offset = transform.position - player.transform.position;
    //}

    //void LateUpdate()
    //{
    //    transform.position = player.transform.position + offset;
    //}

    //public GameObject player;

    // Update is called once per frame
    //public float xOffset = 0;
    //public float yOffset = 0;
    //public float zOffset = 0;

    //void LateUpdate()
    //{
    //    this.transform.position = new Vector3(player.transform.position.x + xOffset,
    //                                          player.transform.position.y + yOffset,
    //                                          player.transform.position.z + zOffset);
    //}
    public Transform target;
    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public float rotationDamping = 10.0f;

    void Update()
    {
        Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        if (smoothRotation)
        {
            //Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }

        else transform.LookAt(target, target.up);
    }
}