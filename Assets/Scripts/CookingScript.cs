using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingScript : MonoBehaviour
{
    private bool cooked = false;
    [SerializeField] GameObject log;
    [SerializeField] GameObject water;
    [SerializeField] GameObject soup;
    // Start is called before the first frame update
    void Start()
    {
        soup.SetActive(false);
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
        if (collision.gameObject.CompareTag("Grabbable")&&cooked){
            collision.gameObject.GetComponent<BowlScript>().makeCooked();
            Debug.Log("cooked the bowl");
        }
    }
}
