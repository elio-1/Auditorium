using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    #region Exposed
    [SerializeField] TextMeshProUGUI  timeCounter;
    private System.TimeSpan timePlaying;
    
    private float elapsedTime;
    
#endregion

#region Lifecycle

   void Awake()
    {
        DontDestroyOnLoad(transform.parent);
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        elapsedTime = 0f;
    }

    
    void Update()
    {
        
    }
    
   void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            elapsedTime = 0f;
        }
        if (SceneManager.GetActiveScene().name !="Win")
        {

        MyTimer();
        }
    }

#endregion
#region Main Methods
    public void MyTimer()
    {
        elapsedTime += Time.deltaTime;
        timePlaying = System.TimeSpan.FromSeconds(elapsedTime);
        timeCounter.text = "Time:" + timePlaying.ToString("mm':'ss'.'ff");

    }
#endregion
#region Private & Protected
#endregion 
}
