using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SoundMachineNewVersion : MonoBehaviour
{
    [SerializeField] private Gradient backgroundGradient;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private TMP_InputField hourTxt, minuteTxt;
    [Space] 
    [SerializeField] private float hourToSecondRatio = 60;
    [SerializeField] private PlayableDirector mainPlayableDirector;
    [SerializeField] private TextMeshProUGUI messagingText;

    [Tooltip("in seconds")] [SerializeField]
    private float playbackDuration = 60;

    [Header("play/pause")] [SerializeField]
    private Image playPauseBtnImage;

    [SerializeField] private Sprite playIcon, pauseIcon;

    private bool reachedPlaybackEnd;
    public float currentSelectedTime;
    public float passedTime;
    private bool isPaused;



    public void OnTimeChanged()
    {
        if (string.IsNullOrEmpty(hourTxt.text))
            hourTxt.text = "00";
        if (string.IsNullOrEmpty(minuteTxt.text))
            minuteTxt.text = "00";
            

        int selectedHour = int.Parse(hourTxt.text);
        int selectedMinute = int.Parse(minuteTxt.text);

        if (!CheckTimeInputValidation(selectedHour, selectedMinute))
            return;

        reachedPlaybackEnd = false;
        currentSelectedTime = (selectedHour * 60) + selectedMinute;
        backgroundColor.color = backgroundGradient.Evaluate(currentSelectedTime / (hourToSecondRatio * 24));
        mainPlayableDirector.time = currentSelectedTime;

        passedTime = 0;
        PlayAudio();
    }

    private bool CheckTimeInputValidation(int hour, int minute)
    {
        if (hour > 23 || hour < 0)
        {
            ShowMessage("the entered HOUR is not valid! try again!");
            return false;
        }

        if (minute > 59 || minute < 0)
        {
            ShowMessage("the entered MINUTE is not valid! try again!");
            return false;
        }

        ShowMessage("");
        return true;
    }

    private void ShowMessage(string message)
    {
        messagingText.text = message;
    }

    #region handling Timeline End Reach  
    private void OnEnable()
    {
        mainPlayableDirector.stopped += OnReachedPlaybackEnd;
    }

    private void OnDisable()
    {
        mainPlayableDirector.stopped -= OnReachedPlaybackEnd;
    }

    private void OnReachedPlaybackEnd(PlayableDirector obj)
    {
        OnReachedPlaybackEnd();
    }
    #endregion
    
    private void OnReachedPlaybackEnd()
    {
        reachedPlaybackEnd = true;
        PauseAudio();
    }

    public void TogglePlayPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseAudio();
        }
        else
        {
            PlayAudio();
        }
    }

    private void PlayAudio()
    {
        if (reachedPlaybackEnd)
        {
            OnTimeChanged();
            return;
        }

        isPaused = false;

        mainPlayableDirector.Play();
        playPauseBtnImage.sprite = pauseIcon;

        float timeToEnd = playbackDuration - passedTime;
        Invoke(nameof(OnReachedPlaybackEnd), timeToEnd);
    }

    private void PauseAudio()
    {
        isPaused = true;

        mainPlayableDirector.Pause();
        playPauseBtnImage.sprite = playIcon;
        CancelInvoke(nameof(OnReachedPlaybackEnd));
        passedTime = (float) (mainPlayableDirector.time - currentSelectedTime);
    }
}