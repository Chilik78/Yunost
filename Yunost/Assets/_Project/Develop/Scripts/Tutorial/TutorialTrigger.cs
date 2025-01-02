using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;
    public string tutorialText;
    private bool hasTriggered = false;

    private void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            //Debug.LogError("Туториал начался");
            hasTriggered = true;
            tutorialManager.StartTutorial(tutorialText);
        }
    }
}
