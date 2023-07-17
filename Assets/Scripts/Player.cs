using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputAsset _input;

    [SerializeField]
    private GameObject _sphere;


    [SerializeField]
    private CubeMove _cube;

    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputAsset();
        _input.Player.Enable();
        _input.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Interact performed");
        Ray rayOrigin = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Debug.DrawRay(transform.position, rayOrigin.direction,Color.blue,5f);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit,Mathf.Infinity,1 << 8))
        {
            Debug.Log("Hit: " + hit.collider.name);
            ClickToMove(hit);
        }
    }

    private void ClickToMove(RaycastHit hit)
    {
        _cube.SetDestination(hit.point);
    }

    private void HitEnemy(RaycastHit hit)
    {
        var hitObj = hit.transform.GetComponent<MeshRenderer>();
        if (hitObj != null)
        {
            Debug.Log("Hit Enemy");
            hitObj.material.color = Color.red;

        }
    }

    private void SpawnGameObject(RaycastHit hit)
    {
        Debug.Log("Spawning sphere");
        Instantiate(_sphere, hit.point, Quaternion.identity);

    }

    private void ColorChange(RaycastHit hit)
    {
        var hitObj = hit.transform.GetComponent<MeshRenderer>();
        if(hitObj != null)
        {
            switch (hitObj.tag)
            {
                case "Cube":
                    Debug.Log("Hit Cube");
                    hitObj.material.color = Random.ColorHSV();
                    break;
                case "Capsule":
                    Debug.Log("Hit Capsule");
                    hitObj.material.color = Color.black;
                    break;
                case "Sphere":
                    Debug.Log("Hit Sphere");
                    break;
            }
                
        }
    }

}
