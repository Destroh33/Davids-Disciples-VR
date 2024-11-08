using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthUIScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text keyText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.GetComponent<EntityHealthAndDmg>().GetHealth();
        keyText.text = "Keys: " + player.GetComponent<PlayerMovement>().keyCount;
    }
}
