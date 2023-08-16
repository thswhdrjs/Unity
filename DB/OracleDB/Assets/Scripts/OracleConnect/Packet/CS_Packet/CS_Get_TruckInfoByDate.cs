using System;

[Serializable]
public class CS_Get_TruckInfoByDate : DefaultPacket
{
    public string date;

    public CS_Get_TruckInfoByDate()
    {
        date = string.Empty;
    }

    public CS_Get_TruckInfoByDate(string _date)
    {
        date = _date;
    }
}
