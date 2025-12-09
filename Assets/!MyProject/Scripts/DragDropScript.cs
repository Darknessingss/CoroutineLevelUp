using UnityEngine;

public class DragDropScript : MonoBehaviour
{
    [SerializeField] private float dragForce = 10f;
    [SerializeField] private float maxDragDistance = 10f;
    [SerializeField] private float attractionForce = 15f;
    [SerializeField] private float damping = 0.5f;
    [SerializeField] private Camera targetCamera;

    private Rigidbody draggedObject;
    private Camera mainCamera;
    private Vector3 offset;
    private float fixedDistance;
    private bool isDragging = false;

    void Start()
    {
        if (targetCamera != null)
        {
            mainCamera = targetCamera;
        }
        else
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }
        }

        if (mainCamera == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        HandleMouseInput();
    }

    void FixedUpdate()
    {
        if (isDragging && draggedObject != null)
        {
            UpdateDraggedObject();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void StartDrag()
    {
        if (mainCamera == null)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        LayerMask draggableLayers = ~0;

        if (Physics.Raycast(ray, out hit, maxDragDistance, draggableLayers, QueryTriggerInteraction.Ignore))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (rb != null && !rb.isKinematic)
            {
                draggedObject = rb;
                isDragging = true;

                offset = hit.point - draggedObject.position;
                fixedDistance = Vector3.Distance(mainCamera.transform.position, hit.point);

                draggedObject.angularVelocity *= 0.1f;
            }
        }
    }

    private void UpdateDraggedObject()
    {
        if (mainCamera == null || draggedObject == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.origin + ray.direction * fixedDistance;

        Vector3 targetPosition = targetPoint - offset;

        Vector3 forceDirection = (targetPosition - draggedObject.position);
        float distance = forceDirection.magnitude;

        float distanceMultiplier = Mathf.Clamp(distance, 0.5f, 5f);

        Vector3 attractionForceVector = forceDirection.normalized * attractionForce * distanceMultiplier;

        Vector3 velocityError = (targetPosition - draggedObject.position) * dragForce;

        Vector3 totalForce = attractionForceVector + velocityError;

        draggedObject.linearVelocity *= (1 - damping * Time.fixedDeltaTime);

        draggedObject.AddForce(totalForce, ForceMode.Force);

        draggedObject.linearVelocity = Vector3.ClampMagnitude(draggedObject.linearVelocity, 20f);
    }

    private void EndDrag()
    {
        isDragging = false;
        draggedObject = null;
    }

    public void SetCamera(Camera newCamera)
    {
        if (newCamera != null)
        {
            mainCamera = newCamera;
        }
    }

    public void SetDragForce(float newForce)
    {
        dragForce = Mathf.Max(0, newForce);
    }

    public void SetAttractionForce(float newForce)
    {
        attractionForce = Mathf.Max(0, newForce);
    }

    public bool IsDragging()
    {
        return isDragging;
    }

    public GameObject GetDraggedObject()
    {
        return draggedObject != null ? draggedObject.gameObject : null;
    }
}