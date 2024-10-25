using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetInfo : MonoBehaviour
{
    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }
    public Vector3 GetPos()
    {
        return t.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
