public sealed class HightMeter
{
    private readonly float _startYPosition = 0;
    private readonly float _earthYPosition;

    public HightMeter(Earth earth)
    {
        _earthYPosition = earth.transform.position.y;
    }
}