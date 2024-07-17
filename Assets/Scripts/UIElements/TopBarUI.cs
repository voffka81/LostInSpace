using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeText;

    [SerializeField]
    private TextMeshProUGUI _moneyText;
    
    [SerializeField]
    private TextMeshProUGUI _locationText;

    [SerializeField]
    public Slider _energy;

    [SerializeField]
    public Slider _food;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.Instance.Time.OnMinuteChanged -= UpdateTime;
        GameManager.Instance.Time.OnMinuteChanged += UpdateTime;
    }

    private void OnDisable()
    {
        GameManager.Instance.Time.OnMinuteChanged -= UpdateTime;
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = $"${(Player.Instance.Stats[StatsId.Money] as INumericStat).Value}";
        _locationText.text= $"{(Player.Instance.Stats[StatsId.LocationName] as IStringStat).Value}";
    }

    private void UpdateTime()
    {
        if (_timeText != null)
        {
            _timeText.text = $"{GameManager.Instance.Time.CurrentTime.GetDayName()} {GameManager.Instance.Time.CurrentTime.ToString(@"hh\:mm")} day ({GameManager.Instance.Time.CurrentTime.Days})";
        }

        _energy.value = (Player.Instance.Stats[StatsId.Energy] as INumericStat).Value;
        _food.value = (Player.Instance.Stats[StatsId.Food] as INumericStat).Value;
    }
}
