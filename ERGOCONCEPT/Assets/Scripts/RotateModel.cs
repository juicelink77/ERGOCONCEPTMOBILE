using UnityEngine;
using UnityEngine.UI;

public class RotateModel : MonoBehaviour
{
    public RawImage Image;
    public GameObject Model;
    public Camera CameraUI;
    public float RotationSpeed = 5;
    public int DistanceToMakeToRotate = 20;
    private bool canRotate = false;
    private bool ImageClicked = false;
    private float mousePositionX = 0.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CameraUI.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == Image.gameObject)
                {
                    ImageClicked = true;
                    mousePositionX = Input.mousePosition.x;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ImageClicked = canRotate = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (canRotate && ImageClicked)
            {
                float rotationY = (Input.GetAxis("Mouse X") * RotationSpeed);
                Model.transform.Rotate(0, rotationY, 0, Space.World);
            }
            else
            {
                Vector3 mp = Input.mousePosition;
                float distance = Mathf.Abs(mousePositionX - mp.x);
                canRotate = distance > DistanceToMakeToRotate;
            }
        }
    }
}