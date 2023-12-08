using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCullingController : MonoBehaviour
{
    public Transform target;
    private bool isTargetIn = false;

    public GameObject canvas;
    public GameObject renderCam;
    private void Update()
    {
        if (isTargetIn)
            return;
 
        Vector3 forwardDirection = transform.forward;
        Vector3 toTarget = (target.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(forwardDirection, toTarget);

        if (dotProduct > 0)
        {
            OffRenderTex();

            target.GetComponent<Camera>().cullingMask = (1 << LayerMask.NameToLayer("Cafe"));
            isTargetIn = true;
        }
    }

    private void OffRenderTex()
    {
        canvas.SetActive(false);
        renderCam.SetActive(false);
    }

}
