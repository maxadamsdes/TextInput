using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 offset;
    //public Transform player;
    //public float cameraDistance = 50.0f;
    //public float screenHeight = 20;
    //public double screenDisplacement = -2.5;

    void Awake()
    {
        //GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / screenHeight) / cameraDistance);
        offset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        //transform.position = new Vector3(player.position.x, -1, transform.position.z);
        transform.position = player.transform.position + offset;
    }
}
