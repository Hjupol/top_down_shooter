public class Following : Controller_Enemy
{
    private void FixedUpdate()
    {
        FollowingBehaviour();
    }

    private void FollowingBehaviour()
    {
        if (player != null )
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
