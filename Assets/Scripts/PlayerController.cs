using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    private int score;
    public Text scoreText;
    public Text timeText;
    public Button buttonPlay;
    public Button buttonQuit;
    private GameObject[] pickups;
    private float usedTime;
    private bool isFinish;

    void Start()
    {
        speed = 3F;
        score = 0;
        rb = GetComponent<Rigidbody>();
        buttonQuit.onClick.AddListener(Quit);
        buttonPlay.onClick.AddListener(PlayAgain);
        pickups = GameObject.FindGameObjectsWithTag("Pickup");
        usedTime = 0;
        isFinish = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void Update(){
        if (!isFinish)
        {
            usedTime += Time.deltaTime;
        }
        timeText.text = "Time: " + usedTime.ToString("F2") + "s";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            scoreText.text = "Score: " + score.ToString();
            if (score >= 8)
            {
                buttonPlay.gameObject.SetActive(true);
                buttonQuit.gameObject.SetActive(true);
                isFinish = true;
            }
        }
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit ();
#endif
    }

    void PlayAgain()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Transform>().position = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(Vector3.zero);
        buttonPlay.gameObject.SetActive(false);
        buttonQuit.gameObject.SetActive(false);
        foreach (GameObject pickup in pickups)
        {
            pickup.gameObject.SetActive(true);
        }
        usedTime = 0;
        isFinish = false;
        score = 0;
    }
}