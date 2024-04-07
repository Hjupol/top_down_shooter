using UnityEngine;

public class Controller_Shooting : MonoBehaviour
{
    public delegate void Shooting();
    public event Shooting OnShooting;
    public static Ammo ammo;
    public static int ammunition;
    public static Controller_Shooting _ShootingPlayer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject cannonPrefab;
    public GameObject bumeranPrefab;
    public float bulletForce = 20f;
    private bool started = false;

    private void Awake()
    {
        if (_ShootingPlayer == null)
        {
            _ShootingPlayer = this.gameObject.GetComponent<Controller_Shooting>();
            Debug.Log("Shooting es nulo");
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (_ShootingPlayer == null)
        {
            _ShootingPlayer = this.gameObject.GetComponent<Controller_Shooting>();
        }

        Restart._Restart.OnRestart += Reset;
        started = true;
        ammo = Ammo.Bumeran;
        ammunition = 1;
    }

    private void OnEnable()
    {
        if (started)
            Restart._Restart.OnRestart += Reset;
    }

    private void Reset()
    {
        ammo = Ammo.Bumeran;
        ammunition = 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            CheckAmmo();
        }
    }

    private void CheckAmmo()
    {
        if (ammunition <= 0)
        {
            ammo = Ammo.Normal;
        }
    }

    private void Shoot()
    {
        if (OnShooting != null)
        {
            OnShooting();
        }
        if (ammo == Ammo.Normal)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else if (ammo == Ammo.Shotgun)
        {
            Rigidbody rb;
            for (float i = -0.2f; i < 0.2f; i += 0.1f)
            {
                rb = null;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(firePoint.forward.x + i, firePoint.forward.y, firePoint.forward.z + i) * bulletForce, ForceMode.Impulse);
            }
            ammunition--;
        }
        else if (ammo == Ammo.Cannon)
        {
            GameObject bullet = Instantiate(cannonPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            ammunition--;
        }
        else if (ammo == Ammo.Bumeran)
        {
            GameObject bullet = Instantiate(bumeranPrefab, firePoint.position, firePoint.rotation);
            Controller_Bumeran bm = bullet.GetComponent<Controller_Bumeran>();
            bm.startPos = firePoint.position;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            ammunition--;
        }
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}

public enum Ammo
{
    Normal,
    Shotgun,
    Cannon,
    Bumeran
}
