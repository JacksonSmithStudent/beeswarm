using UnityEngine;

public class CameraCycleController : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex = -1;

    void Start()
    {
        DisableAllCameras();
    }

    public void EnableCamera(int index)
    {
        if (index < 0 || index >= cameras.Length) return;

        DisableAllCameras();

        cameras[index].gameObject.SetActive(true);
        currentCameraIndex = index;
    }

    public void DisableAllCameras()
    {
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }
        currentCameraIndex = -1;
    }

    // Call this to cycle to the next camera in the array
    public void CycleNextCamera()
    {
        if (cameras.Length == 0) return;

        int nextIndex = currentCameraIndex + 1;
        if (nextIndex >= cameras.Length)
            nextIndex = 0;

        EnableCamera(nextIndex);
    }

    // Call this to cycle to the previous camera in the array
    public void CyclePreviousCamera()
    {
        if (cameras.Length == 0) return;

        int prevIndex = currentCameraIndex - 1;
        if (prevIndex < 0)
            prevIndex = cameras.Length - 1;

        EnableCamera(prevIndex);
    }
}
