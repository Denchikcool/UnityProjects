using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SelectController : MonoBehaviour
{
    public GameObject SqereCube;
    public LayerMask LayerMask, LayerMaskPlayer;
    public List<GameObject> Players;

    private Camera _camera;
    private GameObject _cubeSelection;
    private RaycastHit _hit;
    private float _xScale, _zScale;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && Players.Count > 0)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, LayerMask))
            {
                foreach (var player in Players)
                {
                    player.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
                }
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            foreach(var element in Players)
            {
                if(element != null)
                {
                    element.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            Players.Clear();

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out _hit, 1000f, LayerMask))
            {
                _cubeSelection = Instantiate(SqereCube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        if (_cubeSelection)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, LayerMask))
            {
                _xScale = (_hit.point.x - hitDrag.point.x) * -1;
                _zScale = _hit.point.z - hitDrag.point.z;

                if(_xScale < 0.0f && _zScale < 0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else if (_xScale < 0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (_zScale < 0.0f)
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                else
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                _cubeSelection.transform.localScale = new Vector3(Mathf.Abs(_xScale), 1, Mathf.Abs(_zScale));
            }
        }

        if(Input.GetMouseButtonUp(0) && _cubeSelection) 
        {
            RaycastHit[] hits =  Physics.BoxCastAll(_cubeSelection.transform.position, _cubeSelection.transform.localScale, Vector3.up, Quaternion.identity, 0, LayerMaskPlayer);

            foreach(var element in hits)
            {
                if (element.collider.CompareTag("Enemy")) continue;

                Players.Add(element.transform.gameObject);
                element.transform.GetChild(0).gameObject.SetActive(true);
            }

            Destroy(_cubeSelection);
        }
    }
}
