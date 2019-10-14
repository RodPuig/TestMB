using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeWallBox : MonoBehaviour
{
    public float rotationSpeed;

    float angle;
    

    // Start is called before the first frame update
    void Start()
    {
        angle = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;

        if (angle < 0)
            angle += 360;
        else if (angle > 360)
            angle -= 360;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
