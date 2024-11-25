using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingScript : MonoBehaviour
{
    private bool cooked = false;
    [SerializeField] GameObject log;
    [SerializeField] GameObject water;
    [SerializeField] GameObject key;
    [SerializeField] GameObject soup;
    private bool wallCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        soup.SetActive(false);
        key.SetActive(false);
    }
    public bool isWallCollide()
    {
        return wallCollide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetCooked()
    {
        return cooked;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crab") && log.GetComponent<BurnScript>().GetBurning())
        {
            Destroy(collision.gameObject);
            cooked = true;
            water.SetActive(false);
            soup.SetActive(true);
            Debug.Log("cooked the crab");
        }
        
        if (collision.gameObject.CompareTag("FireGate") && cooked)
        {
            key.SetActive(true);
        }
        /*
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            wallCollide = true;
        }
        */

    }
    /*
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            wallCollide = false;
        }
    }
    */
}
