using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float turningSpeed = 90f;
    [SerializeField] float torqueAmount = 1;
    private ParticleSystem dustParticles;
    private Rigidbody2D rb;
    private GameObject scoreTextGO;
    public float torque = 4;
    public float jumpForce = 10f;
    public float fastSpeed = 1.5f; 
    public float slowSpeed = 0.75f; 
    private bool isGrounded = false;
    public GameObject GameManagerGO;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dustParticles = transform.Find("Dust Particles").GetComponent<ParticleSystem>();
        scoreTextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
       
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddTorque(torqueAmount);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddTorque(-torqueAmount);
            }
        

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            dustParticles.Play();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            dustParticles.Stop();
        }

        if (collision.collider.CompareTag("Rock"))
        {
            Die();
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            Die();
        }
        if (collision.CompareTag("BonusItem"))
        {
            scoreTextGO.GetComponent<GameScore>().Score += 20;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Finish"))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Victory);
            gameObject.SetActive(false);
        }
    }
    private void Die()
    {
        
        gameObject.SetActive(false);

       
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
    }
}
