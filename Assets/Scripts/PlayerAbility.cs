using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] int abilityVal = 1;
    [SerializeField] IceGrow ice;
    [SerializeField] InputAction abilityActivate;
    [SerializeField] GameObject melee;
    [SerializeField] GameObject dave;
    Animator animator;
    [SerializeField] GameObject fire1;
    [SerializeField] GameObject fire2;
    public AudioSource iceSound;
    public AudioSource earthSound; 
    public AudioSource waterFalls; 
    public AudioSource forestSounds; 

    bool abilityActive,cooldown = true;
    //0 - none
    //1 - ice
    // 2 - earth 
    // 3 - fire
    // 4 - air
    Camera cam;
    private GameObject grabbedObject = null; 
    private Vector3 grabOffset; 
    private bool isHolding = false;  

    void Start()
    {
        animator = dave.GetComponent<Animator>();
        cam = Camera.main;
        iceSound = GameObject.Find("Ice Ability").GetComponent<AudioSource>();
        earthSound = GameObject.Find("Earth Ability").GetComponent<AudioSource>();
        waterFalls = GameObject.Find("Water Stream").GetComponent<AudioSource>();
        forestSounds = GameObject.Find("Forest Sounds").GetComponent<AudioSource>();
    }

    public int GetAbilityVal()
    {
        return abilityVal;
    }
    public void SetAbilityVal(int val)
    {
        abilityVal = val;
    }

    private void Awake()
    {
        abilityActivate.Enable();      
    }
    private void Update()
    {
        switch (abilityVal)
        {
            case 0:
                if (waterFalls.isPlaying)  
                {
                    waterFalls.Stop();
                }
                if(forestSounds.isPlaying)
                {
                    forestSounds.Stop();
                }
                fire1.SetActive(false);
                fire2.SetActive(false);

                break;
            case 1: //ice
                IceGrow currIce = null;
                if (waterFalls != null && abilityVal == 1)
                 {
                    if (!waterFalls.isPlaying)  
                    {
                        waterFalls.loop = true;  
                        waterFalls.Play();        
                    }
                }
                if (abilityActivate.triggered)
                {
                    if (cooldown)
                    {
                        animator.SetTrigger("cast");
                        cooldown = false;
                        Debug.Log("ice");
                        int layerMask = (1 << 4) + 1;

                        RaycastHit hit;
                        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, layerMask))
                        {
                            if (hit.transform.gameObject.layer == 4)
                            {
                                Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red, 10);
                                Debug.Log("ray hit");
                                currIce = Instantiate(ice, hit.point, this.transform.rotation);
                                if (iceSound != null)
                                {
                                    iceSound.Play();
                                }

                            }
                        }
                        Invoke("resetCooldown", 1f);
                    }
                }
                break;
            case 4:

                fire1.SetActive(true);
                fire2.SetActive(true);
                goto case 2; //gross!
            case 2:
                {
                    if (forestSounds != null && abilityVal == 2)
                 {
                    if (!forestSounds.isPlaying)  
                    {
                        forestSounds.loop = true;  
                        forestSounds.Play();        
                    }
                }
                    
                    if (abilityActivate.ReadValueAsObject() != null && !isHolding)
                    {
                        
                        //Debug.Log("EARTH");
                        OnGrab();
                    }
                    else if(abilityActivate.ReadValueAsObject()  == null && isHolding)
                    {
                        
                        OnLetGo();
                    }
                };
                break;

        }
    }
    void FixedUpdate()
    {
        if (isHolding && grabbedObject != null)
        {
            Vector3 targetPosition = melee.transform.position /** 3.2f*/; 
            Vector3 positionDiff = targetPosition-grabbedObject.transform.position;
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.velocity = positionDiff*4.0f/* (cam.transform.forward - (grabOffset - cam.transform.position))*/;
            if (positionDiff.x < 0.2 && positionDiff.x > -0.2 && positionDiff.y < 0.2 && positionDiff.y > -0.2 && positionDiff.z < 0.2 && positionDiff.z > -0.2)
            {
                rb.velocity = new Vector3(0, 0, 0);
                Debug.Log(grabbedObject.transform.name);
                /*if (grabbedObject.transform.name =="Cauldron")
                {
                    if (grabbedObject.GetComponent<CookingScript>().isWallCollide() == false)
                    {
                        grabbedObject.transform.position = targetPosition;
                    }
                }
                else*/ if (grabbedObject.transform.name == "crabComplex")
                {
                    if (grabbedObject.GetComponent<FireCrabMoveScript>().isWallCollide() == false)
                    {
                        grabbedObject.transform.position = targetPosition;
                    }
                }
                else
                {
                    grabbedObject.transform.position = targetPosition;
                }
            }
        }
        if(grabbedObject == null && isHolding)
        {
            animator.SetBool("ecast", false);
            isHolding = false;
        }
        
    }

    void Hold(InputAction.CallbackContext context) { 

    }
    private void OnGrab(){
        if (abilityVal == 2 || abilityVal == 4)
        {
           // Debug.Log("i am grabbing");
            int layerMask = 1 << 3;
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("Grabbable")|| hit.transform.gameObject.CompareTag("Crab")) 
                {
                    animator.SetBool("ecast", true);
                    // Debug.Log("Hit: " + hit.transform.name);
                    if(earthSound != null && hit.transform.gameObject.CompareTag("Grabbable"))
                    {
                        earthSound.Play();
                    }
                    grabbedObject = hit.transform.gameObject;
                    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                    grabOffset = rb.transform.position;
                    //grabOffset = rb.transform.position - hit.point; 
                    isHolding = true;
                    //rb.isKinematic = true;
                    rb.useGravity = false;
        

               //     Debug.Log("grabbed");
                }
            }
        }
    }
    private void OnLetGo(){
        if (isHolding && grabbedObject != null)
        {
            animator.SetBool("ecast", false);
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            grabbedObject = null;
            isHolding = false;
      //      Debug.Log("let go");
        }

    }
    void resetCooldown()
    {
        cooldown = true;
    }
}

