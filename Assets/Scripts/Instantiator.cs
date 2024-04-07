using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public static List<Controller_Enemy> enemies;
    public List<GameObject> enemy;
    public List<GameObject> positions;
    public GameObject powerUp;
    private GameObject powerUpInstance;
    private float initialWaveDuration, initialAumentedWaveDuration, initialPowerUpTime;
    public int wave = 1;
    public float waveDuration = 5, aumentedWaveDuration = 3, powerUpTime = 10;

    private void Start()
    {
        enemies = new List<Controller_Enemy>();
        initialWaveDuration = waveDuration;
        initialAumentedWaveDuration = aumentedWaveDuration;
        initialPowerUpTime = powerUpTime;
        Restart._Restart.OnRestart += Reset;
        SpawnEnemies();
    }

    private void Reset()
    {
        waveDuration = initialWaveDuration;
        aumentedWaveDuration = initialAumentedWaveDuration;
        powerUpTime = initialPowerUpTime;
        wave = 1;
        if (powerUpInstance != null)
            Destroy(powerUpInstance);
        foreach (Controller_Enemy c in enemies)
        {
            c.Reset();
        }
        SpawnEnemies();
    }

    private void Update()
    {
        waveDuration -= Time.deltaTime;
        powerUpTime -= Time.deltaTime;
        if (powerUpTime < 0)
        {
            SpawnPowerUp();
        }
        if (waveDuration < 0)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (!Controller_Hud.gameOver)
        {
            int enemiesCount = wave * 2;
            for (int i = 0; i < enemiesCount; i++)
            {
                int random = UnityEngine.Random.Range(0, positions.Count);
                GameObject enemyInstance = Instantiate(enemy[UnityEngine.Random.Range(0, enemy.Count)], positions[random].transform.position, Quaternion.identity);
                enemies.Add(enemyInstance.GetComponent<Controller_Enemy>());
            }
            aumentedWaveDuration += 0.3f;
            waveDuration = aumentedWaveDuration;
            wave++;
        }
    }

    private void SpawnPowerUp()
    {
        Vector3 randomizer = new Vector3(UnityEngine.Random.Range(-7, 7), 1, UnityEngine.Random.Range(-7, 7));
        powerUpInstance = Instantiate(powerUp, randomizer, Quaternion.identity);
        Destroy(powerUpInstance, 10);
        powerUpTime = 20;
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}
