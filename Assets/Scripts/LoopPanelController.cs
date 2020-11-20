using UnityEngine;
using UnityEngine.UI;

public class LoopPanelController : MonoBehaviour
{
    public Button startStopRecordButton;
    public Text startStopRecordButtonText;
    public Button startStopLoopButton;
    public Text startStopLoopButtonText;
    public LoopManager loopManager;

    private void OnEnable()
    {
        loopManager.OnStartedRecording += OnStartedRecording;
        loopManager.OnStoppedRecording += OnStoppedRecording;
        loopManager.OnStartedLooping += OnStartedLooping;
        loopManager.OnStoppedLooping += OnStoppedLooping;
    }

    private void OnDisable()
    {
        loopManager.OnStartedRecording -= OnStartedRecording;
        loopManager.OnStoppedRecording -= OnStoppedRecording;
        loopManager.OnStartedLooping -= OnStartedLooping;
        loopManager.OnStoppedLooping -= OnStoppedLooping;
    }

    void OnStartedRecording()
    {
        startStopRecordButtonText.text = "Stop Recording";
    }

    void OnStoppedRecording()
    {
        startStopRecordButtonText.text = "Start Recording";
        startStopLoopButton.interactable = true;
    }

    void OnStartedLooping()
    {
        startStopLoopButtonText.text = "Stop Loop";
        startStopRecordButton.interactable = false;
    }

    void OnStoppedLooping()
    {
        startStopLoopButtonText.text = "Start Loop";
        startStopRecordButton.interactable = true;
    }
}
