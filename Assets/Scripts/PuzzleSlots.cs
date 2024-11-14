using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleSlots : MonoBehaviour
{
   public bool isOccupied = false;
   public GameObject puzzlePiece = null;
   [SerializeField] GameObject correctPiece;  
   private PuzzleManager puzzleManager;
   private void Start()
   {
      puzzleManager = FindObjectOfType<PuzzleManager>();
   }
   private void OnTriggerEnter(Collider other)
   {
    if (other.gameObject == correctPiece && !isOccupied)
    {
        puzzlePiece = other.gameObject;
        isOccupied = true;
        puzzlePiece.transform.position = transform.position;
        puzzlePiece.transform.rotation = transform.rotation;
        puzzlePiece.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("weeeheee puzzle piece placed");
    }
// use this later to seeif puzzle is complete and output a key 
         if (puzzleManager.puzzleComplete())
        {
            Debug.Log("Puzzle complete");
        }
   }

}
