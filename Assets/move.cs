using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float mainFly = 100f;
    [SerializeField] float mainMove = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
  

    Rigidbody rb;
    AudioSource audioSource;


    bool isAlive;
    
    
    void Start()
    {
    rb = GetComponent<Rigidbody>();
    audioSource =GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessFly();
        ProcessMove();
    }
    void ProcessFly()
    {
      if(Input.GetKey(KeyCode.Space))
        {
            startFly();
        }
        else
        {
            stopFly();
        }
    }

    void ProcessMove()
    {
    if(Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moveRight();
        }
        else
        {
            moveStop();
        }

    }

    private void stopFly()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void startFly()
    {
        rb.AddRelativeForce(Vector3.up * mainFly * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void moveStop()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void moveRight()
    {
        ApplyRotation(-mainMove);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void moveLeft()
    {
        ApplyRotation(mainMove);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
