using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrust = 1000f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem centreThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    private Rigidbody rocketRb;
    private AudioSource audioSource;

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
    // applies thrust when player presses space
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    // applies rotation when player presses A or D
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }
    }
    private void StartThrusting()
    {
        rocketRb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }

    private void StartRotatingRight()
    {
        ApplyRotation(-rotationSpeed);

        if (!centreThrustParticle.isPlaying)
        {
            leftThrustParticle.Stop();
            rightThrustParticle.Stop();
            centreThrustParticle.Play();
        }
    }

    private void StartRotatingLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!leftThrustParticle.isPlaying || !rightThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
            rightThrustParticle.Play();
            centreThrustParticle.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRb.freezeRotation = false;
    }
}