using System;

[Serializable]
public class SC_Get_YardTruckInfoByID : DefaultPacket
{
    public string EQU_NO;
    public string LATITUDE;
    public string LONGITUDE;
    public string ALTITUDE;
    public string GPS_TIME;
    public string FAULTCD;
    public string USER_ID;
    public string UPDATE_TIME;

    public SC_Get_YardTruckInfoByID()
    {
        EQU_NO = string.Empty;
        LATITUDE = string.Empty;
        LONGITUDE = string.Empty;
        ALTITUDE = string.Empty;
        GPS_TIME = string.Empty;
        FAULTCD = string.Empty;
        USER_ID = string.Empty;
        UPDATE_TIME = string.Empty;
    }

    public SC_Get_YardTruckInfoByID(string _EQU_NO, string _LATITUDE, string _LONGITUDE, string _ALTITUDE, string _GPS_TIME, 
        string _FAULTCD, string _USER_ID, string _UPDATE_TIME)
    {
        EQU_NO = _EQU_NO;
        LATITUDE = _LATITUDE;
        LONGITUDE = _LONGITUDE;
        ALTITUDE = _ALTITUDE;
        GPS_TIME = _GPS_TIME;
        FAULTCD = _FAULTCD;
        USER_ID = _USER_ID;
        UPDATE_TIME = _UPDATE_TIME;
    }

}
