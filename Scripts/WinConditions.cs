using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditions : MonoBehaviour
{
    #region Exposed
    [SerializeField] private GameObject[] _musicBoxes;
    //private GameObject _musicBox;
    [SerializeField] private float[] _audioSources;
    [SerializeField] private float _timeMusicBoxesAreFull;
    [SerializeField] private float _baseTimeBeforeMusicBoxesAreFull = 3f;
    #endregion
    //private bool _areAllVolumeMaxed;
#region Lifecycle

   void Awake() 
    {
        _musicBoxes =  GameObject.FindGameObjectsWithTag("MusicBox");
        _audioSources = new float[_musicBoxes.Length];
        //_areAllVolumeMaxed = false;

        for (int i = 0; i < _musicBoxes.Length; i++)
        {
            _audioSources[i] = _musicBoxes[i].GetComponent<AudioSource>().volume;
        }
        _timeMusicBoxesAreFull = _baseTimeBeforeMusicBoxesAreFull;
    }


    
    void Update()
    {
        if(AreAllVolumeAtMax() == true)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            
        }

    }

   void FixedUpdate()
    {
       
    }

#endregion
#region Main Methods
    public bool AreAllVolumeAtMax()
    {
        for (int i = 0; i < _audioSources.Length; i++)
        {
            _audioSources[i] = _musicBoxes[i].GetComponent<AudioSource>().volume;
            if (_audioSources[0] == 1.0f && _audioSources[0] == _audioSources[i])
            {
                if (_timeMusicBoxesAreFull > 0)
                {
                    _timeMusicBoxesAreFull -= Time.deltaTime;

                }
                else  if (i == _audioSources.Length - 1 && _timeMusicBoxesAreFull < 0)
                {
                    return true;
                }
                
                continue;
            }
            else 
            {
                //break;
                _timeMusicBoxesAreFull = _baseTimeBeforeMusicBoxesAreFull;
                return false;
            }
        }
        return false;
    }
    
#endregion
#region Private & Protected
#endregion 
}
