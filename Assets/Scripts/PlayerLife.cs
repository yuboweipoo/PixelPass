using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// ���س���

public class PlayerLife : MonoBehaviour
{
    public GameObject playerPS;

    private Rigidbody2D rb;
    private Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip deathClip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "dieLine")
        {
            
            Invoke("Restart", 1f);
            audioSource.PlayOneShot(deathClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            Death();
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Death();
        }
    }

    private void Death()
    {
        audioSource.PlayOneShot(deathClip);
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(playerPS,1f);
        animator.SetTrigger("dies") ;
    }

    // ���¼��س���
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
