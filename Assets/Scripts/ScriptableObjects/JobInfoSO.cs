using UnityEngine;
using Assets.Scripts.Interfaces;

[CreateAssetMenu(menuName = "LifeJourney/Job info")]
public class JobInfoSO : IDialogOption
{
    public string Description;
    public float Salary;
    public JobPositions JobPosition;
    public EducationSkill MinimumEducationSkill;
}
