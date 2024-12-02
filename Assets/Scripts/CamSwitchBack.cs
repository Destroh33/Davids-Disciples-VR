using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchBack : MonoBehaviour
{
    [SerializeField] GameObject ImageUI;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SwitchBack()
    {
        ImageUI.GetComponent<Animator>().SetTrigger("change");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
