
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : BaseInteractableObject
{
    [SerializeField]
    private ContainerSO _containerSO;

    private List<FoodItemSO> _foodObjects = new List<FoodItemSO>();

    private bool CheckIfPlayerHaveItems()
    {
        if (_player.IsHoldContainerItem())
        {
            var playerContainer = _player.GetContainerItem();
            if (!playerContainer.IsSalebleItems())
            {
                return true;
            }
        }
        return false;
    }

    protected override void PrepareMenuActions()
    {
        _menuActions[RadialMenuActions.Put].IsEnabled = CheckIfPlayerHaveItems();
        _menuActions[RadialMenuActions.Eat].IsEnabled = _foodObjects.Count > 0;
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        switch (interactAction)
        {
            case RadialMenuActions.Put:
                var playerContainer = _player.GetContainerItem();
                if (_foodObjects.Count + playerContainer.GetItems().Count <= _containerSO.MaxCapacity)
                {
                    foreach (FoodItemSO item in playerContainer.GetItems())
                    {
                        _foodObjects.Add(item);
                    }

                    _player.ClearContainerItem();
                    Debug.Log($"Fridge have {_foodObjects.Count} pices of food");
                }
                else
                    Debug.Log($"Fridge is full");
                break;
            case RadialMenuActions.Eat:
                var hunger = (_player.Stats[StatsId.Food] as INumericStat).MaxValue - (_player.Stats[StatsId.Food] as INumericStat).Value;
                var eatingItems = _foodObjects.Count < (hunger / 10) ? _foodObjects.Count : (hunger / 10);
                StartCoroutine(EatRoutine(eatingItems));
                _foodObjects.RemoveRange(0, (int)eatingItems);
                break;
            default:
                print("unknown action");
                break;
        }
    }

    private IEnumerator EatRoutine(float timeToEat)
    {
        _player.SetPlayerActing(PlayerStates.Eating);
        yield return new WaitForSeconds(timeToEat);
        _player.SetPlayerActing(PlayerStates.Awake);
        yield break;
    }

}
