using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rocketRb;
    private AudioSource audioSource;
    [SerializeField] private float thrust = 1000f;
    [SerializeField] private float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    // applies thrust when player presses A or D
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    // applies rotation when player presses A or D
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRb.freezeRotation = false;
    }
}