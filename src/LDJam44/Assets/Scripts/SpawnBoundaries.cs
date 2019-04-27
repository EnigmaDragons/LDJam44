using UnityEngine;

static class SpawnBoundaries
{
    public const int minX = -30;
    public const int minY = -22;
    public const int maxX = 30;
    public const int maxY = 22;
    public const int minScreenX = -10;
    public const int minScreenY = -6;
    public const int maxScreenX = 10;
    public const int maxScreenY = 6;

    public static Vector3 RandomOffScreen(float z)
    {
        var x = Random.Range(minX, maxX);
        while (x > minScreenX && x < maxScreenX)
            x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);
        while (y > minScreenY && y < maxScreenY)
            y = Random.Range(minY, maxY);

        return new Vector3(x, y, z);
    }

    public static Vector3 RandomInLevel(float z)
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), z);
    }

    public static Vector3 RandomBetween(Vector3 first, Vector3 second)
    {
        return new Vector3(Random.Range(first.x, second.x), Random.Range(first.y, second.y), Random.Range(first.z, second.z));
    }
}
