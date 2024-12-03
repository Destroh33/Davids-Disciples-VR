using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject entity;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerUI;
    public Slider slider;
    bool playerBool = false;
    void Start()
    {
        if(entity.transform.name == "Player")
        {
            playerBool = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerBool &&playerUI.activeSelf)
        {
            this.transform.LookAt(Camera.main.transform);
        }
        slider.value = entity.GetComponent<EntityHealthAndDmg>().GetHealth() / 2.0f;
    }
}
