using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour
{
    [SerializeField] GameObject fire;
    bool isBurning = false;
    // Start is called before the first frame update
    void Start()
    {
        fire.SetActive(false);
    }
    public void Burn()
    {
        fire.SetActive(true);
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
