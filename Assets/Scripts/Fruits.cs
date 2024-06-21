
using UnityEngine;
using UnityEngine.UI;

public class Fruits : MonoBehaviour
{
    private int banans = 0;
    [SerializeField] private Text bananText;
    [SerializeField] AudioClip collect;

    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits")) {

            audio.clip = collect;
            audio.Play();
            Destroy(collision.gameObject);
            ++banans;
            bananText.text = "香蕉:" + banans;
        }
    }
}
