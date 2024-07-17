using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private Button _btnCancel;
    [SerializeField]
    private Button _btnOk;
    [SerializeField]
    private Slider _slider;

    private TimeSpan _time;

    public void ShowTimeSliderDialog(string title, string description, Action onCancel, Action<TimeSpan> onConfirm)
    {
        GameManager.Instance.Time.OnFastForwardEnd += CloseDialog;
        GameManager.Instance.Pause();
        GameManager.Instance.UI.Freeze();

        gameObject.SetActive(true);
        _title.text = title;
        _description.text = description;
        _btnCancel.onClick.AddListener(() =>
        {
            onCancel?.Invoke();
            Hide();
            CloseDialog();
        });
        _btnOk.onClick.AddListener(() =>
        {
            onConfirm?.Invoke(_time);
            Hide();
        });

        SliderValueChanger(_slider.value);
        _slider.onValueChanged.AddListener(x=>SliderValueChanger(x));
    }


    private void CloseDialog()
    {
        GameManager.Instance.UI.Unfreeze();
        Destroy(this);
    }

    private void SliderValueChanger(float time)
    {
        if (gameObject.activeSelf)
        {
            _time = TimeSpan.FromHours(time);
            _timeText.text = $"{_time.Hours} hours";
        }
    }

    private void Hide()
    {
        _slider.onValueChanged.RemoveListener(x=>SliderValueChanger(x));
        gameObject.SetActive(false);
        GameManager.Instance.Resume();
    }

}
