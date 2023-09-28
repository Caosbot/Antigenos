using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Aim_Component
{
    [System.NonSerialized] private Transform          aimPosition;
    [SerializeField] private float              aimSpeed = 20;
    [SerializeField] private LayerMask          aimMask;
    [System.NonSerialized] public Transform     hitTransform;
    [System.NonSerialized] public RaycastHit    hitR;

    public void Start(GameObject aimCube)
    {
        aimPosition = aimCube.transform;
        aimCube.name = "PlayerAimCube";
    }
    public void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPosition.position = Vector3.Lerp(aimPosition.position, hit.point, aimSpeed * Time.deltaTime);
            hitTransform = hit.transform;
            hitR = hit;
        }
        Debug.DrawRay(ray.GetPoint(0), ray.GetPoint(100));
    }
}
