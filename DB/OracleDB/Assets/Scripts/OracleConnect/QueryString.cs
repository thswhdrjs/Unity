using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnect
{
    public class QueryString
    {
        // [Kesti DB 사용]

        // 1DT 메인화면 날씨 정보 pm10 : 미세먼지, pm25 : 초미세먼지  
        // dbManager_kesti.GetDataFromDB(sql_main_weather, out result);
        // 10분에 한번씩 서버에서 갱신
        public string sql_main_weather = "SELECT pm10,pm25 FROM (SELECT * FROM airq_fcst_d ORDER BY fcst_date DESC) WHERE ROWNUM = 1 AND index_id = '5539'";
        public string sql_main_weather22 = "SELECT * FROM (SELECT * FROM airq_fcst_d ORDER BY fcst_date DESC) WHERE ROWNUM = 1 AND index_id = '5539'";

        // 3DT 4개지역 기상정보 왼쪽부터 spot1, spot2, spot3, spot4.   
        // spot1 은 DT1 의 메인 기상정보와 동일함
        // 10분에 한번씩 서버에서 갱신
        public string weather_spot1 = "SELECT pm10,pm25 FROM (SELECT * FROM airq_fcst_d ORDER BY fcst_date DESC) WHERE ROWNUM = 1 AND index_id = '5539'";
        public string weather_spot2 = "SELECT pm10,pm25 FROM (SELECT * FROM airq_fcst_d ORDER BY fcst_date DESC) WHERE ROWNUM = 1 AND index_id = '5546'";
        public string weather_spot3 = "SELECT pm10,pm25 FROM (SELECT * FROM airq_fcst_d ORDER BY fcst_date DESC) WHERE ROWNUM = 1 AND index_id = '5551'";
        public string weather_spot4 = "SELECT pm10,pm25 FROM (SELECT * FROM airq_fcst_d ORDER BY fcst_date DESC) WHERE ROWNUM = 1 AND index_id = '5557'";
        //날씨 정보 10분에 한번씩 정보를 가져오고 클라에는 뿌려주기, 정보가 많아서 데이터 가져오는데 시간이 걸릴 수 있음 현재 쿼리는 미세먼지와 초미세먼지


        // [TSB DB 사용]
        // 2DT 컨테이너 위치 입력 (세개 드랍박스) 로 컨테이너 정보 가져오기
        // // 사용법 dbManager_tsb.GetDataFromDB(sql_containerInfo, out result);
        public string containerInfoFromLoc = "SELECT CNTR_NO, VSL_CD, VOYAGE, FE, WEIGHT, CARGO_TYPE, IN_DATE, OUT_DATE FROM TOS_CNTR WHERE tos_cntr.bay_idx = '3' AND tos_cntr.row_idx = '4' AND tos_cntr.tier_idx = '3' AND ROWNUM = 1";

        //tsb
        // 컨테이너 아이디로 컨테이너 정보 가져오기
        public string containerInfoFromID = "SELECT CNTR_NO, VSL_CD, VOYAGE, FE, WEIGHT, CARGO_TYPE, IN_DATE, OUT_DATE FROM TOS_CNTR WHERE CNTR_NO = '{0}' AND ROWNUM = 1";

        //tsb
        // 야드 트럭 이이디로 정보 가져오기
        public string yardTruckInfoFromID = "SELECT * FROM TOS_EQU_STATE WHERE EQU_NO = '{0}'";

        //gmt
        public string weatherInfo = "SELECT * FROM(SELECT fcst_dt as fcst_dt1, to_char(fcst_dt, 'dd(DY)','NLS_DATE_LANGUAGE=korean') AS fcst_dt" +
            "           , to_char(fcst_dt, 'hh:mi(AM)', 'NLS_DATE_LANGUAGE=korean') AS fcst_time" +
            "            , to_char(fcst_dt, 'YYYY-MM-DD HH24:MI', 'NLS_DATE_LANGUAGE=korean') AS fcst_dt_main" +
            "               , MAX(f_sky) AS f_sky /*날씨*/" +
            "                , decode(MAX(f_sky)," +
            "                    '1'," +
            "                    '맑음'," +
            "                    '3'," +
            "                    '구름많음'," +
            "                    '4'," +
            "                    '흐림'," +
            "                    '-') AS f_sky_name" +
            "                , MAX(f_tmp) || ' ℃' AS f_tmp /*온도*/" +
            "                 , MAX(f_pop) AS f_pop /*강수확율*/" +
            "                  , MAX(f_pty) AS f_pty /*강수형태*/" +
            "                   , decode(MAX(f_pty)," +
            "                       '0'," +
            "                       '없음'," +
            "                       '1'," +
            "                       '비'," +
            "                       '2'," +
            "                       '비/눈'," +
            "                       '3'," +
            "                       '눈'," +
            "                       '4'," +
            "                       '소나기'," +
            "                       '-') AS f_pty_name" +
            "                   , MAX(f_pcp) AS f_pcp /*강수량*/" +
            "                    , MAX(f_vec) AS f_vec /*풍향*/" +
            "                     , MAX(f_wsd) AS f_wsd /*풍속*/" +
            "                      , nvl(MAX(f_wav), '-') AS f_wav /*파고*/" +
            "                       , MAX(area_nm) AS area_nm" +
            "        FROM(SELECT base_dt" +
            "             , fcst_dt" +
            "             , category" +
            "             , decode(category, 'WSD', fcst_value) AS f_wsd" +
            "              , decode(category, 'SKY', fcst_value) AS f_sky" +
            "               , decode(category, 'PTY', fcst_value) AS f_pty" +
            "                , decode(category, 'POP', fcst_value) AS f_pop" +
            "                 , decode(category, 'PCP', fcst_value) AS f_pcp" +
            "                  , decode(category, 'REH', fcst_value) AS f_reh" +
            "                   , decode(category, 'SNO', fcst_value) AS f_sno" +
            "                    , decode(category, 'TMP', fcst_value) AS f_tmp" +
            "                     , decode(category, 'UUU', fcst_value) AS f_uuu" +
            "                      , decode(category, 'VVV', fcst_value) AS f_vvv" +
            "                       , decode(category, 'VEC', fcst_value) AS f_vec" +
            "                        , decode(category, 'WAV', fcst_value) AS f_wav" +
            "                         , dcc.cd_nm AS area_nm" +
            "                       FROM dt_vilage_fcst_info vfi" +
            "                          , dt_cmmn_cd          dcc" +
            "                        WHERE /*fcst_dt BETWEEN SYSDATE AND SYSDATE + (1 / 24)" +
            "            AND */vfi.area_cd = dcc.cd_id" +
            "            AND dcc.cd_ty_id = 'CT001'" +
            "            AND vfi.area_cd = '2644058000'" +
            "            AND fcst_dt > to_date('20211129','yyyymmdd')" +
            "           )" +
            "        GROUP BY fcst_dt" +
            "       )" +
            "    order by fcst_dt1 asc";

        //klnet
        public string dt3TruckInfoByDate = "select * from DT_TML_ARR_H where TRACE_DATE like '{0}%'";
        public string dt3TruckInfoByCAR_CD = "select * from DT_TML_ARR_H where CAR_CD = '{0}'";
        public string dt3TruckDetailInfoByCAR_CD = "select * from DT_VEH_DISP where CAR_CD = '{0}'";
    }
}
