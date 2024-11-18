using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject fire2;
    [SerializeField] GameObject fire3;
    bool isBurning = false;
    // Start is called before the first frame update
    void Start()
    {
        fire.SetActive(false);
        fire2.SetActive(false);
        fire3.SetActive(false);
    }
    public void Burn()
    {
        fire.SetActive(true);
        fire2.SetActive(true);
        fire3.SetActive(true);
        isBurning = true;
    }
    public bool GetBurning()
    {
        return isBurning;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
