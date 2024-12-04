using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingScript : MonoBehaviour
{
    private bool crabbed = false;
    private bool cooked = false;
    [SerializeField] GameObject log;
    [SerializeField] GameObject water;
    [SerializeField] GameObject key;
    [SerializeField] GameObject soup;
    private bool wallCollide = false;

    public AudioSource dropCrab;
    public AudioSource cookSoup;
    // Start is called before the first frame update
    void Start()
    {
        soup.SetActive(false);
        key.SetActive(false);
        dropCrab = GameObject.Find("Drop Crab").GetComponent<AudioSource>();
        cookSoup = GameObject.Find("Cooking Soup").GetComponent<AudioSource>();
    }
    public bool isWallCollide()
    {
        return wallCollide;
    }

    // Update is called once per frame
    void Update()
    {
        if (log.GetComponent<BurnScript>().GetBurning() && crabbed)
        {
            gameObject.tag = "Grabbable";
            //Destroy(collision.gameObject);
            cooked = true;
            water.SetActive(false);
            soup.SetActive(true);
            Debug.Log("cooked the crab");
            if(cookSoup != null)
            {
                cookSoup.Play();
            }
            crabbed = false;
        }
    }
    public bool GetCooked()
    {
        return cooked;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crab"))
        {
            //GetComponent<Rigidbody>().isKinematic = false;
            Destroy(collision.gameObject);
            if (dropCrab != null)
            {
                dropCrab.Play();
            }
            crabbed = true;
        }
        
        if (collision.gameObject.CompareTag("FireGate") && cooked)
        {
            key.SetActive(true);
            if(cookSoup != null && cookSoup.isPlaying)
            {
                cookSoup.Stop();
            }
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
