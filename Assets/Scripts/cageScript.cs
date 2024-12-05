using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cageScript : MonoBehaviour
{
    [SerializeField] bossBehavior bossBehavior;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bossBehavior.isDead())
        {
            Debug.Log("YOU DA WINNAR!!! YYOU ARE SUPER PLAYER");
            SceneManager.LoadScene("Victory!!");
        }
    }
}
