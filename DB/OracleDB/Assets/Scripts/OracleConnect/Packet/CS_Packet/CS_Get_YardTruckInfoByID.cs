using System;

[Serializable]
public class CS_Get_YardTruckInfoByID : DefaultPacket
{
    public string yardTruckID;

    public CS_Get_YardTruckInfoByID()
    {
        yardTruckID = string.Empty;
    }

    public CS_Get_YardTruckInfoByID(string _yardTruckID)
    {
        yardTruckID = _yardTruckID;
    }
}
