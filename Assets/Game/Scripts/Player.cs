using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ControlUI ctUI;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer rbRenderer;
    private BlendTree bt;
    [SerializeField] private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float sprint;
    [SerializeField] private bool isSprint;
    [SerializeField] private Vector2 direction;
    [SerializeField] private bool isAttack;
    public int life = 400;
    [SerializeField] private bool invincible;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rbRenderer = GetComponent<SpriteRenderer>();
        bt = GetComponent<BlendTree>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        OnAttack();

        if (isAttack)
        {
            animator.SetInteger("Movement", 2);
        }

        Sprint();

        Die();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
        
        if (direction != Vector2.zero)
        {
            animator.SetInteger("Movement", 3);
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
        else
        {
            animator.SetInteger("Movement", 1);
        }
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
            speed = sprint;
        }
        else
        {
            isSprint = false;
            speed = initialSpeed;
        }
    }

    void OnAttack()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            isAttack = true;
            speed = 0f;
        }
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            isAttack = false;
            speed = initialSpeed;
        }
        if(direction.x < 0f) {
            animator.SetFloat("Flip", 1f);
        }else if (direction.x > 0f)
        {
            animator.SetFloat("Flip", -1f);
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        ctUI.UpdateLifePlayer(life);
        StartCoroutine(OnDamage());
    }

    void Die()
    {
        if(life <= 0)
        {
            speed = 0f;
            rbRenderer.color = new Color(1f, 1f, 1f, 1f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetInteger("Movement", 4);
            Destroy(this.gameObject, 1f);
        }
    }

    IEnumerator OnDamage()
    {
        if(life > 0)
        {
            yield return new WaitForSeconds(0.2f);
            rbRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.2f);
            rbRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
            rbRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.2f);
            rbRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Slime>().TakeDamage(1);
        }
    }
}
