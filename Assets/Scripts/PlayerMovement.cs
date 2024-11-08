using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody rb;
    [SerializeField] int speed = 0;
    [SerializeField] private Vector2 sensitivity;
    private Vector2 rotation;
    [SerializeField] private float maxVertAngle;
    private Vector2 tempVec;
    [SerializeField] GameObject melee;
    float time;
    float lastSwingTime;
    bool grounded = true;
    public int keyCount = 0;
    //[SerializeField] Transform tr;
    // Start is called before the first frame update

    private Vector2 GetMouseInput()
    {

        Vector2 input = new Vector2(Input.GetAxis("Mouse X") , Input.GetAxis("Mouse Y"));
        return input;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        melee.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rotation = new Vector2(0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    void OnMove(InputValue value)
    {
        tempVec = value.Get<Vector2>();
        //Debug.Log(tempVec);
    }
    void OnJump()
    {
        if (grounded)
        {
            rb.AddForce(new Vector3(0, 200, 0));
        }
    }
    void OnMelee()
    {
        melee.SetActive(true);
        lastSwingTime = time;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            keyCount++;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 4)
        {
            grounded = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            grounded = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.forward = new Vector3(tr.forward.x, 0, tr.forward.z);
        time += Time.deltaTime;
        if(time> lastSwingTime + 0.05f)
        {
            melee.SetActive(false);
        }
        rotation += GetMouseInput() * sensitivity;
        transform.localEulerAngles = new Vector3(0,rotation.x, 0);
        rb.velocity = new Vector3(speed * movementVector.x, rb.velocity.y, speed * movementVector.y);

        if (tempVec.x > 0.8)
        {//to the right movement
            movementVector = new Vector2(Mathf.Sin((Mathf.PI / 2) + (rotation.x * Mathf.PI / 180)), Mathf.Cos((Mathf.PI / 2) + rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.x < -0.8)
        {//to the left movement
            movementVector = new Vector2(-Mathf.Sin((Mathf.PI / 2) + (rotation.x * Mathf.PI / 180)), -Mathf.Cos((Mathf.PI / 2) + rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.y > 0.8)
        {//forward movement
            movementVector = new Vector2(Mathf.Sin(rotation.x * Mathf.PI / 180), Mathf.Cos(rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.y < -0.8)
        {//backward movement
            movementVector = new Vector2(-Mathf.Sin(rotation.x * Mathf.PI / 180), -Mathf.Cos(rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.x < -0.5 && tempVec.x >-0.8 && tempVec.y<-0.5 && tempVec.y >-0.8)
        {//back and left movement
            movementVector = new Vector2(-Mathf.Sin((Mathf.PI / 4) + (rotation.x * Mathf.PI / 180)), -Mathf.Cos((Mathf.PI / 4) + rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.x < -0.5 && tempVec.x >-0.8 && tempVec.y>0.5 && tempVec.y <0.8)
        {//front and left movement
            movementVector = new Vector2(Mathf.Sin((-Mathf.PI / 4) + (rotation.x * Mathf.PI / 180)), Mathf.Cos((-Mathf.PI / 4) + rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.x > 0.5 && tempVec.x < 0.8 && tempVec.y < -0.5 && tempVec.y > -0.8)
        {//back and right movement
            movementVector = new Vector2(-Mathf.Sin((-Mathf.PI / 4) + (rotation.x * Mathf.PI / 180)), -Mathf.Cos((-Mathf.PI / 4) + rotation.x * Mathf.PI / 180));
        }
        else if (tempVec.x > 0.5 && tempVec.x < 0.8 && tempVec.y > 0.5 && tempVec.y < 0.8)
        {//front and right movement
            movementVector = new Vector2(Mathf.Sin((Mathf.PI / 4) + (rotation.x * Mathf.PI / 180)), Mathf.Cos((Mathf.PI / 4) + rotation.x * Mathf.PI / 180));
        }
        else
        {
            movementVector = new Vector2(0, 0);
        }
    }
}
