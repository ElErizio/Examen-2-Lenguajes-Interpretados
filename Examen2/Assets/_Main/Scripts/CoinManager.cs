public class CoinManager : Singleton<CoinManager>
{
    private int _countCounter = 0;

    public int OnCollectCoin()
    {
        _countCounter++;
        return _countCounter;
    }
}
