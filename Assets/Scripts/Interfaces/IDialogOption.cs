using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IDialogItemUI
    {
        IDialogOption Item { get; }
        void SetItem(DialogOptionsUI parent, IDialogOption item);
    }
    public class IDialogOption: ScriptableObject
    {
        public Sprite Icon;
    }
}
