using Assets.Scripts.Interfaces;
using UnityEngine;

public class OfficeTable : BaseInteractableObject
{
    [SerializeField]
    private DialogSO _dialogSO;
    protected override void PrepareMenuActions()
    {
        _menuActions[RadialMenuActions.Talk].IsEnabled = true;
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        GameManager.Instance.UI.ShowTabOptionsDialog(_dialogSO,  null, OnConfirm);
    }

    private void OnConfirm(IDialogOption selectedJob)
    {
        _player.JobPosition = (selectedJob as JobInfoSO).JobPosition;
        print($"player selected position is {_player.JobPosition}");
    }
}
