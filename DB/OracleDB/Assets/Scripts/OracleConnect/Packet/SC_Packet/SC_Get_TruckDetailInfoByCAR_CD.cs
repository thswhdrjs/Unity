using System;

[Serializable]
public class SC_Get_TruckDetailInfoByCAR_CD : DefaultPacket
{
    public string CAR_CD;
    public string VSL;
    public string VOY;
    public string CNTR_NO;
    public string TYPE_SIZE;

    public SC_Get_TruckDetailInfoByCAR_CD()
    {
        CAR_CD = string.Empty;
        VSL = string.Empty;
        VOY = string.Empty;
        CNTR_NO = string.Empty;
        TYPE_SIZE = string.Empty;
    }

    public SC_Get_TruckDetailInfoByCAR_CD(string _CAR_CD, string _VSL, string _VOY, string _CNTR_NO, string _TYPE_SIZE)
    {
        CAR_CD = _CAR_CD;
        VSL = _VSL;
        VOY = _VOY;
        CNTR_NO = _CNTR_NO;
        TYPE_SIZE = _TYPE_SIZE;
    }
}
