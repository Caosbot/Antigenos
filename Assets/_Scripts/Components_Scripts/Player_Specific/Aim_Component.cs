using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Component : MonoBehaviour
{
    [SerializeField] private Transform aimPosition;
    [SerializeField] private float aimSpeed = 20;
    [SerializeField] LayerMask aimMask;

    private void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPosition.position = Vector3.Lerp(aimPosition.position, hit.point, aimSpeed * Time.deltaTime);
        }
        Debug.DrawRay(ray.GetPoint(0), ray.GetPoint(100));
    }
}
