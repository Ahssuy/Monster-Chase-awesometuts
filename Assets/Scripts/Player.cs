using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;

    private float MovementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string Walk_Animation = "Walk";

    private bool isGrounded = true;
    private string Ground_Tag = "Ground";

    private string Enemy_Tag = "Enemy";

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();   
    }
    private void FixedUpdate()
    {
        //PlayerJump();
    }
    void PlayerMoveKeyboard()
    {
        MovementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(MovementX, 0f, 0f) * Time.deltaTime * moveForce;
    }
    void AnimatePlayer()
    {
        // we are going to the right side
        if (MovementX > 0)
        {
            anim.SetBool(Walk_Animation, true);
            sr.flipX = false;
        }
        // we are going to the left side
        else if(MovementX < 0)
        {
           anim.SetBool(Walk_Animation, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(Walk_Animation, false);
        }
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector3(0f,jumpForce,0f),ForceMode2D.Impulse);
        }
    }
    // Allows to jump only once
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Ground_Tag))
            isGrounded = true;

        if(collision.gameObject.CompareTag(Enemy_Tag))
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag(Enemy_Tag))
            Destroy(gameObject);
    }
}
