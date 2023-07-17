using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    private enum AIState { Walking, Attacking, Jumping, Death }

    [SerializeField]
    private AIState _aiState;

    private NavMeshAgent _ai;
    [SerializeField]
    private Transform[] _destinations;
    private int _waypoint = 0;

    private Vector3 _destination;

    private bool _reverse = false;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("destinations start: "+_destinations.ToString());
        _ai = gameObject.GetComponent<NavMeshAgent>();
        if (_ai != null)
        {
            UpdateDestination();
            StartCoroutine(MoveToNextWaypoint());
        }
        Debug.Log("destinations start: " + _destinations.ToString());
    }
    private void Update()
    {
        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _aiState = AIState.Jumping;
        }

        switch (_aiState)
        {
            case AIState.Walking:
                Debug.Log("Walking");
                _ai.isStopped = false;
                break;
            case AIState.Jumping:
                Debug.Log("Jumping");
                break;
            case AIState.Attacking:
                Debug.Log("Attacking");
                _ai.isStopped = true;
                StartCoroutine(Attacking());
                break;
            case AIState.Death:
                Debug.Log("Death");
                break;
        }
    }

    private void UpdateDestination()
    {
        if (_reverse)
        {
            _waypoint = (_waypoint + _destinations.Length - 1) % _destinations.Length;
        }
        else
        {
            _waypoint = (_waypoint + 1) % _destinations.Length;
        }
        //_waypoint = Random.Range(0, _destinations.Length);
        _destination = _destinations[_waypoint].position;
        _ai.SetDestination(_destination);
    }
    private IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (_ai.remainingDistance < 1)
            {
                _aiState = AIState.Attacking; // Set attacking state before the coroutine
                UpdateDestination();

                if (_waypoint == _destinations.Length - 1)
                {
                    _reverse = true;
                }
                else if (_waypoint == 0)
                {
                    _reverse = false;
                }

                yield return StartCoroutine(Attacking()); // Wait for the Attacking coroutine to finish
                _aiState = AIState.Walking; // Set back to walking state after the coroutine
            }

            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(1f);
    }
}
