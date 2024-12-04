using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    //[SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text keyText;
    [SerializeField] Image fire;
    [SerializeField] Image water;
    [SerializeField] Image air;
    [SerializeField] Image earth;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //healthText.text = "Health: " + player.GetComponent<EntityHealthAndDmg>().GetHealth();
        keyText.text = ""+player.GetComponent<PlayerMovement>().keyCount;
        if(player.GetComponent<PlayerAbility>().GetAbilityVal() == 0)
        {
            fire.enabled = false;
            water.enabled = false;
            earth.enabled = false;
            air.enabled = false;
        }else if (player.GetComponent<PlayerAbility>().GetAbilityVal() == 1)
        {
            fire.enabled = false;
            water.enabled = true;
            earth.enabled = false;
            air.enabled = false;
        }
        else if (player.GetComponent<PlayerAbility>().GetAbilityVal() == 2)
        {
            fire.enabled = false;
            water.enabled = false;
            earth.enabled = true;
            air.enabled = false;
        }
        else if (player.GetComponent<PlayerAbility>().GetAbilityVal() == 3)
        {
            fire.enabled = false;
            water.enabled = false;
            earth.enabled = false;
            air.enabled = true;
        }
        else if (player.GetComponent<PlayerAbility>().GetAbilityVal() == 4)
        {
            fire.enabled = true;
            water.enabled = false;
            earth.enabled = false;
            air.enabled = false;
        }
    }
}
