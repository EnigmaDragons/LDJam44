using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Role role;
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;

    public void ApplyDamage(int amount)
    {
        currentHp -= amount;
    }
}
