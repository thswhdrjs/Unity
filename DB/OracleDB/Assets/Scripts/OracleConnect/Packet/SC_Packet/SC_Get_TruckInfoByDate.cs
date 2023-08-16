using System;
using System.Collections.Generic;

[Serializable]
public class SC_Get_TruckInfoByDate : DefaultPacket
{
    public List<DT3TruckInfo> dT3TruckInfos;

    public SC_Get_TruckInfoByDate()
    {
        dT3TruckInfos = new List<DT3TruckInfo>();
    }

    public void Add_Info(DT3TruckInfo _info)
    {
        if (dT3TruckInfos.Contains(_info) == false)
        {
            dT3TruckInfos.Add(_info);
        }
    }
}
