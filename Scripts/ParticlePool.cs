using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private int _amountOfParticles;

    private GameObject[] _particles; //garder en ref tt les particles cree dans la pool


   void Awake()
    {
        _particles = new GameObject[_amountOfParticles];//on cree un tyableau de go avec un certain nb d'elem

        for(int i = 0 ; i < _amountOfParticles ; i++)
        {
            _particles[i] = Instantiate(_particlePrefab, transform); // on cree est particle
            _particles[i].SetActive(false); // desactive de base
        }
    }

    public GameObject GetParticle() //on l'appel dans spawner
    {
        for(int i = 0; i < _amountOfParticles; i++) //on va chercher dans toute la pool
        {
            if (!_particles[i].activeInHierarchy) // si la part selectionne est desactivé
            {
                return _particles[i]; // on retourne la ref de cette particule
            }
        }

        return null;//on peut retourner null si ya plus de stock
    }
}
