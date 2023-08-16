using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private string server = "127.0.0.1";
    private string user = "root";
    private string database = "test";
    private string pw = "qwer1234";
    private string connStr;

    private MySqlConnection conn;

    // Start is called before the first frame update
    void Start()
    {
        connStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", server, user, database, pw);
        MySqlConnection conn = new MySqlConnection(connStr);
    }

    // 접속테스트
    private bool ConnectionTest()
    {
        try
        {
            conn.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    //데이터조회
    public void SelectDB()
    {
        string sql = "select * from user";

        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader dr = cmd.ExecuteReader();
        dr.Close();
    }

    //INSERT처리
    public void InsertDB()
    {
        string sql = "Insert Into user  (id,name) values ('1','홍길동')";

        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    //UPDATE처리
    public void UpdateDB()
    {
        string sql = "Update user Set name ='홍길동2' where id = 1";

        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    //DELETE처리
    public void DeleteDB()
    {
        string sql = "Delete From user where id = '1'";

        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    //데이터조회
    public DataSet GetUser()
    {
        string sql = "select * from user";
        DataSet ds = new DataSet();

        conn.Open();
        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
        da.Fill(ds);
        return ds;
    }
}
