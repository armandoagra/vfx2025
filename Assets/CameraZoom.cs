using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minRadius = 2f;
    [SerializeField] private float maxRadius = 10f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            freeLook.m_Orbits[0].m_Radius = Mathf.Clamp(freeLook.m_Orbits[0].m_Radius - scroll * zoomSpeed, minRadius, maxRadius);
            freeLook.m_Orbits[1].m_Radius = Mathf.Clamp(freeLook.m_Orbits[1].m_Radius - scroll * zoomSpeed, minRadius, maxRadius);
            freeLook.m_Orbits[2].m_Radius = Mathf.Clamp(freeLook.m_Orbits[2].m_Radius - scroll * zoomSpeed, minRadius, maxRadius);
        }
        if (Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetMouseButtonUp(1))
            Cursor.lockState = CursorLockMode.None;
    }
}
