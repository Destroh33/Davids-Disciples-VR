using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    public Slider slider;
    bool playerBool = false;
    void Start()
    {
        if(player.transform.name == "Player")
        {
            playerBool = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerBool)
        {
            this.transform.LookAt(Camera.main.transform);
        }
        slider.value = player.GetComponent<EntityHealthAndDmg>().GetHealth() / 2.0f;
    }
}
