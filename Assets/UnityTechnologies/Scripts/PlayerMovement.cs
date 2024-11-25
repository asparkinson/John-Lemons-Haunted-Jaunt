using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;

    public bool playerPassed1;
    public bool playerPassed2;
    public bool playerPassed3;
    public bool playerPassed4;

    public TextMeshProUGUI pointsText;
    
    public float turnSpeed = 20f;
    public int points;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        pointsText.text = "Points: " + points;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == checkpoint1 && playerPassed1 == false)
        {
            playerPassed1 = true;
            points++;
            UpdatePointsText();
        }
        else if(other.gameObject == checkpoint2 && playerPassed2 == false)
        {
            playerPassed2 = true;
            points++;
            UpdatePointsText();
        }
        else if (other.gameObject == checkpoint3 && playerPassed3 == false)
        {
            playerPassed3 = true;
            points++;
            UpdatePointsText();
        }
        else if (other.gameObject == checkpoint4 && playerPassed4 == false)
        {
            playerPassed4 = true;
            points++;
            UpdatePointsText();
        }
    }

    void UpdatePointsText()
    {
        pointsText.text = "Points: " + points;
    }
}
