using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAnimation : MonoBehaviour
{
    public float rotationSpeed = 80.0f;
    public string rotationAxis = "x";

    // Update is called once per frame
    void Update()
    {
        if (rotationAxis.Equals('x'))
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        }else if (rotationAxis.Equals("y"))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }else if(rotationAxis.Equals("z"))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Wrong Axis choosen, please select either x, y or z");
        }

    }
}
