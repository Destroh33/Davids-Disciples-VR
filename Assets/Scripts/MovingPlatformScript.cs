using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    // Start is called before the first frame update
    Transform t;
    bool forward;
    [SerializeField] GameObject melee;
    bool moving = true;
    void Start()
    {
        t = GetComponent<Transform>();
        if (t.position.x >= -88.81)
        {
            forward = false;
        }
        else
        {
            forward = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moving = melee.GetComponent<MeleeAttack>().isAirMoving();
        if (moving)
        {
            if (forward)
            {
                t.position = new Vector3(t.position.x + 0.1f, t.position.y, t.position.z);
            }
            else
            {
                t.position = new Vector3(t.position.x - 0.1f, t.position.y, t.position.z);
            }
            if (t.position.x >= -88.81)
            {
                forward = false;
            }
            else if (t.position.x <= -98.81)
            {
                forward = true;
            }
        }
    }
}
