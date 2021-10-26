using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    delegate void TurnDelegate();
    TurnDelegate turnDelegate;

    public float moveSpeed = 2;
    bool lookingRight = true;
    GameManager gameManager;
    Animator anim;
    public Transform rayOrigin;
    public ParticleSystem effect;

    public Text scoreTxt, hScoreTxt;
    public int Score { get; private set; }
    public int Hscore { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        #region PLATFORM FOR TURNING
            #if UNITY_EDITOR
                    turnDelegate = TurnPlayerUsingKeyboard;
            #endif
            #if UNITY_ANDROID
                    turnDelegate = TurnPlayerUsingTouch;
            #endif
        #endregion

        gameManager = GameObject.FindObjectOfType<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        Hscore = PlayerPrefs.GetInt("highscore");
        hScoreTxt.text = Hscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameStarted) return;

        anim.SetTrigger("gameStarted");

        moveSpeed *= 1.0001f;
        //transform.position += transform.forward * Time.deltaTime * moveSpeed;       
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed);

        turnDelegate();

        CheckFalling();
    }

    private void TurnPlayerUsingKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Turn();
    }

    private void TurnPlayerUsingTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            Turn();
    }

    float elapsedTime = 0;
    float freq = 1f / 5f;
    private void CheckFalling()
    {
        if((elapsedTime += Time.deltaTime) > freq)
        {
            if (!Physics.Raycast(rayOrigin.transform.position, Vector3.down))
            {
                anim.SetTrigger("falling");
                gameManager.RestartGame();
                elapsedTime = 0;
            }
        }
        
    }

    private void Turn()
    {
        if (lookingRight)
        {
            transform.Rotate(new Vector3(0, 1, 0), -90);
        }
        else
        {
            transform.Rotate(new Vector3(0, 1, 0), 90);
        }
        lookingRight = !lookingRight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("crystal"))
        {
            MakeScore();
            CreateEffect();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(collision.gameObject, 2f);
    }

    private void CreateEffect()
    {
        var vfx = Instantiate(effect, transform);
        Destroy(vfx, 1f);
    }

    private void MakeScore()
    {
        Score++;
        scoreTxt.text = Score.ToString();
        if (Score > Hscore)
        {
            Hscore = Score;
            hScoreTxt.text = Hscore.ToString();
            PlayerPrefs.SetInt("highscore", Hscore);
        }
    }
}
