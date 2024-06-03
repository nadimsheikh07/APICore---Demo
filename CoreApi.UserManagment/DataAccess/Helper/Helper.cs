using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace CoreApi.UserManagment.DataAccess.Helper
{
    public class Helper
    {
     
        public static DataTable executeDataTableSqlQuery(string query, IConfiguration _configuration)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return table;
        }
      
        public static DataTable executeDataTableSqlFunction(string query, IConfiguration _configuration)
        {
            DataSet ds = new DataSet();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            var conn = new NpgsqlConnection(sqlDataSource);
            conn.Open();
            var tran = conn.BeginTransaction();
            var cmd = new NpgsqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(ds);

            tran.Commit();
            conn.Close();
            return ds.Tables[1];
        }
      
        public static void executeFunction(string query, IConfiguration _configuration)
        {
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            var conn = new NpgsqlConnection(sqlDataSource);
            NpgsqlCommand cmd = new NpgsqlCommand(query);
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        
        public static DataTable executeDataTableSqlFunctionMsg(string query, IConfiguration _configuration)
        {
            DataSet ds = new DataSet();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            var conn = new NpgsqlConnection(sqlDataSource);
            conn.Open();
            var tran = conn.BeginTransaction();
            var cmd = new NpgsqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(ds);

            tran.Commit();
            conn.Close();
            return ds.Tables[0];
        }

        public static DataTable executeDataTableSqlFunctionMsg_SHO(string query, IConfiguration _configuration)
        {
            DataSet ds = new DataSet();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            var conn = new SqlConnection(sqlDataSource);
            conn.Open();
            //var tran = conn.BeginTransaction();
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            //tran.Commit();
            conn.Close();
            return ds.Tables[0];
        }

        public static int executeDataTableSqlFunctionMsgSHO(string query, IConfiguration _configuration)
        {
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            var conn = new SqlConnection(sqlDataSource);
            var cmd = new SqlCommand(query,conn);
            conn.Open();
          int result=   cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            return result;
        }

    }
}
