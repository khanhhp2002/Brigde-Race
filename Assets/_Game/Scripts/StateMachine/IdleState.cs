public class IdleState : IState
{
    public void OnEnter(Bot bot)
    {
        UIManager.Instance.CurrentState.text = this.ToString();
    }

    public void OnExecute(Bot bot)
    {

    }

    public void OnExit(Bot bot)
    {

    }
}
