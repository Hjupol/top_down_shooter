using UnityEngine;

public class RandomEnemy : Controller_Enemy
{
    private void FixedUpdate()
    {
        PatrolBehaviour();
    }

    private void PatrolBehaviour()
    {
        agent.SetDestination(destination);
        destinationTime -= Time.deltaTime;
        if (destinationTime < 0)
        {
            destination = new Vector3(Random.Range(-10, 12), 1, Random.Range(-12, 9));
            destinationTime = 4;
        }
    }
}
