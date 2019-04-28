
class MaxLifetime : VerboseMonoBehaviour
{
    public float Seconds = 10f;

    private void Start()
    {
        Destroy(gameObject, Seconds);
    }
}
