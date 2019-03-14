using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ground : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.instance == null)
        {
            SceneManager.LoadScene("Splash");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Prop"))
        {
            PropBehaviour prop = collision.collider.GetComponent<PropBehaviour>();
            if (!prop.attached)
            {
                PropManager.instance.PropFallen(prop);
            }
            
        }
    }

}
