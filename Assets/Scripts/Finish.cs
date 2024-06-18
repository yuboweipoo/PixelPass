using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    private AudioSource _finishSound;
    private void Start() {
        _finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player"){
            _finishSound.Play();
            Invoke("FinishLevel",0.6f);
        }
    }

    private void FinishLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);//切换场景
    }
}