using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGrow : MonoBehaviour
{
    public float timer = 10f;

    //public float maxSize = 20f;
    //public bool growing = false;

    public void Start()
    {
        Invoke("Melt", 7f);
    }
    //private void Update()
    //{
    //    Debug.Log(timer);
    //    timer -= 0.01f;
    //    if (timer <= 0)
    //        Destroy(gameObject);
    //}

    private void Melt()
    {
        Destroy(gameObject);
    }
    //private IEnumerator Grow()
    //{
    //    yield return null;
    //    //while (growing)
    //    //{
    //    //    timer+= Time.deltaTime; 
    //    //}
    //    //Vector3 startScale = transform.localScale;
    //    //Vector3 startPos = transform.localPosition;
    //    //Vector3 maxScale = new Vector3(2, (0.05f * timer), 2);
    //    //Vector3 maxPos = new Vector3(startPos.x, (0.025f * timer), startPos.z);
    //    //transform.localScale = Vector3.Lerp(startScale, maxScale, timer);
    //    //transform.localScale = Vector3.Lerp(startPos, maxPos, timer);
    //    //yield return null;

    //    //Vector3 startScale = transform.localScale;
    //    //Vector3 maxScale = new Vector3(2, maxSize, 2);
    //    //do
    //    //{
    //    //    transform.localScale = Vector3.Lerp(startScale, maxScale, 1);
    //    //    timer += Time.deltaTime;
    //    //    yield return null;

    //    //} while (timer < 6 && growing == true);
    //    //growing = false;
    //}
}