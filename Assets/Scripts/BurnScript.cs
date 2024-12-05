using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject fire2;
    [SerializeField] GameObject steam;
    [SerializeField] bool destructible;
    bool isBurning = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Burn()
    {
        fire.SetActive(true);
        fire2.SetActive(true);
        if(steam != null ) steam.SetActive(true);
        isBurning = true;
        if (destructible)
            Invoke("Burnt", 5f);
            //Destroy();
    }
    public bool GetBurning()
    {
        return isBurning;
    }
    // Update is called once per frame
   private void Burnt()
    {
        Debug.Log("burnt");
        Destroy(gameObject);
    }
}
