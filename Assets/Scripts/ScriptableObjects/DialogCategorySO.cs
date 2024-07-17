using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LifeJourney/Dialog Category")]
public class DialogCategorySO : ScriptableObject
{
    public string Title;
    public Sprite Icon;
    public List<IDialogOption> OptionsList;
}
