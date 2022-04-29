using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawner : MonoBehaviour
{
    #region Exposed
    [SerializeField] private ParticlePool _pool;

    [Header("Spawner Params")]
    [Tooltip("Le GameObject dans lequel sera contenu les particules dans la hierarchie")]
    [SerializeField] Transform _particleContainer;
    [Tooltip("Rayons d'apparition des particules")]
    [Range(.1f, 3f)]
    [SerializeField] private float _spawnRadius = 0.5f;
    [Tooltip("Vitesse d'apparitions des particules")]
    [Range(.1f, 10f)]
    [SerializeField] private float _fireRate = 2.0f;

    [Header("Particles Params")]
    [Tooltip("Vitesse des initiales des Particule en m/s")]
    [Range(.1f, 30f)]
    [SerializeField] private float _particleSpeed = 5.0f;
    [Tooltip("Gere le 'drag' cad la force de frotement des particules cad la vitesse a laquelle elles ralentissent")]
    [SerializeField] private float _particulDrag;

    [Header("Gizmos Params")]
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private Color _gizmosColor;

    private float _nextFire;
    private Transform _transform;

    #endregion

    #region Lifecycle

   void Awake() { _transform = transform; }
    

    void Start(){ }

    
    void Update()
    {
        if(Time.time > _nextFire)
        {
            GameObject newParicle = SpawnParticle();
            LunchParticle(newParicle);
            _nextFire = Time.time + _fireRate;
        }
        
    }

   void FixedUpdate() { }

#endregion
#region Main Methods
    private GameObject SpawnParticle() 
    {

        Vector2 position = Random.insideUnitCircle * _spawnRadius + (Vector2)_transform.position; //new Vector2(transform.position.x, transform.position.y);     

        GameObject particle = _pool.GetParticle();
        if(particle != null) // pour eviter les erruer si le pool est trop petit
        {
        particle.SetActive(true);
        particle.transform.position = position;

        particle.GetComponent<TrailRenderer>().Clear(); // clear pour virer les histo des points 
        }
        return particle;
    }
    private void LunchParticle(GameObject particle)
    {
        if(particle != null)
        {
        Rigidbody2D rb2d = particle.GetComponent<Rigidbody2D>();
        rb2d.drag = _particulDrag;
        rb2d.velocity = _transform.right * _particleSpeed; // transform.right correspond a l'axe local

        }
    }

    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        if (!_drawGizmos) { return; } // si _drawGizmos nm'est pas coche return donc on sort de la fontion
        // ou if(_drawGizmos){}
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        Gizmos.DrawRay(transform.position, transform.right * _particleSpeed);
    }
    #endregion
    #region Private & Protected
    #endregion
}
