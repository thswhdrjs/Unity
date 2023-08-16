using System;

[Serializable]
public class CS_GetContainerInfoByID : DefaultPacket
{
    public string containerID;

    public CS_GetContainerInfoByID()
    {
        containerID = string.Empty;
    }

    public CS_GetContainerInfoByID(string _containerID)
    {
        containerID = _containerID;
    }
}
