using UnityEngine;

public class BrickCollectingState : IState
{
    public void OnEnter(Bot bot)
    {
        //UIManager.Instance.CurrentState.text = this.ToString();
        bot.LookToTarget(bot.TargetBrick);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.MoveToBrick())
        {
            int tmp = Random.Range(0, 4);
            if (tmp == 0)
            {
                bot.ChangeState(new BridgeFindingState());
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
