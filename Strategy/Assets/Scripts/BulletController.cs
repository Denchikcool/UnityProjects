using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [NonSerialized] public Vector3 Position;
    public float Speed = 30f;

    private float _stepByFrame;
    private CatAttack _attack;
    private int _damage = 20;
    private Transform _healthBar;

    private void Update()
    {
        _stepByFrame = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Position, _stepByFrame);

        if (transform.position == Position)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            _attack = other.GetComponent<CatAttack>();
            _attack.Health -= _damage;

            _healthBar = other.transform.GetChild(0).transform;
            _healthBar.localScale = new Vector3(_healthBar.localScale.x - 0.3f, _healthBar.localScale.y, _healthBar.localScale.z);

            if(_attack.Health <= 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
