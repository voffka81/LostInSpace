using System.Runtime.Serialization;

public enum CharacterSex {Female, Male }
public enum JobPositions { Unemployed, Cashier, Clerk, ManagerAssistaint, Manager };
public enum EducationSkill { NotEducated, School, HightSchool, University };
public enum PlayerStates { Awake, Sleeping, Eating, Working }
public enum StatsId { Money, RentAccount, Food, Energy, BankAccount, Job, LocationName }
public enum Tasks { Move, Interact, Rotate };
public enum TaskStatus { None,Waiting, InProgress, Complete };
public enum InteractionStatus { None, Complete, WaitForChoose, InProgress, FarFromPlayer };
public enum AnimationStates
{
    [EnumMember(Value = "{0}_Idle")]
    Idle,
    [EnumMember(Value = "{0}_Move")]
    Walking,
    [EnumMember(Value = "Sleeping")]
    Sleeping,
    [EnumMember(Value = "StandToSit")]
    [BlockingAnimation]
    Sitting,
    [EnumMember(Value = "SitToStand")]
    [BlockingAnimation]
    Standing
};

public enum RadialMenuActions
{
    Cancel,
    Sleep,
    Eat,
    Put,
    Take,
    Work,
    Talk,
    Buy,
    Open,
    Enter,
    Learn,
}