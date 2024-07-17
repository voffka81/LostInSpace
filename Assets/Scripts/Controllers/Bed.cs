using System;

public class Bed : BaseInteractableObject
{

    protected override void PrepareMenuActions()
    {
        _menuActions[RadialMenuActions.Sleep].IsEnabled = true;
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        _player.SetPlayerAnimation(AnimationStates.Sitting, OnAnimationFinished);
    }

    private void OnAnimationFinished()
    {
        GameManager.Instance.UI.ShowTimeSliderDialog("Sleep", "Wake up in", OnCancel, OnConfirm);
    }
    private void OnCancel()
    {
        OnFastForwardEnd();
    }

    private void OnConfirm(TimeSpan time)
    {
        _player.SetPlayerActing(PlayerStates.Sleeping);
        GameManager.Instance.Time.FastForward(time);
        GameManager.Instance.Time.OnFastForwardEnd += OnFastForwardEnd;
    }

    private void OnFastForwardEnd()
    {
        _player.SetPlayerActing(PlayerStates.Awake);
        _player.SetPlayerAnimation(AnimationStates.Standing);
        GameManager.Instance.Time.OnFastForwardEnd -= OnFastForwardEnd;
    }
}
