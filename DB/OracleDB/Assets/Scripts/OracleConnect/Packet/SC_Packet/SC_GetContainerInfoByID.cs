using System;

[Serializable]
public class SC_GetContainerInfoByID : DefaultPacket
{
    public string CNTR_NO;
    public string VSL_CD;
    public string VOYAGE;
    public string FE;
    public string WEIGHT;
    public string CARGO_TYPE;
    public string IN_DATE;
    public string OUT_DATE;

    public SC_GetContainerInfoByID()
    {
        CNTR_NO = string.Empty;
        VSL_CD = string.Empty;
        VOYAGE = string.Empty;
        FE = string.Empty;
        WEIGHT = string.Empty;
        CARGO_TYPE = string.Empty;
        IN_DATE = string.Empty;
        OUT_DATE = string.Empty;
    }

    public SC_GetContainerInfoByID(string _CNTR_NO, string _VSL_CD, string _VOYAGE, string _FE, string _WEIGHT, string _CARGO_TYPE, string _IN_DATE, string _OUT_DATE)
    {
        CNTR_NO = _CNTR_NO;
        VSL_CD = _VSL_CD;
        VOYAGE = _VOYAGE;
        FE = _FE;
        WEIGHT = _WEIGHT;
        CARGO_TYPE = _CARGO_TYPE;
        IN_DATE = _IN_DATE;
        OUT_DATE = _OUT_DATE;
    }

}
