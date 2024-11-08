using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] int abilityVal = 1;
    [SerializeField] IceGrow ice;
    [SerializeField] InputAction abilityActivate;

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
                {
                    abilityActivate.started += _ => {
                        Debug.Log("earth");
                        OnGrab();
                    };
                    abilityActivate.canceled += _ => OnLetGo();

                };
                //abilityActivate.performed += _ => { Debug.Log("ice grow max!!!!!!"); currIce.growing = false; };
                //abilityActivate.canceled += _ => { Debug.Log("premature!!"); currIce.growing = false; };
                break;
            
        }

        
        
 
        
    }

    //private IEnumerator Grow(GameObject ice)
    //{
    //    abilityActive = true;
    //    while (abilityActive)
    //    {
    //        Debug.Log("growingngggg");
    //        //float x = 0.5f;
    //        //ice.transform.localScale = new Vector3(2f, x, 2f);
    //        //x += 0.05f;
    //        ////Invoke("mehtod", 0.05f);
    //        ////Time.
    //    }
    //    yield return null;
    //}
    // Update is called once per frame
    void Update()
    {
        if (isHolding && grabbedObject != null)
        {
            Vector3 targetPosition = cam.transform.position + cam.transform.forward * 3.2f; 
            grabbedObject.transform.position = targetPosition + grabOffset;
        }
        //transform.rotation = new Qua
           //transform.LookAt()
        
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
// have to make it so that when colliding with the ground it turns kinematic again or else it passes thru the ground 
// this does not work yet for some reason 
private void OnCollisionEnter(Collision collision)
{
    int groundLayer = 6; 
    if (collision.gameObject.layer == groundLayer && isHolding)
    {
        Debug.Log("hit ground");
        
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}

private void OnCollisionExit(Collision collision)
{
    int groundLayer = 6;

    if (collision.gameObject.layer == groundLayer && isHolding)
    {
        Debug.Log("no longer on ground");
        
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
    private void OnGrab(){
        if (abilityVal == 2)
        {
           // Debug.Log("i am grabbing");
            int layerMask = 1 << 3;
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                // change to include tag of puzzle pieces later
                if (hit.transform.gameObject.CompareTag("Grabbable")) 
                {
                    Debug.Log("Hit: " + hit.transform.name);
                    grabbedObject = hit.transform.gameObject;
                    grabOffset = grabbedObject.transform.position - hit.point; 
                    isHolding = true;
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
               //     Debug.Log("grabbed");
                }
            }
        }
    }
    private void OnLetGo(){
        if (isHolding && grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
            isHolding = false;
      //      Debug.Log("let go");
        }

    }

}

