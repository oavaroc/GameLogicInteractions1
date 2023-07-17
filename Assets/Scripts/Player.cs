using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputAsset _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputAsset();
        _input.Player.Enable();
        _input.Player.ChangeColor.performed += ChangeColor_performed;
    }

    private void ChangeColor_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        Debug.DrawRay(transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,10f))
        {
            Debug.Log("Did Hit");
            hit.transform.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
        else
        {
            Debug.Log("Did Not Hit");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
