using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public Camera cam;
    private Rigidbody rb;
    private Renderer render;
    public static Controller_Player _Player;
    private Vector3 movement;
    private Vector3 mousePos;
    internal Vector3 shootAngle;
    private Vector3 startPos;
    private bool started = false;
    public float speed = 5;

    private void Start()
    {
        if (_Player == null)
        {
            _Player = this.gameObject.GetComponent<Controller_Player>();
        }
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        Restart._Restart.OnRestart += Reset;
        started = true;
        Controller_Shooting._ShootingPlayer.OnShooting += Shoot;
    }

    private void OnEnable()
    {
        if (started)
            Restart._Restart.OnRestart += Reset;
    }

    private void Reset()
    {
        this.transform.position = startPos;
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public virtual void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        transform.LookAt(new Vector3(mousePos.x, 1, mousePos.z));
    }

    public Vector3 GetLastAngle()
    {
        if (Input.GetKey(KeyCode.W))
        {
            shootAngle = Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            shootAngle = Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            shootAngle = Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            shootAngle = Vector3.right;
        }
        return shootAngle;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            gameObject.SetActive(false);
            Controller_Hud.gameOver = true;
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            int rnd = UnityEngine.Random.Range(1, 3);
            if (rnd == 1)
            {
                Controller_Shooting.ammo = Ammo.Shotgun;
                Controller_Shooting.ammunition = 5;
            }
            else if (rnd == 2)
            {
                Controller_Shooting.ammo = Ammo.Cannon;
                Controller_Shooting.ammunition = 5;
            }
            else
            {
                Controller_Shooting.ammo = Ammo.Bumeran;
                Controller_Shooting.ammunition = 1;
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bumeran"))
        {
            Controller_Shooting.ammo = Ammo.Bumeran;
            Controller_Shooting.ammunition = 1;
            Destroy(collision.gameObject);
        }
    }

    void OnDisable()
    {
        Controller_Shooting._ShootingPlayer.OnShooting -= Shoot;
        Restart._Restart.OnRestart -= Reset;
    }

    private void Shoot()
    {
        if (Controller_Shooting.ammo == Ammo.Cannon)
        {
            rb.AddForce(this.transform.forward * -4f, ForceMode.Impulse);
        }
    }
}
