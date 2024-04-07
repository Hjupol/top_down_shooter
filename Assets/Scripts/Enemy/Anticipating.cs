public class Anticipating : Controller_Enemy
{
    private void FixedUpdate()
    {
        AnticipatingBehaviour();
    }

    private void AnticipatingBehaviour()
    {
        if (player != null)
        {
            Controller_Player p = player.GetComponent<Controller_Player>();
            agent.SetDestination(player.transform.position + p.GetLastAngle() * 2);
        }
    }
}
