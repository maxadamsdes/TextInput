using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shake : MonoBehaviour
{
    public GameObject idol;
    private Quaternion calibrationQuaternion;
    public bool shaken = false;
    private bool oscillate;
    private void Start()
    {
        CalibrateAccelerometer();
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.acceleration.x > 5) || (Input.acceleration.y > 5))
        {
            shaken = true;
            idol.GetComponent<Image>().sprite = Resources.Load("marker_statue4") as Sprite;
        }

        if ((shaken == true) && (transform.position.y > -10))
        {
            idol.GetComponent<Image>().sprite = Resources.Load("marker_statue4") as Sprite;
            float dropping = transform.position.y - 0.1f;
            float shakingX = transform.position.x;
            if (oscillate == true)
            {
                oscillate = false;
                shakingX = shakingX + 0.1f;
            }
            else
            {
                oscillate = true;
                shakingX = shakingX - 0.1f;
            }
            
            transform.position = new Vector3(shakingX, dropping, transform.position.z);
        }
        
    }

    // Used to calibrate the Input.acceleration
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;

        Quaternion rotateQuaternion = Quaternion.FromToRotation(
            new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);

        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }
}
