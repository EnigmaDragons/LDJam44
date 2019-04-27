using UnityEngine;

public abstract class VerboseMonoBehaviour : MonoBehaviour
{
    protected T VerboseGetComponent<T>()
    {
        var c = GetComponent<T>();
        if (c == null)
            Debug.LogError($"Missing required {typeof(T).Name} on GameObject {name}");
        return c;
    }

    protected T VerboseGetComponent<T>(GameObject o)
    {
        var c = o.GetComponent<T>();
        if (c == null)
            Debug.LogError($"Missing required {typeof(T).Name} on GameObject {o.name}");
        return c;
    }

    protected T VerboseFindObjectOfType<T>() where T : UnityEngine.Object
    {
        var o = FindObjectOfType<T>();
        if (o == null)
            Debug.LogError($"Missing required {typeof(T).Name} on GameObject {name}");
        return o;
    }
}
