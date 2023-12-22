using UnityEngine;

public class BrickFindingState : IState
{
    private int attempt;
    private float _maxProgressTime = 10f;
    private float _progressTimeCount;
    public void OnEnter(Bot bot)
    {
        //UIManager.Instance.CurrentState.text = this.ToString();
        attempt = 1;
        _progressTimeCount = 0;
    }

    public void OnExecute(Bot bot)
    {
        if (bot.FindBrick(attempt))
        {
            bot.ChangeState(new BrickCollectingState());
        }
        else
        {
            attempt++;
        }
        _progressTimeCount += Time.deltaTime;
        if (_progressTimeCount >= _maxProgressTime)
        {
            bot.ChangeState(new BridgeFindingState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
