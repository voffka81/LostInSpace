//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ItemActionsUI : MonoBehaviour
//{
//    [SerializeField]
//    private TextMeshProUGUI _title;
//    [SerializeField]
//    private TextMeshProUGUI _description;

//    [SerializeField]
//    private Button _btnCancel;

//    private void Start()
//    {
//        Hide();
//    }

//    public void Show(string title, string description)
//    {
//        gameObject.SetActive(true);
//        _title.text = title;
//        _description.text = description;

//        _btnCancel.onClick.AddListener(() =>
//        {
//            Hide();
//        });
//    }

//    private void Hide()
//    {
//        gameObject.SetActive(false);
//    }
//}
