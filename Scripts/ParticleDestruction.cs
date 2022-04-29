using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestruction : MonoBehaviour
{
    #region Exposed
    [Tooltip("Si la vitesse de l'objet passe ne dessous de minspeed, il est detruit")]
    [SerializeField] private float _minSpeed;

    private Rigidbody2D _rb;
#endregion

#region Lifecycle

   void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       
    }


    void Update()
    {
        if (_rb.velocity.magnitude < _minSpeed) { gameObject.SetActive(false); }
    }

   void FixedUpdate()
    {
       
    }

#endregion
#region Main Methods
#endregion
#region Private & Protected
#endregion 
}
