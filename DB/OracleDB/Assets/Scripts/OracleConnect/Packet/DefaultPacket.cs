using System;

[Serializable]
public abstract class DefaultPacket
{
    public string protocol;

    public DefaultPacket()
    {
        protocol = this.GetType().ToString();
    }
}

[Serializable]
public class DT3TruckInfo
{
    public string CAR_CD;
    public string DISTANCE;
    public string ARRIVE_DATE;

    public DT3TruckInfo()
    {
        CAR_CD = string.Empty;
        DISTANCE = string.Empty;
        ARRIVE_DATE = string.Empty;
    }

    public DT3TruckInfo(string _CAR_CD, string _DISTANCE, string _ARRIVE_DATE)
    {
        CAR_CD = _CAR_CD;
        DISTANCE = _DISTANCE;
        ARRIVE_DATE = _ARRIVE_DATE;
    }
}