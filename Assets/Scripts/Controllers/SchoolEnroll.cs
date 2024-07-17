using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class SecretaryDesk : BaseInteractableObject
{
    [SerializeField]
    private DialogSO _dialogSO;

    protected override void PrepareMenuActions()
    {
        _menuActions[RadialMenuActions.Talk].IsEnabled = true;
        _menuActions[RadialMenuActions.Learn].IsEnabled = Player.Instance.ActiveCourse != null;
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        switch (interactAction)
        {

            case RadialMenuActions.Talk:
                GameManager.Instance.UI.ShowTabOptionsDialog(_dialogSO, null, OnConfirm);
                break;
            case RadialMenuActions.Learn:
                GameManager.Instance.UI.ShowTimeSliderDialog("Learn", $"learn {_player.ActiveCourse.name} in next", OnCancel, OnLearnConfirm);
                break;
        }
    }

    private void OnLearnConfirm(TimeSpan time)
    {       
        GameManager.Instance.Time.FastForward(time);
        GameManager.Instance.Time.OnFastForwardEnd += OnFastForwardEnd;
        _player.Learn(time);
    }

    private void OnCancel()
    {
        OnFastForwardEnd();
    }

    private void OnFastForwardEnd()
    {
        _player.SetPlayerActing(PlayerStates.Awake);
        _player.SetPlayerAnimation(AnimationStates.Standing);
        GameManager.Instance.Time.OnFastForwardEnd -= OnFastForwardEnd;
    }
    private void OnConfirm(IDialogOption selectedOption)
    {
        _player.ActiveCourse = (selectedOption as EducationInfoSO);
        print($"player selected course is {(selectedOption as EducationInfoSO).Description}");
    }
}
