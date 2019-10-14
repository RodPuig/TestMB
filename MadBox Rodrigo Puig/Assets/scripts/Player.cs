using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float speed;

    [Header("Depencencies")]
    public GameManager gm;
    
    Vector3 direction;

    bool pressed;

    private void Awake()
    {
        pressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            pressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
            pressed = false;
    }

    void FixedUpdate()
    {
        if (pressed)
            GetComponent<Rigidbody>().velocity = direction * speed;
        else
            GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;

        Debug.Log(_tag);

        if(_tag == "Obstacle")
        {
            gm.Reload();
            direction = Vector3.forward;

            if (direction.y == 0 && transform.forward != direction)
            {
                transform.LookAt(transform.position + direction * 1f);
                gm.RedirectCamera();
            }
        }
        else if(_tag == "Goal")
        {
            gm.Goal();
        }
        else if(_tag == "LateralCamera")
        {
            gm.SetLateralCamera();
        }
        else if(_tag == "CenitalCamera")
        {
            gm.SetCenitalCamera();
        }
        else if(_tag == "backCamera")
        {
            gm.SetBackCamera();
        }
        else if(_tag == "ChangeDirection")
        {
            direction = other.transform.forward;
            if (direction.y == 0 && transform.forward != direction)
            {
                transform.LookAt(transform.position + direction * 1f);
                gm.RedirectCamera();
            }
        }
    }
}
