using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool cooked = false;
    [SerializeField] GameObject key;
    void Start()
    {
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void makeCooked()
    {
        cooked = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FireGate")&&cooked)
        {
            key.SetActive(true);
        }
    }
}
