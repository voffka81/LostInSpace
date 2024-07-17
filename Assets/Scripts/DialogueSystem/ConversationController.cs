//using Assets.Scripts.Actions.Interfaces;
//using System.Collections.Generic;
//using System.Linq;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ConversationController : MonoBehaviour
//{
//    [SerializeField] public TextMeshProUGUI _title;
//    [SerializeField] public Button _choiceButton;
//    [SerializeField] private Button _btnApply;
//    [SerializeField] private Button _btnClose;

//    public ConversationChangeEvent conversationChangeEvent;
//    private List<ChoiceController> choiceControllers = new();

//    private IPlayerAction _selectedAction = null;

//    private void Awake()
//    {
//        Hide();
//    }

//    public void Change(string title, List<IPlayerAction> options)
//    {
//        _btnApply.interactable = false;

//        _btnClose.onClick.AddListener(() =>
//         {
//             Hide();
//         });

//        _btnApply.onClick.AddListener(() =>
//        {
//            ApplyActionOnPlayer(_selectedAction);
//        });

//        //Player.Instance.allowMovement = false;
//        RemoveChoices();
//        _title.text = title;
//        gameObject.SetActive(true);

//        for (var count = 0; count < options.Count; count++)
//        {
//            ChoiceController c = ChoiceController.AddChoiceButton(_choiceButton, options.ElementAt(count), count);
//            c.conversationChangeEvent.AddListener(SelectAction);
//            choiceControllers.Add(c);
//        }

//        StopAllCoroutines();

//        Time.timeScale = 0;
//        _choiceButton.gameObject.SetActive(false);
//    }

//    private void SelectAction(IPlayerAction action)
//    {
//        _selectedAction = action;
//        if (action is ISellable)
//        {
//           // _btnApply.interactable = Player.Instance.Stats[StatsId.Money].Value >= (action as ISellable).Price;
//        }
//    }

//    private void ApplyActionOnPlayer(IPlayerAction action)
//    {
//        //if (action is ISellable)
//        //    Player.Instance.BuyAction(action);
//    }
//    private void RemoveChoices()
//    {
//        foreach (ChoiceController c in choiceControllers)
//        {
//            c.conversationChangeEvent.RemoveAllListeners();
//            Destroy(c.gameObject);
//        }
//        choiceControllers.Clear();
//    }

//    public void Hide()
//    {
//        RemoveChoices();
//        gameObject.SetActive(false);
//        Time.timeScale = 1;
//        //Player.Instance.allowMovement = true;
//    }
//}
