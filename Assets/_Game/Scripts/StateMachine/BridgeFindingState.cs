public class BridgeFindingState : IState
{
    public void OnEnter(Bot bot)
    {
        //UIManager.Instance.CurrentState.text = this.ToString();
        bot.LookToTarget(bot.BridgeStartPos);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.MoveToBridge())
        {
            if (bot.GetBrickCount() > 0)
            {
                bot.ChangeState(new BridgeCrossingState());
            }
            else
            {
                bot.ChangeState(new BrickFindingState());
            }
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
