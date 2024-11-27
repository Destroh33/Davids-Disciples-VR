using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;
    [SerializeField] public Vector2 sensitivity;
    private Vector2 rotation;
    [SerializeField] private float maxVertAngle;
    [SerializeField] Transform t;
    [SerializeField] private ButtonKey buttonKey;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  
        }
    }
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

    public void SetSensitivity(float x, float y)
    {
        sensitivity.x = x;
        sensitivity.y = y;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = t.position;
        rotation += GetMouseInput() * sensitivity;
        // GameObject optionsMenu = GameObject.Find("Options");
        if (buttonKey.OptionsUp)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (!buttonKey.OptionsUp)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
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
