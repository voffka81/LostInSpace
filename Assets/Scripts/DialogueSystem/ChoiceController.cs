//using TMPro;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//[System.Serializable]
//public class ConversationChangeEvent : UnityEvent<IPlayerAction> { }

//public class ChoiceController : MonoBehaviour
//{
//    public IPlayerAction _option;
//    public ConversationChangeEvent conversationChangeEvent;
//    // Update is called once per frame
//    public static ChoiceController AddChoiceButton(Button choiceButtonTemplate, IPlayerAction option, int index)
//    {
//        int buttonSpacing = -50;

//        Button button = Instantiate(choiceButtonTemplate);

//        button.transform.SetParent(choiceButtonTemplate.transform.parent);
//        button.transform.localScale = Vector3.one;
//        button.transform.localPosition = choiceButtonTemplate.transform.localPosition + new Vector3(0, index * buttonSpacing, 0);
//        button.name = option.Description;
//        button.gameObject.SetActive(true);
//        ChoiceController choiceController = button.GetComponent<ChoiceController>();
//        choiceController._option = option;
//        return choiceController;
//    }

//    private void Start()
//    {
//        if (conversationChangeEvent == null)
//            conversationChangeEvent = new ConversationChangeEvent();
//        var btn = GetComponent<Button>();

//        var txt = btn.GetComponentInChildren<TextMeshProUGUI>();
//        txt.text = _option.Description;
//    }

//    public void MakeChoice()
//    {
//        conversationChangeEvent.Invoke(_option);
//    }
//}
