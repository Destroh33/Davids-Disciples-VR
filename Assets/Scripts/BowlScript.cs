using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool cooked = false;
    [SerializeField] GameObject bowl;
    [SerializeField] GameObject key;
    [SerializeField] GameObject soup;
    private bool wallCollide = false;
    void Start()
    {
        key.SetActive(false);
        soup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isWallCollide()
    {
        return wallCollide;
    }
    public void makeCooked()
    {
        cooked = true;
        soup.SetActive(true);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FireGate")&&cooked)
        {
            key.SetActive(true);
            bowl.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            wallCollide = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            wallCollide = false;
        }
    }
}
