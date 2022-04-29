using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    // Cr�er un prefab "MusicBox",
    // Ce prefab sera constitu� d'un sprite Square pour le fond,
    // Et de plusieurs sprites Square pour repr�senter les barres de son.
    // Ce prefab devra avoir un BoxCollider2D afin de d�tecter les collisions avec les particules.
    // Les particules doivent pouvoir passer au travers des MusicBox.

    // Cr�er un script MusicBox, et le placer sur le prefab.
    // Impl�menter les choses suivantes dans le script:
    // Une variable priv�e de type float appel�e _volume;
    // Faire en sorte que lorsqu'une particule entre en collision avec la MusicBox, _volume augmente d'un certain montant, param�trable;
    // Si on passe un certain temps, param�trable, sans collision avec une particule, on baisse _volume d'un certain montant par seconde, param�trable;
    // Faire en sorte que _volume ne passe jamais en dessous de zero, ni jamais au dessus de un.

    // Pour ceux qui sont en avance:
    // Ajouter un AudioSource sur le MusicBox;
    // Y ajouter un AudioClip (du package fourni sur Discord) et param�trer l'AudioSource afin qu'elle joue en boucle;
    // Faire en sorte que le volume de l'AudioSource soit corr�l� avec la variable _volume du script MusicBox;
    // Changer la couleur des barres de son afin de repr�senter graphiquement le volume, via le script MusicBox.

    #region Exposed

    [SerializeField] private float _volumeRaisePerParticles;
    [SerializeField] private float _volumeDecayPerSeconds;
    [SerializeField] private float _volumeDecayDelay;
    [SerializeField] private SpriteRenderer[] _volumeBars; // [] = tableau [x] x= l'index par ex _volumeBars[0]
    [SerializeField] private Color _enabledColor; // couleur des barres actives
    [SerializeField] private Color _disabledColor; // couleur des barres inactives
    #endregion
    private WinConditions _winConditions;

    #region Private & Protected
    private float _volume;
    private float _startDecayTime; // lheure a partir de laquel je dois baisser le temps
    private AudioSource _audioSource;

    #endregion
#region Lifecycle

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
     
    }
   
    void Update()
    {
        if(Time.time >= _startDecayTime) 
        {
            _volume = Mathf.Clamp01(_volume - _volumeDecayPerSeconds*Time.deltaTime); // avoir une baisse en vol par sec
        }

            _audioSource.volume = _volume;
        UpdateRenderers();
        
        
    }


    #endregion
    #region Main Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _volume = Mathf.Clamp01(_volume + _volumeRaisePerParticles); // la mem qu'au dessus en + epure
        _startDecayTime = Time.time + _volumeDecayDelay;
    }
    private void UpdateRenderers()
    {
        int barsToEnable = Mathf.FloorToInt(_volumeBars.Length * _volume); // environs entre 0 et 1 car volume est entre - et 1 et bars 10

        for (int i = 0; i < _volumeBars.Length; i++)
        {
            if(i< barsToEnable) { _volumeBars[i].color = _enabledColor;}
            else { _volumeBars[i].color = _disabledColor; }  
        }

    }
    #endregion

}
