using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public event EventHandler<bool> OnContainerChanged;
    public static Player Instance { get; private set; }

    private HoldPoint _holdPoint;

    private PlayerStates _currentActing;

    public Dictionary<StatsId, object> Stats;
    public JobPositions JobPosition { get; set; }

    private List<EducationInfoSO> _completedCourses = new();
    public EducationInfoSO ActiveCourse { get; set; }
    public EducationSkill Education { get; set; }
    private int _educationPoints = 0;
    private ContainerItem _containerItem;

    private string _locationName;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("There's more than one player instance");
            return;
        }
        PlayerPrefs.SetString("lastExitName", string.Empty);
        Instance = this;
        Stats = PlayerStats.CreateInitialStats();
        JobPosition = JobPositions.Unemployed;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        base.IntCharacter();
        _holdPoint = GetComponentInChildren<HoldPoint>();
        GameManager.Instance.Time.OnMinuteChanged += UpdateStatsByClock;
        _animator.applyRootMotion = true;
        _navAgent.updatePosition = false;

        _currentActing = PlayerStates.Awake;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Time.OnMinuteChanged -= UpdateStatsByClock;
    }

    public void SetPosition(Transform desiredPosition)
    {
        _navAgent.Warp(desiredPosition.position);
        _navAgent.updatePosition = false;
        Rotate(desiredPosition.forward * -1);
    }

    public void GoToPoint(BaseInteractableObject point)
    {
        AddTask(new PlayerTasks(Tasks.Move, point));
    }

    public async void Interact(BaseInteractableObject interactionItem)
    {
        var result = await interactionItem.ShowPopupMenu(this);
        if (result != InteractionStatus.Complete)
        {
            if (!IsPathComplete(interactionItem._interactionPoint.position))
            {
                AddTask(new PlayerTasks(Tasks.Move, interactionItem));
                AddTask(new PlayerTasks(Tasks.Rotate, interactionItem));
                AddTask(new PlayerTasks(Tasks.Interact, interactionItem));
            }
            else
            {
                AddTask(new PlayerTasks(Tasks.Interact, interactionItem));
            }
        }
    }

    public void SetPlayerActing(PlayerStates state)
    {
        _currentActing = state;
    }

    public void UpdateStatsByClock()
    {
        switch (_currentActing)
        {
            case PlayerStates.Eating:
                (Stats[StatsId.Food] as INumericStat).increase(10f);
                break;
            case PlayerStates.Sleeping:
                (Stats[StatsId.Energy] as INumericStat).increase(0.2f);
                (Stats[StatsId.Food] as INumericStat).deduct(0.03f);
                break;
            default:
                (Stats[StatsId.Food] as INumericStat).deduct(0.05f); // 48 hours it's 100, 100/2880=~0.034 per minute
                (Stats[StatsId.Energy] as INumericStat).deduct(0.1f); // 24 hours it's 100, 100/1440=~0.096 per minute
                break;
        }
    }

    public void Pay(float amount)
    {
        (Stats[StatsId.Money] as INumericStat).deduct(amount);
    }

    public void AddMoney(float amount)
    {
        (Stats[StatsId.Money] as INumericStat).increase(amount);
    }

    public void SetContainerItem(ContainerItem containerItem)
    {
        containerItem.transform.parent = _holdPoint.transform;
        containerItem.transform.localPosition = Vector3.zero;
        _containerItem = containerItem;
        OnContainerChanged.Invoke(this, true);
    }

    public void ClearContainerItem()
    {
        if (_containerItem == null) 
            return;
        Destroy(_containerItem.gameObject);
        OnContainerChanged.Invoke(this, false);
        _containerItem = null;
    }

    public ContainerItem GetContainerItem()
    {
        return _containerItem;
    }

    public bool IsHoldContainerItem()
    {
        return _containerItem != null;
    }

    public void SetLocationName(string locationName)
    {
        _locationName = locationName;
    }
    public string GetLocationName()
    {
        return _locationName;
    }

    internal void Learn(TimeSpan time)
    {
        ActiveCourse.PlayerProgress += time.Hours;
        if (ActiveCourse.PlayerProgress >= ActiveCourse.Duration)
        {
            _educationPoints++;
            if(_educationPoints == 2)
            {
                Education++;
                print($"Congratulation player finish {Education - 1}  and his education now {Education}");
            }
            _completedCourses.Add(ActiveCourse);
            ActiveCourse = null;
        }
    }
}
