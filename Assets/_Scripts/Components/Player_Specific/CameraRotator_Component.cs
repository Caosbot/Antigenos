using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator_Component : MonoBehaviour
{
    private float xOffset; //Up
    private float yOffset; //down
    [SerializeField] private Vector2 offsetMultiplier;

    private void Start()
    {
        StartCoroutine(CalculateOffsset());
        offsetMultiplier *= 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private IEnumerator CalculateOffsset()
    {
        float factor = 0;
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            transform.Rotate(Vector3.up, (Input.GetAxis("Mouse X") * Time.deltaTime) * offsetMultiplier.x, Space.World);
            if (transform.rotation.eulerAngles.x > 45+factor && transform.rotation.eulerAngles.x < 55 + factor && Input.GetAxis("Mouse Y") < 0) { }
            else if (transform.rotation.eulerAngles.x < 330 + factor && transform.rotation.eulerAngles.x > 300 + factor && Input.GetAxis("Mouse Y") > 0) { }
            else
            {
                transform.Rotate(Vector3.right, (Input.GetAxis("Mouse Y") * Time.deltaTime) * (offsetMultiplier.y * -1), Space.Self);
            }
            yield return 0;
        }

    }
}
