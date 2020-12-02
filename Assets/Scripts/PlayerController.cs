using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private const int MAX_OBJECT_POSITION = 18;
    private const int MIN_OBJECT_POSITION = -18;

    private Animator animator;
    private bool isJumping;
    private int score = 0;
    private int bestScore;
    //private bool isBackStep = true;
    //private bool isLose = false;
    public static PlayerController player;
    private int coins;

    [SerializeField] private Generator generator;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject[] canvasButtons;

    private void Start()
    {
        //PlayerPrefs.SetInt("coin", 0);
        player = this;
        coins = PlayerPrefs.GetInt("coin");
        coinText.text = coins.ToString();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                var posChange = touch.deltaPosition;

            }
        }
#endif

        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            score++;
            scoreText.text = score.ToString();
            bestScore = PlayerPrefs.GetInt("score");

            if (score > bestScore)
            {
                PlayerPrefs.SetInt("score", score);
                
            }
            
            float zDifference = 0;

            if(transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }

            MoveCharacter(new Vector3(1, 0, zDifference), 90);
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isJumping)
        {
            MoveCharacter(new Vector3(0, 0, 1), 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isJumping)
        {
            MoveCharacter(new Vector3(0, 0, -1), 180);
        }
        else if(Input.GetKeyDown(KeyCode.S) && !isJumping)
        {
            float zDifference = 0;

            if (transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(-1, 0, zDifference), 90);
        }

        KillPlayer();
    }

    private void FinishJump()
    {
        isJumping = false;
    }

    private void MoveCharacter(Vector3 difference, float rotationY)
    {
        Destroy(canvasButtons[0]);

        GetJumpSound();

        animator.SetTrigger("jump");
        isJumping = true;
        var finalPos = transform.position + difference;
        transform.DOMove(finalPos, 0f);
        transform.DORotate(new Vector3(0, rotationY, 0), 0f, RotateMode.Fast);
        generator.SpawnTerrain(false, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if(collision.collider.GetComponent<MovingObject>().movingObject.tag == "Log")
            {
                transform.parent = collision.collider.transform;
            }
        }
        else if (collision.collider.gameObject.tag == "Tree")
        {
            transform.parent = null;
            Debug.Log("Столкновение");
        }
        else
        {
            transform.parent = null;
        }
    }

    public void Lose()
    {
        bestScoreText.text = $"Рекорд:{PlayerPrefs.GetInt("score")}";
        canvasButtons[1].SetActive(true);
        canvasButtons[2].SetActive(true);
        canvasButtons[3].SetActive(true);
    }

    private void GetJumpSound()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("coin", coins);
            Destroy(collider.gameObject);
            coinText.GetComponent<Text>().text = coins.ToString();
            if (PlayerPrefs.GetString("music") != "No")
            {
                coinText.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void KillPlayer()
    {
        float zPos = this.transform.position.z;

        if (zPos > MAX_OBJECT_POSITION || zPos < MIN_OBJECT_POSITION)
        {
            Destroy(this);
            this.Lose();
        }
    }
}