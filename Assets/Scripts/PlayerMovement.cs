using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody rb;
    [SerializeField] int speed = 0;
    [SerializeField] private Vector2 sensitivity;
    private Vector2 rotation;
    [SerializeField] private float maxVertAngle;
    [SerializeField] GameObject player;
    private Vector2 tempVec;
    [SerializeField] GameObject melee;
    [SerializeField] GameObject dave;
    float time;
    float lastSwingTime;
    [SerializeField] private Slider sensitivitySlider;
    bool grounded = true,watered = false,meleecooldown = true;
    public int keyCount = 0;
    public AudioSource swingSword;
    Animator anim;
    //[SerializeField] Transform tr;
    // Start is called before the first frame update
    bool dubjump = true;
    private EntityHealthAndDmg playerHealth;
    public AudioSource keyGain; 

    private Vector2 GetMouseInput()
    {

        Vector2 input = new Vector2(Input.GetAxis("Mouse X") , Input.GetAxis("Mouse Y"));
        return input;
    }
    void Start()
    {
        anim = dave.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        melee.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rotation = new Vector2(0, 0);
        rb.velocity = new Vector3(0, 0, 0);
        playerHealth = GetComponent<EntityHealthAndDmg>();
        keyGain = GameObject.Find("Key Pickup").GetComponent<AudioSource>(); 
        swingSword = GameObject.Find("Swing Sword").GetComponent<AudioSource>();
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }
    private void OnSensitivityChanged(float value)
    {
        sensitivity.x = value;
        sensitivity.y = value;
        Debug.Log(sensitivity);
    }

    void OnMove(InputValue value)
    {
        tempVec = value.Get<Vector2>();
        //Debug.Log(tempVec);
    }
    void OnJump()
    {
        if (grounded && !watered)
        {
            rb.AddForce(new Vector3(0, 10000, 0));
        }
        if (!grounded && dubjump && player.GetComponent<PlayerAbility>().GetAbilityVal() == 3)
        {
            rb.AddForce(new Vector3(0, 10000, 0));
            dubjump = false;
        }
        if(watered)
            rb.AddForce(new Vector3(0, 2500, 0));

    }
    void OnMelee()
    {
        if (meleecooldown)
        {
            melee.SetActive(true);
            lastSwingTime = time;
            anim.SetTrigger("attack");
            swingSword.Play();
            meleecooldown = false;
            Invoke("ResetCooldown", 0.7f);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            dubjump = true;
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            if (keyGain != null)
            {
                keyGain.Play();
            }
            keyCount++;
            playerHealth.GainHealth();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            dubjump = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            dubjump = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == 4)
        {
            watered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.layer == 4)
        {
            watered = false;
        }
    }
        // Update is called once per frame
    void Update()
    {
        
        //transform.forward = new Vector3(tr.forward.x, 0, tr.forward.z);
        time += Time.deltaTime;
        if (player.GetComponent<PlayerAbility>().GetAbilityVal() == 2)
        {
            melee.SetActive(true);
        }
        if (time> lastSwingTime + 0.05f)
        {
            melee.SetActive(false);
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

    private void ResetCooldown()
    {
        meleecooldown = true;
    }
}
