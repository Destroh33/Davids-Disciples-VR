using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject Camera1;
    [SerializeField] GameObject Camera2;
    public int Manager;
    // Start is called before the first frame update
    public void ManageCam()
    {
        if (Manager == 0)
        {
            Cam1();
            Manager = 1;
        }
        else if (Manager == 1)
        {
            Cam2();
            Manager = 0;
        }
    }
    void Start()
    {
        ManageCam();
    }
    void Cam1()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }
    void Cam2()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
