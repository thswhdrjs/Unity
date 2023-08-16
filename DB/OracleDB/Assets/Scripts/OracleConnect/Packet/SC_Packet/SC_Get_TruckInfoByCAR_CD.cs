using System;

[Serializable]
public class SC_Get_TruckInfoByCAR_CD : DefaultPacket
{
    public string CAR_CD;
    public string DISTANCE;
    public string ARRIVE_DATE;

    public SC_Get_TruckInfoByCAR_CD()
    {
        CAR_CD = string.Empty;
        DISTANCE = string.Empty;
        ARRIVE_DATE = string.Empty;
    }

    public SC_Get_TruckInfoByCAR_CD(string _CAR_CD, string _DISTANCE, string _ARRIVE_DATE)
    {
        CAR_CD = _CAR_CD;
        DISTANCE = _DISTANCE;
        ARRIVE_DATE = _ARRIVE_DATE;
    }
}
