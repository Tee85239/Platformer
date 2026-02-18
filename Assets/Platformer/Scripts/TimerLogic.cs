using TMPro;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI Timer;
    float time = 500;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        Timer.text = "TIME: " + ((int)time).ToString();

        
    }
}
