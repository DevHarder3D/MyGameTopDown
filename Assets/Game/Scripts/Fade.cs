using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public string targetDetection = "Player";
    private SpriteRenderer sr;
    public List<Collider2D> col = new List<Collider2D>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(col.Count > 0)
        {
            sr.color = new Color(255, 255, 255, 0.4f);
        }
        else
        {
            sr.color = new Color(255, 255, 255, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == targetDetection )
        {
            col.Add(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == targetDetection)
        {
            col.Remove(other);
        }
    }
}
