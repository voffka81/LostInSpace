using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CashierDesk : BaseInteractableObject
{
    [SerializeField]
    private ContainerSO _containerSO;
    [SerializeField]
    private List<JobInfoSO> _jobsInfoList;
    private JobInfoSO _playerJob;

    private bool CheckIfPlayerHaveItems()
    {
        if (_player.IsHoldContainerItem())
        {
            var playerContainer = _player.GetContainerItem();
            if (playerContainer.IsSalebleItems())
            {
                return true;
            }
        }
        return false;
    }

    protected override void PrepareMenuActions()
    {
        _playerJob = _jobsInfoList.FirstOrDefault(x => x.JobPosition == _player.JobPosition);
        if (_playerJob != null)
            _menuActions[RadialMenuActions.Work].IsEnabled = true;

        _menuActions[RadialMenuActions.Buy].IsEnabled = CheckIfPlayerHaveItems();

    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        switch (interactAction)
        {
            case RadialMenuActions.Buy:
                BuyItems();
                break;
            case RadialMenuActions.Work:
                GameManager.Instance.UI.ShowTimeSliderDialog($"Work", $"Work as {_playerJob.Description}", OnCancel, OnConfirm);
                break;
            default:
                print("unknown action");
                break;
        }
    }

    private void BuyItems()
    {
        var playerContainer = _player.GetContainerItem();
        if (playerContainer.IsSalebleItems())
        {
            var playerItemsList = playerContainer.GetItems();
            _player.ClearContainerItem();
            float finalPrice = 0;
            foreach (SellableItemSO item in playerItemsList)
            {
                finalPrice += item.Price;
            }

            var transform = Instantiate(_containerSO.prefab, _interactionPoint);
            var containerItem = transform.GetComponent<ContainerItem>();
            if (containerItem == null)
            {
                Debug.LogError("Container Item is null");
                return;
            }
            _player.Pay(finalPrice);
            foreach (var item in playerContainer.GetItems())
            {

                var foodItemSO = ScriptableObject.CreateInstance<FoodItemSO>();
                foodItemSO.ItemName = item.ItemName;
                foodItemSO.Energy = 0;
                containerItem.AddItem(foodItemSO);
            }
            _player.SetContainerItem(containerItem);
        }
    }

    private void OnCancel()
    {
        OnFastForwardEnd();
    }

    float _totalSalary;
    private void OnConfirm(TimeSpan time)
    {
        _totalSalary = (float)(time.TotalHours * _playerJob.Salary);
        _player.SetPlayerActing(PlayerStates.Working);
        GameManager.Instance.Time.FastForward(time);
        GameManager.Instance.Time.OnFastForwardEnd += OnFastForwardEnd;
    }

    private void OnFastForwardEnd()
    {
        _player.AddMoney(_totalSalary);
        _player.SetPlayerActing(PlayerStates.Awake);
        GameManager.Instance.Time.OnFastForwardEnd -= OnFastForwardEnd;
    }

}
