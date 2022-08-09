public interface INetworkViewThread
{
    public void SetModel(ICharacterModel model);
    public void StartThread(CharacterNetworkState state);
}
