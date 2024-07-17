using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LifeJourney/Dialog")]
public class DialogSO : ScriptableObject
{
    public string Title;
    public MonoBehaviour UITemplate;
    public List<DialogCategorySO> CategoriesSO;
}
