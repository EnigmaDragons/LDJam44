using UnityEngine;

class HpPercentBar : VerboseMonoBehaviour
{
    public Health Health;

    void Update()
    {
        transform.localScale = new Vector3(Health.HpPercent, 1, 1);
    }
}
