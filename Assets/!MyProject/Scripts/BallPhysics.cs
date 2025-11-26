using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [Header("��������� ����")]
    [SerializeField] private float _speedThreshold = 2f;
    [SerializeField] private Color _slowColor = Color.red;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Renderer _renderer;
    private float _slowSpeedTimer = 0f;
    private bool _isBelow = false;
    private Color _originalColor;

    void Start()
    {

        _originalColor = _renderer.material.color;

        _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        float currentSpeed = _rb.linearVelocity.magnitude;

        if (currentSpeed < _speedThreshold)
        {
            if (!_isBelow)
            {
                _slowSpeedTimer = Time.time;
                _renderer.material.color = _slowColor;
                _isBelow = true;
            }
        }
        else
        {
            if (_isBelow)
            {
                _isBelow = false;
                _renderer.material.color = _originalColor;

                float slowDuration;
                do
                {
                    slowDuration = Time.time - _slowSpeedTimer;
                } while (slowDuration < 0.0001f);
            }
        }
    }
}