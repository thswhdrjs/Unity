using System;

[Serializable]
public class CS_Get_TruckDetailInfoByCAR_CD : DefaultPacket
{
    public string CAR_CD;

    public CS_Get_TruckDetailInfoByCAR_CD()
    {
        CAR_CD = string.Empty;
    }

    public CS_Get_TruckDetailInfoByCAR_CD(string _CAR_CD)
    {
        CAR_CD = _CAR_CD;
    }
}
