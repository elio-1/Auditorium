using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowCurrentLevel : MonoBehaviour
{
   
    private void Awake()
    {
        GetComponent<TextMesh>().text = SceneManager.GetActiveScene().name;
        
    }
}
