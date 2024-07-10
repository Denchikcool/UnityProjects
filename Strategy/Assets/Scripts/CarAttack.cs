using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAttack : MonoBehaviour
{
    [NonSerialized] public int Health = 100;
    public float Radius = 70f;
    public GameObject Bullet;

    private Collider[] _hitColliders;
    private GameObject _bulletObject;
    private Coroutine _coroutine = null;

    private void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        _hitColliders = Physics.OverlapSphere(transform.position, Radius);

        if (_hitColliders.Length == 0 && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            if (gameObject.CompareTag("Enemy"))
            {
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
            }
        }

        foreach(var element in _hitColliders)
        {
            if((gameObject.CompareTag("Player") && element.gameObject.CompareTag("Enemy")) || (gameObject.CompareTag("Enemy") && element.gameObject.CompareTag("Player")))
            {
                if (gameObject.CompareTag("Enemy"))
                {
                    GetComponent<NavMeshAgent>().SetDestination(element.transform.position);
                }

                if(_coroutine == null)
                {
                    _coroutine = StartCoroutine(StartAttack(element));
                }
            }
        }
    }

    IEnumerator StartAttack(Collider enemyPosition)
    {
        _bulletObject = Instantiate(Bullet, transform.GetChild(1).position, Quaternion.identity);
        _bulletObject.GetComponent<BulletController>().Position = enemyPosition.transform.position;
        yield return new WaitForSeconds(1f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
