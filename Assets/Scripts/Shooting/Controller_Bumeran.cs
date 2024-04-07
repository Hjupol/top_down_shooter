using UnityEngine;

public class Controller_Bumeran : MonoBehaviour
{
    private Controller_Player parent;
    private Rigidbody rb;
    private CapsuleCollider collider;
    private Vector3 direction;
    public Vector3 startPos;
    public float maxDistance;
    public float bumeranSpeed;
    private float travelDistance;
    private float colliderTimer = 0.07f;
    private bool going;

    void Start()
    {
        parent = Controller_Player._Player;
        rb = GetComponent<Rigidbody>();
        Restart._Restart.OnRestart += Reset;
        collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
        going = true;
    }

    private void Reset()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        Rotate();
        if (going)
        {
            travelDistance = (startPos - transform.position).magnitude;
            if (travelDistance > maxDistance)
            {
                CheckDirection();
            }
        }
        else
        {
            ReturnToPlayer();
        }
    }

    void Update()
    {
        colliderTimer -= Time.deltaTime;
        if (colliderTimer < 0)
        {
            collider.enabled = true;
        }
        if (going)
        {
            travelDistance = (startPos - transform.position).magnitude;
        }
    }

    private void CheckDirection()
    {
        going = false;
        rb.velocity = Vector3.zero;
        if (Controller_Player._Player != null)
        {
            direction = -(this.transform.localPosition - parent.transform.localPosition).normalized;
        }
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(10, 0, 0));
    }

    private void ReturnToPlayer()
    {
        rb.AddForce(direction * bumeranSpeed);
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}
