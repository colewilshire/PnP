using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SelectionHandler : NetworkBehaviour
{
    [SerializeField] private DynastyCharacter selectedCharacter;
    [SerializeField] private Camera followCamera;

    // Start is called before the first frame update
    private void Start()
    {
        if (!IsOwner) return;
    }

    // Update is called once per frame
    private void Update()
    {
        //RaycastFromCamera();
    }

    private void RaycastFromCamera()
    {
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

        DynastyCharacter hitCharacter = hit.collider.GetComponent<DynastyCharacter>();
        
        if (hitCharacter)
        {
            selectedCharacter = hitCharacter;
            //hitCharacter.Move(hitCharacter.Position);
        }
    }
}
