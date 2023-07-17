using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private PlayerInputAsset _input;

    [SerializeField]
    private GameObject _bulletHole;

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
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Instantiate(_bulletHole, hit.point + hit.normal * Random.Range(0.001f, 0.01f), Quaternion.LookRotation(hit.normal));
        }
    }

}
