using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour
    where T : class {

    public static T instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}