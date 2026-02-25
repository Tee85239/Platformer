using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
