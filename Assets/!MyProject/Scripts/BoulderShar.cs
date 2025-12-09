using UnityEngine;

public class BoulderShar : MonoBehaviour
{
    [SerializeField] private GameObject pendulumBall;
    [SerializeField] private int chainLinks = 8;
    [SerializeField] private float chainLinkLength = 0.5f;
    [SerializeField] private float ballMass = 35f;
    [SerializeField] private float platformSpeed = 2f;
    [SerializeField] private float platformRange = 3f;

    private Rigidbody pendulumRb;
    private GameObject[] chainLinksArray;
    private Vector3 platformStartPosition;
    private float platformTimer = 0f;

    void Start()
    {
        platformStartPosition = transform.position;
        CreatePendulum();
    }

    void Update()
    {
            MovePlatform();
    }

    private void CreatePendulum()
    {
        chainLinksArray = new GameObject[chainLinks];
        GameObject previousLink = new GameObject("ChainStart");
        previousLink.transform.position = transform.position;
        Rigidbody startRb = previousLink.AddComponent<Rigidbody>();
        startRb.isKinematic = true;
        previousLink.transform.parent = transform;

        for (int i = 0; i < chainLinks; i++)
        {
            GameObject link = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            link.name = $"ChainLink_{i}";
            link.transform.localScale = new Vector3(0.1f, chainLinkLength, 0.1f);

            Vector3 linkPosition = previousLink.transform.position;
            linkPosition.y -= chainLinkLength;
            link.transform.position = linkPosition;

            link.transform.rotation = Quaternion.identity;

            Rigidbody linkRb = link.AddComponent<Rigidbody>();
            linkRb.mass = 0.1f;
            linkRb.linearDamping = 1f;
            linkRb.angularDamping = 1f;

            CapsuleCollider collider = link.GetComponent<CapsuleCollider>();
            collider.height = 1f;
            collider.direction = 1;

            HingeJoint joint = link.AddComponent<HingeJoint>();
            joint.connectedBody = previousLink.GetComponent<Rigidbody>();
            joint.axis = Vector3.forward;
            joint.anchor = new Vector3(0, 0.5f, 0);
            joint.connectedAnchor = new Vector3(0, -0.5f, 0);

            JointLimits limits = new JointLimits();
            limits.min = -1f;
            limits.max = 1f;
            joint.limits = limits;
            joint.useLimits = true;

            JointSpring spring = new JointSpring();
            spring.spring = 100f;
            spring.damper = 5f;
            joint.spring = spring;
            joint.useSpring = true;

            chainLinksArray[i] = link;
            previousLink = link;
        }

        if (pendulumBall == null)
        {
            pendulumBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pendulumBall.name = "BouncerBall";
            pendulumBall.transform.localScale = Vector3.one * 0.8f;
        }

        Vector3 ballPosition = previousLink.transform.position;
        ballPosition.y -= chainLinkLength * 0.5f;
        pendulumBall.transform.position = ballPosition;

        pendulumRb = pendulumBall.GetComponent<Rigidbody>();
        if (pendulumRb == null)
        {
            pendulumRb = pendulumBall.AddComponent<Rigidbody>();
        }
        pendulumRb.mass = ballMass;
        pendulumRb.linearDamping = 0.5f;
        pendulumRb.angularDamping = 0.5f;

        pendulumBall.GetComponent<SphereCollider>().radius = 0.4f;

        HingeJoint ballJoint = pendulumBall.AddComponent<HingeJoint>();
        ballJoint.connectedBody = previousLink.GetComponent<Rigidbody>();
        ballJoint.axis = Vector3.forward;
        ballJoint.anchor = new Vector3(0, 0.4f, 0);
        ballJoint.connectedAnchor = new Vector3(0, -0.5f, 0);

        JointLimits ballLimits = new JointLimits();
        ballLimits.min = -2f;
        ballLimits.max = 2f;
        ballJoint.limits = ballLimits;
        ballJoint.useLimits = true;

        Renderer ballRenderer = pendulumBall.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.material.color = Color.red;
        }
    }

    private void MovePlatform()
    {
        platformTimer += Time.deltaTime * platformSpeed;

        float horizontalOffset = Mathf.Sin(platformTimer) * platformRange;

        Vector3 newPosition = platformStartPosition + transform.right * horizontalOffset;

        transform.position = newPosition;
    }
}