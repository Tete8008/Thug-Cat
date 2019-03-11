using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour
    where T : class {

    public static T instance = null;
    public static GameObject instanceObject=null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            instanceObject = gameObject;
        }
        else
        {
            Destroy(instanceObject);
            instance = GetComponent<T>();
        }
    }
}