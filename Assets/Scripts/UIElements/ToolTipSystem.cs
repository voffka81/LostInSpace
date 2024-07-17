using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    private static ToolTipSystem _current;
    [SerializeField] private ToolTip _toolTip;
    public void Awake()
    {
        _current = this;
        _current._toolTip.gameObject.SetActive(false);
    }

    public static void Show(string content, string header = "")
    {
        _current._toolTip.SetText(content, header);
        _current._toolTip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        _current._toolTip.gameObject.SetActive(false);
    }
}
