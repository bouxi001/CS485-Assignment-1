using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private int score;
    public Text scoreText;

    public Button buttonPlay;
    public Button buttonQuit;

    void Start ()
    {
        speed = 3F;
        score = 0;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Pickup"))
        {
            other.gameObject.SetActive (false);
            score += 1;
            scoreText.text = "Score: " + score.ToString();
            if (score >= 1){
                buttonPlay.gameObject.SetActive(true);
                buttonQuit.gameObject.SetActive(true);
            }
        }
    }
}