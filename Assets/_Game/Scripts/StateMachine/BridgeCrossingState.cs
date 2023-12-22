using UnityEngine;

public class BridgeCrossingState : IState
{
    public void OnEnter(Bot bot)
    {
        //UIManager.Instance.CurrentState.text = this.ToString();
        bot.BridgeEndPos = new Vector3(bot.BridgeController.EndPosition.x, 1.5f, bot.BridgeController.EndPosition.z);
        bot.LookToTarget(bot.BridgeEndPos);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.GetBrickCount() == 0)
        {
            bot.ChangeState(new BridgeFindingState());
        }
        else if (bot.TryMoveToEndBridge())
        {
            bot.ChangeState(new BrickFindingState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}

