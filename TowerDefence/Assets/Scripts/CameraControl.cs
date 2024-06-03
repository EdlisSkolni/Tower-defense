using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float speed = 30f;
    private bool[] outOfRange = {false, false, false, false}; //w,a,s,d
    private Vector3 v3;

    [HeaderAttribute("Borders")]
    public GameObject[] borders;

    void Update()
    {
        if (Input.GetKey("w") && !outOfRange[0])
        {
            v3 = new Vector3(1f,0,-0.5f);
            transform.Translate(v3 * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") && !outOfRange[1])
        {
            v3 = new Vector3(1, 0, 1.5f);
            transform.Translate(v3 * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") && !outOfRange[2])
        {
            v3 = new Vector3(-1f, 0, 0.5f);
            transform.Translate(v3 * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") && !outOfRange[3])
        {
            v3 = new Vector3(-1, 0, -1.5f);
            transform.Translate(v3 * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == borders[0])//Border (Foward)
        {
            outOfRange[0] = true;
        }
        if (other.gameObject == borders[1])//Boder (Back)
        {
            outOfRange[2] = true;
        }
        if (other.gameObject == borders[2])//Boder (Left)
        {
            outOfRange[1] = true;
        }
        if (other.gameObject == borders[3])//Boder (Right)
        {
            outOfRange[3] = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == borders[0])//Border (Foward)
        {
            outOfRange[0] = false;
        }
        if (other.gameObject == borders[1])//Boder (Back)
        {
            outOfRange[2] = false;
        }
        if (other.gameObject == borders[2])//Boder (Left)
        {
            outOfRange[1] = false;
        }
        if (other.gameObject == borders[3])//Boder (Right)
        {
            outOfRange[3] = false;
        }
    }
}