using UnityEngine;

static class SpawnBoundaries
{
    public const int minDecorX = -200;
    public const int maxDecorX = 200;
    public const int minDecorY = -120;
    public const int maxDecorY = 120;
    public const int minX = -80;
    public const int minY = -34;
    public const int maxX = 80;
    public const int maxY = 34;
    public const int minScreenX = -10;
    public const int minScreenY = -6;
    public const int maxScreenX = 10;
    public const int maxScreenY = 6;
    public const int yOffset = 2;
    public const float startClearPlayAreaDistance = 38f;
    public const float endClearPlayAreaDistance = 30f;
    public const float playZoneFactor = 1.5f;

    public static Vector3 RandomDecorZone(float z, float zVariance = 0)
    {
        var x = Random.Range(minDecorX, maxDecorX);
        while (x > minScreenX && x < maxScreenX)
            x = Random.Range(minDecorX, maxDecorX);
        var y = Random.Range(minDecorY, maxDecorY);
        while (y > minScreenY && y < maxScreenY)
            y = Random.Range(minDecorY, maxDecorY);

        return new Vector3(x, y, VariedZ(z, zVariance));
    }

    public static Vector3 RandomOffPlayZone(float z, float zVariance = 0)
    {
        var x = Random.Range(minX, maxX);
        while (x > minScreenX && x < maxScreenX)
            x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);
        while (y > minScreenY && y < maxScreenY)
            y = Random.Range(minY, maxY);

        return new Vector3(x, y, VariedZ(z, zVariance));
    }

    public static Vector3 RandomInPlayZone(float z, float zVariance = 0)
    {
        return new Vector3(
            Random.Range(minScreenX * playZoneFactor, maxScreenX * playZoneFactor), 
            Random.Range(minScreenY * playZoneFactor * playZoneFactor * playZoneFactor, maxScreenY * playZoneFactor * playZoneFactor * playZoneFactor) + yOffset, 
            VariedZ(z, zVariance));
    }

    public static Vector3 RandomInLevel(float z)
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), z);
    }

    public static Vector3 RandomBetween(Vector3 first, Vector3 second)
    {
        return new Vector3(Random.Range(first.x, second.x), Random.Range(first.y, second.y), Random.Range(first.z, second.z));
    }

    private static float VariedZ(float z, float zVariance) => z + Random.Range(-zVariance, zVariance);

    public static bool IsInPlayArea(Vector3 position)
    {
        return (position.x >= minScreenX * playZoneFactor && position.x <= maxScreenX * playZoneFactor)
            && (position.y >= minScreenY * playZoneFactor * playZoneFactor && position.y <= maxScreenY * playZoneFactor * playZoneFactor);
    }

    public static bool IsInEnemyPlayArea(Vector3 position, Vector3 playerPosition)
    {
        return IsInPlayArea(position) && position.z > playerPosition.z + 8f;
    }
}
