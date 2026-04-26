using UnityEngine;
using System.Collections;

public class MainCameraWiggle : MonoBehaviour
{
    public float sensitivity = 0.05F;
    private float curX = 0;
    public float xLimit = 50;
    private float curY = 0;
    public float yLimit = 50;
    void Update()
    {
        float x = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
        float y =- Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1);
        if (curX <= xLimit && curX >= -xLimit && curY <= yLimit && curY >= -yLimit)
        {
            transform.Rotate(new Vector3(y, x) * sensitivity);
        }
        else if (curX > xLimit)
        {
            curX = xLimit;
        }
        else if (curX < -xLimit)
        {
            curX = -xLimit;
        }
        else if (curY > yLimit)
        {
            curY = yLimit;
        }
        else if (curY < -yLimit)
        {
            curY = -yLimit;
        }
        curX += Input.GetAxis("Mouse X");
        curY += Input.GetAxis("Mouse Y");
 
    }
}
