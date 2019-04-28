public static class IEnumerableExtensions
{
    public static T Random<T>(this T[] list)
    {
        return list[UnityEngine.Random.Range(0, list.Length - 1)];
    }
}
