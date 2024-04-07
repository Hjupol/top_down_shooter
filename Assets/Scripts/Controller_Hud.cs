using UnityEngine;
using UnityEngine.UI;

public class Controller_Hud : MonoBehaviour
{
    public static bool gameOver;
    public static int points;
    private Ammo ammo;
    public Text gameOverText;
    public Text pointsText;
    public Text powerUpText;

    void Start()
    {
        Restart._Restart.OnRestart += Reset;
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        points = 0;
    }

    private void Reset()
    {
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        points = 0;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverText.text = "Game Over";
            gameOverText.gameObject.SetActive(true);
        }

        switch (Controller_Shooting.ammo)
        {
            case Ammo.Normal:
                powerUpText.text = "Gun: Normal - Ammo:∞";
                break;
            case Ammo.Shotgun:
                powerUpText.text = "Gun: Shotgun - Ammo:" + Controller_Shooting.ammunition.ToString();
                break;
            case Ammo.Cannon:
                powerUpText.text = "Gun: Cannon - Ammo:" + Controller_Shooting.ammunition.ToString();
                break;
            case Ammo.Bumeran:
                powerUpText.text = "Gun: Bumeran - Ammo:" + Controller_Shooting.ammunition.ToString();
                break;
        }

        pointsText.text = "Score: " + points.ToString();
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}
