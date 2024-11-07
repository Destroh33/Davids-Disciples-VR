using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour
{
    [SerializeField] GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        fire.SetActive(false);
    }
    public void Burn()
    {
        fire.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
