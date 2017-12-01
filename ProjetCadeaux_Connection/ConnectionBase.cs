using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;


namespace ProjetCadeaux_Connection
{
    public class ConnectionBase
    {
       private DataSet ds = new DataSet();
       private DataTable dt = new DataTable();

    #if DEBUG
            //Connection des requêtes sql avec retours dans une DataTable
            private string connstring = String.Format("Server={0};Port={1};" +
                "User Id={2};Password={3};Database={4};ConnectionLifeTime={5};MaxPoolSize={6};",
                "localhost", "5432", "postgres",
                "root", "recette", "2", "1000");
    #else
       //Connection string pour la production 
       private string connstring = String.Format("Server={0};Port={1};" +
           "User Id={2};Password={3};Database={4};ConnectionLifeTime={5};MaxPoolSize={6};",
           "localhost", "5432", "postgres",
           "root", "production", "2", "1000");
    #endif

            public DataTable getConnection(string sql)
        {

            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();

            try
            {
                   
                    // data adapter making request from our connection
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    // i always reset DataSet before i do
                    // something with it.... i don't know why :-)
                    ds.Reset();
                    // filling DataSet with result from NpgsqlDataAdapter
                    da.Fill(ds);
                    // since it C# DataSet can handle multiple tables, we will select first
                    dt = ds.Tables[0];            

                    return dt;

            }
            catch (Exception)
            {
                    throw;
            }
            finally{
                conn.Close();
            }
        }

        //Connection pour les requêtes sans retours
        public void getVoidConnection(string sql)
        {

            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();

            try
            {

                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

