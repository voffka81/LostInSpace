using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuItem : MonoBehaviour
{
    [SerializeField]
    private Button _radialMenuItemPrefab;
    private Dictionary<RadialMenuActions, RadialMenuActionDescription> _actions;
    private UniTaskCompletionSource<RadialMenuActions> tcs = new UniTaskCompletionSource<RadialMenuActions>();
    //private float _buttonWidth;

    private void Start()
    {
     //   _buttonWidth = _radialMenuItemPrefab.GetComponent<RectTransform>().rect.width;
    }

    public UniTask<RadialMenuActions> ShowButtons(Dictionary<RadialMenuActions, RadialMenuActionDescription> actions)
    {
        _actions = actions;
        for (int buttonsCount = 0; buttonsCount < actions.Count; buttonsCount++)
        {
            var button = Instantiate(_radialMenuItemPrefab);
            button.transform.SetParent(transform, false);

            float theta = (2 * Mathf.PI / actions.Count) * buttonsCount;
            float posX = Mathf.Sin(theta);
            float posY = Mathf.Cos(theta);

            button.transform.localPosition = new Vector3(posX, posY, 0) * 150f;

            var textMeshPro = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = actions.ElementAt(buttonsCount).Value.Description;
            }
            AddEvent(button, buttonsCount);

        }
        return tcs.Task;
    }

    void AddEvent(Button b, int buttonNumber)
    {
        b.onClick.AddListener(() =>
        {
            tcs.TrySetResult(_actions.ElementAt(buttonNumber).Key);
            Close();
        });
    }

    public void CancelAndClose()
    {
        tcs.TrySetResult(RadialMenuActions.Cancel);
        Close();
    }

    private void Close()
    {
        GameManager.Instance.UI.Unfreeze();
        Destroy(this);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
