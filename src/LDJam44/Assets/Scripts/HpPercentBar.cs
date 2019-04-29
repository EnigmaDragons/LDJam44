using UnityEngine;

class HpPercentBar : VerboseMonoBehaviour
{
    public Health Health;

    void Update()
    {
        transform.localScale = new Vector3(Mathf.Clamp(Health.HpPercent, 0, 1.0f), 1, 1);
    }
}
