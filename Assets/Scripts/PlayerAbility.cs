using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] int abilityVal = 1;
    [SerializeField] GameObject ice;
    //Ray ray;
    //0 - none
    //1 - ice
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAbility()
    {
        Debug.Log("ability");
        if (abilityVal == 1) {
            Debug.Log("ice");
            int layerMask = 0b1001;
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red, 10);
                Debug.Log("ray hit");
                Instantiate(ice, hit.point, this.transform.rotation);
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
