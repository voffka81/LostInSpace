using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlockingAnimation : Attribute { }
public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField]
    protected NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;
    protected Animator _animator;
    private const string WALK_VELOCITY = "WalkVelocity";
    private readonly Queue<PlayerTasks> _tasks = new Queue<PlayerTasks>();
    private PlayerTasks _currentTask;

    private CharacterDescriptor _characterDescriptor;
    private Action _OnAnimationFinish;
    private AnimationStates _currentAnimation;

    private CharacterSex _characterSex;

    protected void IntCharacter()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterDescriptor = GetComponentInChildren<CharacterDescriptor>();
        if (_characterDescriptor == null) throw new Exception("Character descriptor not found");
        _characterSex = _characterDescriptor.Sex;
        SetPlayerAnimation(AnimationStates.Walking);
    }

    private void Update()
    {
        if (PlayerHelper.IsBlockingAnimation(_currentAnimation))
        {
            if (IsAnimationStatePlaying(0))
            {
                return;
            }
            else
            {
                _OnAnimationFinish?.Invoke();
                _OnAnimationFinish = null;
            }
        }

        if (_currentTask == null || _currentTask.Status == TaskStatus.Complete)
        {
            _tasks.TryDequeue(out _currentTask);
        }
        if (_currentTask != null)
        {
            if (_currentTask.Status == TaskStatus.Waiting)
                Debug.Log($"Current task {_currentTask.Task}");
            switch (_currentTask.Task)
            {
                case Tasks.Rotate:
                    _currentTask.UpdateStatus(Rotate(_currentTask.TagretObject._interactionPoint.forward));
                    break;
                case Tasks.Move:
                    if (_currentAnimation == AnimationStates.Sitting)
                    {
                        SetPlayerAnimation(AnimationStates.Standing);
                        return;
                    }
                    _navAgent.SetDestination(_currentTask.TagretObject._interactionPoint.position);
                    _currentTask.UpdateStatus(MoveToPoint());
                    break;
                case Tasks.Interact:
                    _currentTask.UpdateStatus(TaskStatus.Waiting);

                    _currentTask.UpdateStatus(_currentTask.TagretObject.Interact());
                    break;
            }
        }
    }

    private TaskStatus MoveToPoint()
    {
        _navAgent.isStopped = false;
        SetPlayerAnimation(AnimationStates.Walking);

        _animator.SetFloat(WALK_VELOCITY, _navAgent.velocity.magnitude);
        return IsPathComplete(_navAgent.destination) ? TaskStatus.Complete : TaskStatus.InProgress;
    }

    public bool IsPathComplete(Vector3 destination)
    {
        var dest = new Vector3(destination.x, 0, destination.z);
        var pos = new Vector3(_navAgent.transform.position.x, 0, _navAgent.transform.position.z);

        if (Vector3.Distance(dest, pos) <= _navAgent.radius)
        {
            transform.position = destination;
            if (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude < 0.2f)
            {
                SetPlayerAnimation(AnimationStates.Idle);
                _navAgent.isStopped = true;
                return true;
            }
        }
        return false;
    }

    protected TaskStatus Rotate(Vector3 target)
    {
        var targetRot = Quaternion.LookRotation(target);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target), 10 * Time.deltaTime);
        rotation.x = 0;
        transform.rotation = rotation;
        if (IsApproximate(targetRot, transform.rotation, 0.000004f))
        {
            return TaskStatus.Complete;
        }
        return TaskStatus.InProgress;
    }

    protected void AddTask(PlayerTasks task)
    {
        _tasks.Enqueue(task);
    }

    public bool IsAnimationStatePlaying(int animLayer)
    {
        string stateName = PlayerHelper.GetEnumMemberValue(_currentAnimation);
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(animLayer);
        return stateInfo.IsName(stateName) && stateInfo.normalizedTime < 1.0f;
    }

    public void SetPlayerAnimation(AnimationStates newState, Action onAnimationFinish)
    {
        _OnAnimationFinish = onAnimationFinish;
        SetPlayerAnimation(newState);
    }

    public void SetPlayerAnimation(AnimationStates newState)
    {
        if (newState == _currentAnimation)
        {
            return;
        }
        var stringState=PlayerHelper.GetEnumMemberValue(newState);
        stringState= string.Format(stringState,_characterSex.ToString());
        Debug.Log($"Animation state {stringState}");
        _animator.Play(stringState);
        _currentAnimation = newState;
    }

    private bool IsApproximate(Quaternion q1, Quaternion q2, float precision)
    {
        return Mathf.Abs(Quaternion.Dot(q1, q2)) >= 1 - precision;
    }
}
