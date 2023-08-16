using System;

[Serializable]
public class SC_Get_Weather : DefaultPacket
{
    public string FCST_DT1;
    public string FCST_DT;
    public string FCST_TIME;
    public string FCST_DT_MAIN;
    public string F_SKY;
    public string F_SKY_NAME;
    public string F_TMP;
    public string F_POP;
    public string F_PTY;
    public string F_PTY_NAME;
    public string F_PCP;
    public string F_VEC;
    public string F_WSD;
    public string F_WAV;
    public string AREA_NM;

    public SC_Get_Weather()
    {
        FCST_DT1 = string.Empty;
        FCST_DT = string.Empty;
        FCST_TIME = string.Empty;
        FCST_DT_MAIN = string.Empty;
        F_SKY = string.Empty;
        F_SKY_NAME = string.Empty;
        F_TMP = string.Empty;
        F_POP = string.Empty;
        F_PTY = string.Empty;
        F_PTY_NAME = string.Empty;
        F_PCP = string.Empty;
        F_VEC = string.Empty;
        F_WSD = string.Empty;
        F_WAV = string.Empty;
        AREA_NM = string.Empty;
    }
}
