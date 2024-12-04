using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleSlots : MonoBehaviour
{
   public bool isOccupied = false;
   public GameObject puzzlePiece = null;
   [SerializeField] GameObject correctPiece; 
   public AudioSource correctSound;

   void Start(){
    correctSound = GameObject.Find("Correct Puzzle").GetComponent<AudioSource>();
   }
   private void OnTriggerEnter(Collider other)
   {
    if (other.gameObject == correctPiece && !isOccupied)
    {
        puzzlePiece = other.gameObject;
        isOccupied = true;
        if (correctSound != null)
        {
            correctSound.Play();
        }
        puzzlePiece.transform.position = transform.position;
        puzzlePiece.transform.rotation = transform.rotation;
        puzzlePiece.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("weeeheee puzzle piece placed");
    } 
   }

}
