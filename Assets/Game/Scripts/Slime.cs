using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public ControlUI ctUI;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private int slimeLife = 3;
    [SerializeField] private float slimeMoveSpeed = 3.5f;
    [SerializeField] private Vector2 slimeDirection;
    [SerializeField] private DetectionArea detectionController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        slimeDirection = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (slimeDirection.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("Movement", 1);
        }
        if (slimeDirection.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetInteger("Movement", 1);
        }
        if (slimeMoveSpeed == 0.1f)
        {
            animator.SetInteger("Movement", 2);
            StartCoroutine(TimerToMove());
        }

        Die();
    }

    private void FixedUpdate()
    {
        if (detectionController.detectCol.Count > 0)
        {
            slimeDirection = (detectionController.detectCol[0].transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + slimeDirection * slimeMoveSpeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        slimeLife -= damage;
        slimeMoveSpeed = 0.1f;
    }

    public void ApplyDamage(GameObject other)
    {
        other.GetComponent<Player>().TakeDamage(1);
    }

    void Die()
    {
        if (slimeLife <= 0)
        {
            slimeMoveSpeed = 0;
            animator.SetInteger("Movement", 3);
            Destroy(this.gameObject, 0.71f);
        }
    }

    IEnumerator TimerToMove()
    {
        yield return new WaitForSeconds(0.5f);
        slimeMoveSpeed = 3.5f;
    }

    IEnumerator TimerToPlayerAttack()
    {
        yield return new WaitForSeconds(2);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyDamage(other.gameObject);
            StartCoroutine(TimerToPlayerAttack());
        } 
    }
}
