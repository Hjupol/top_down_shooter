using UnityEngine;

public class Restart : MonoBehaviour
{
    public static Restart _Restart;
    public delegate void Restarting();
    public event Restarting OnRestart;

    private void Awake()
    {
        if (_Restart == null)
        {
            _Restart = this.gameObject.GetComponent<Restart>();
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRestarting();
        }
    }

    public void OnRestarting()
    {
        Time.timeScale = 0;
        Controller_Player._Player.gameObject.SetActive(true);
        OnRestart();
        Time.timeScale = 1;
    }
}
