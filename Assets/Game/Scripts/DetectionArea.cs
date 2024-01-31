using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public string targetDetection = "Player";

    public List<Collider2D> detectCol = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == targetDetection)
        {
            detectCol.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == targetDetection)
        {
            detectCol.Remove(other);
        }
    }
}
