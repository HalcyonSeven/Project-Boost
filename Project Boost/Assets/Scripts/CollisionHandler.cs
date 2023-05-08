using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1.5f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    private AudioSource audioSource;

    bool isGameTransitioning;

    private void Start()
    {
        isGameTransitioning = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isGameTransitioning) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit Friendly");
                break;
            case "Finish":
                TransitionToNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        isGameTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<ParticleSystem>().Play(crashParticle);
        GetComponent<Movement>().enabled = false;
        StartCoroutine(ReloadScene());
    }
    void TransitionToNextLevel()
    {
        isGameTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<ParticleSystem>().Play(successParticle);
        GetComponent<Movement>().enabled = false;
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(currentSceneIndex);
        GetComponent<Movement>().enabled = true;
    }
    IEnumerator LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        yield return new WaitForSeconds(loadDelay);
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        GetComponent<Movement>().enabled = true;
    }
}
