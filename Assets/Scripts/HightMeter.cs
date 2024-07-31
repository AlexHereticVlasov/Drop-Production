public sealed class HightMeter
{
    private float _startYPosition = 0;
    private float _earthYPosition;

    public HightMeter(Earth earth)
    {
        _earthYPosition = earth.transform.position.y;
    }
}