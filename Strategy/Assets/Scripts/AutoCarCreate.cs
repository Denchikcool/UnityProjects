using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarCreate : MonoBehaviour
{
    [NonSerialized] public bool IsEnemy = false;
    public GameObject Car;
    public float TimeToSpawn = 5.0f;
    

    private int _maxCars = 3;
    private GameObject _newCar;
    private Vector3 _position;

    private void Start()
    {
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        for(int i = 1; i <= _maxCars; i++)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            _position = new Vector3(transform.GetChild(0).position.x + UnityEngine.Random.Range(3f, 7f), transform.GetChild(0).position.y, transform.GetChild(0).position.z + UnityEngine.Random.Range(3f, 7f));
            _newCar = Instantiate(Car, _position, Quaternion.identity);

            if (IsEnemy)
            {
                _newCar.tag = "Enemy";
            }
        }
    }
}
