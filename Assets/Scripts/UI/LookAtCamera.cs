using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Mode mode;
    enum Mode{
        lookForward,
        lookInvert,
        cameraForward,
        cameraInvert,
    }
    private void LateUpdate() {
        switch (mode) {
            case Mode.lookForward:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.lookInvert:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(dirFromCamera);
                break;
            case Mode.cameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.cameraInvert:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
    }
}
