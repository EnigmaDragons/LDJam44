
using TMPro;

class HpText : VerboseMonoBehaviour
{
    public Health Health;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = VerboseGetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = $"{Health.currentHp} / {Health.maxHp}";
    }
}
