using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    // Créer un prefab "MusicBox",
    // Ce prefab sera constitué d'un sprite Square pour le fond,
    // Et de plusieurs sprites Square pour représenter les barres de son.
    // Ce prefab devra avoir un BoxCollider2D afin de détecter les collisions avec les particules.
    // Les particules doivent pouvoir passer au travers des MusicBox.

    // Créer un script MusicBox, et le placer sur le prefab.
    // Implémenter les choses suivantes dans le script:
    // Une variable privée de type float appelée _volume;
    // Faire en sorte que lorsqu'une particule entre en collision avec la MusicBox, _volume augmente d'un certain montant, paramétrable;
    // Si on passe un certain temps, paramétrable, sans collision avec une particule, on baisse _volume d'un certain montant par seconde, paramétrable;
    // Faire en sorte que _volume ne passe jamais en dessous de zero, ni jamais au dessus de un.

    // Pour ceux qui sont en avance:
    // Ajouter un AudioSource sur le MusicBox;
    // Y ajouter un AudioClip (du package fourni sur Discord) et paramétrer l'AudioSource afin qu'elle joue en boucle;
    // Faire en sorte que le volume de l'AudioSource soit corrélé avec la variable _volume du script MusicBox;
    // Changer la couleur des barres de son afin de représenter graphiquement le volume, via le script MusicBox.

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
