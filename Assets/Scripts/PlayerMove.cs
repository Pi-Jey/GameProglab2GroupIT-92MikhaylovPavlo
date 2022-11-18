using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float Speed = 10;
    [SerializeField] private float JumpForce = 200;
    
    private bool IsPlayerOnGround = true;
    private Rigidbody rb;
    private const float positionToLose = -5f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Movement();
        Jump();
        CheckLoseCondition();
    }

    private void CheckLoseCondition()
    {
        if (transform.position.y < positionToLose)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0f, vertical) * Speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerOnGround)
        {
            rb.AddForce(Vector3.up * JumpForce);
            IsPlayerOnGround = false;
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Portal"))
        {
            if (ScoreUI.Instance.CanGoToPortal())
            {
                if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
                    SceneManager.LoadScene(0);
                else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                ScoreUI.Instance.ShowNotEnough();
            }
        }
        else if (other.transform.CompareTag("Plate"))
        {
            IsPlayerOnGround = true;
        }
        else if (other.transform.CompareTag("Coin"))
        {
            ScoreUI.Instance.OnCoinPickUp();
            Destroy(other.gameObject);
        }
    }
}
