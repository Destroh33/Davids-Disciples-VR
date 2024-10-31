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
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
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

}
