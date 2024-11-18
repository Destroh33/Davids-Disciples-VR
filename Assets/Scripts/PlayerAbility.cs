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

    bool abilityActive;
    //Ray ray;
    //0 - none
    //1 - ice
    // 2 - earth 
    Camera cam;
    private GameObject grabbedObject = null; 
    private Vector3 grabOffset; 
    private bool isHolding = false;  

    void Start()
    {
        cam = Camera.main;
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
        switch (abilityVal)
        {
            case 0:
                break;
            case 1: //ice
                IceGrow currIce = null;
                abilityActivate.started += _ => {
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
                                                        
                        }
                    }  
                };
                break; 
                case 2:
                case 4:
                {
                    abilityActivate.started += _ => {
                        Debug.Log("earth");
                        OnGrab();
                    };
                    abilityActivate.canceled += _ => OnLetGo();

                };
                break;
            
        }

        
        
 
        
    }

    void Update()
    {
        if (isHolding && grabbedObject != null)
        {
            Vector3 targetPosition = melee.transform.position /** 3.2f*/; 
            Vector3 positionDiff = targetPosition-grabbedObject.transform.position;
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.AddForce(positionDiff*4.0f/* (cam.transform.forward - (grabOffset - cam.transform.position))*/);
            if (positionDiff.x < 0.2 && positionDiff.x > -0.2 && positionDiff.y < 0.2 && positionDiff.y > -0.2 && positionDiff.z < 0.2 && positionDiff.z > -0.2)
            {
                rb.velocity = new Vector3(0, 0, 0);
                if (grabbedObject.transform.name =="Bowl")
                {
                    if (grabbedObject.GetComponent<BowlScript>().isWallCollide() == false)
                    {
                        grabbedObject.transform.position = targetPosition;
                    }
                }
                else if (grabbedObject.transform.name == "crabComplex")
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
        
    }

    void Hold(InputAction.CallbackContext context) { 

    }

    void OnAbility()
    {
        Debug.Log("ability");
        if (abilityVal == 1) {
            Debug.Log("ice");
            int layerMask = (1 << 4) + 1;
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.gameObject.layer == 4)
                {
                    Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red, 10);
                    Debug.Log("ray hit");
                    Instantiate(ice, hit.point, this.transform.rotation);
                }
            }
            else
            {
                Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000, Color.yellow, 10);
                Debug.Log("ray miss!");
            }
            // Debug.DrawRay(cam.transform.position,cam.transform.forward,Color.red,10);    
        }
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
                   // Debug.Log("Hit: " + hit.transform.name);
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
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            grabbedObject = null;
            isHolding = false;
      //      Debug.Log("let go");
        }

    }

}

