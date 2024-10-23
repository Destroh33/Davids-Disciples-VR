using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Vector2 sensitivity;
    private Vector2 rotation;
    [SerializeField] private float maxVertAngle;
    [SerializeField] Transform t;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector2(0, 0);
    }
    private Vector2 GetMouseInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        return input;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = t.position;
        rotation += GetMouseInput() * sensitivity;
        while (rotation.x >= 360.00f)
        {
            rotation.x -= 360.00f;
        }
        while (rotation.x < 0.00f)
        {
            rotation.x += 360.00f;
        }
        if (rotation.y > 90)
        {
            rotation.y = 90;
        }
        else if (rotation.y < -90)
        {
            rotation.y = -90;
        }
        transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
    }
}
