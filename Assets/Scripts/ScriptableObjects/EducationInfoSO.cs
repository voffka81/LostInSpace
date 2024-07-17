using Assets.Scripts.Interfaces;
using UnityEngine;

[CreateAssetMenu(menuName = "LifeJourney/Education Info")]
public class EducationInfoSO : IDialogOption
{
    public string Description;
    public int Duration;
    public float EnrollPrice;
    public EducationSkill Skill;
    public int PlayerProgress;
}
