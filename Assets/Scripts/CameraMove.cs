using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float borderSize = 20f;
    public float zoomSpeed = 5f;
    public float minHeight = 10f;
    public float maxHeight = 30f;

    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow)) move += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow)) move += Vector3.down;
        if (Input.GetKey(KeyCode.LeftArrow)) move += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) move += Vector3.right;

        if (Input.mousePosition.y >= Screen.height - borderSize) move += Vector3.up;
        if (Input.mousePosition.y <= borderSize) move += Vector3.down;
        if (Input.mousePosition.x <= borderSize) move += Vector3.left;
        if (Input.mousePosition.x >= Screen.width - borderSize) move += Vector3.right;

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= scroll * zoomSpeed;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minHeight, maxHeight);

    }
}
