using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlots : MonoBehaviour
{
   public bool isOccupied = false;
   public GameObject puzzlePiece = null;
// maybe later want to make it so that the puzzle pieces snap in place and arent moveable ? 
// also change the tag to compare to actual puzzle slots since there will be multiple grabbable things
   private void OnTriggerEnter(Collider other)
   {
    if (other.CompareTag("Grabbable") && !isOccupied)
    {
        puzzlePiece = other.gameObject;
        isOccupied = true;
        puzzlePiece.transform.position = transform.position;
        puzzlePiece.transform.rotation = transform.rotation;
        puzzlePiece.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("weeeheee puzzle piece placed");
    }
   }
// can add check here later to check if all slots have been activated
   private void OnTriggerExit(Collider other)
   {
     if (other.CompareTag("Grabbable"))
        {
            isOccupied = false;
            puzzlePiece = null;
            Debug.Log("puzzle gone");
        }
   }
}
