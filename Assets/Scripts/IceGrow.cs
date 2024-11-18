using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class IceGrow : MonoBehaviour
{
    [SerializeField] float timer;
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("Melt", timer);
    }

    private void Melt()
    {
        Destroy(gameObject);
    }
}

    