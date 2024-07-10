using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public Transform[] Points;
    public GameObject Factory;

    private float _secondsTime = 10f;
    private GameObject _spawnCar;

    private void Start()
    {
        StartCoroutine(SpawnFactory());
    }

    IEnumerator SpawnFactory()
    {
        for(int i = 0; i < Points.Length; i++)
        {
            yield return new WaitForSeconds(_secondsTime);
            _spawnCar = Instantiate(Factory);
            Destroy(_spawnCar.GetComponent<PlaceObjects>());
            _spawnCar.transform.position = Points[i].position;
            _spawnCar.transform.rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            _spawnCar.GetComponent<AutoCarCreate>().enabled = true;
            _spawnCar.GetComponent<AutoCarCreate>().IsEnemy = true;
        }
    }
}
