using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private Vector3 _destination;

    [SerializeField]
    private float _speed;
    private void Start()
    {
        _destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != _destination)
        {
            //Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = new Vector3(destination.x, 1f, destination.z);
    }
}
