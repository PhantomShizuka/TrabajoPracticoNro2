using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace TrabajoPracticoNro2
{
    public static class ConexionSQL
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=Base;Integrated Security=True";

        public static DataTable FillData(string comandoSql, string database)
        {
            var dataTable = new DataTable();

            try
            {
                new SqlDataAdapter(comandoSql, connectionString.Replace("Base", database)).Fill(dataTable);
            }
            catch (Exception)
            {
                return null;
            }

            return dataTable;
        }

        public static List<object> ExecuteProcedure(string procedure, string database, List<object> objectValues)
        {
            List<object> values_output = new List<object>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString.Replace("Base", database)))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = procedure,
                        Connection = sqlConnection
                    };

                    List<SqlParameter> parameters = GetListParameters(procedure, database, objectValues);

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        sqlCommand.Parameters.Add(parameters[i]);
                    }

                    sqlCommand.ExecuteNonQuery();                   

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (parameters[i].Direction != ParameterDirection.Input)
                        {
                            values_output.Add(parameters[i].Value);
                        }
                        else
                        {
                            values_output.Add(null);
                        }
                    }
                }

                return values_output;
            }
            catch (Exception)
            {
                throw;
            }  
        }

        private static List<SqlParameter> GetListParameters(string procedureSql, string database, List<object> objectValues)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            procedureSql =
                "SELECT P.name, TYPE_NAME(P.user_type_id), P.max_length ,P.is_output, P.is_nullable " +
                "FROM sys.objects AS S INNER JOIN sys.parameters AS P ON S.OBJECT_ID = P.OBJECT_ID " +
                "WHERE S.name = '" + procedureSql + "' ORDER BY P.parameter_id";

            DataTable dataTable = FillData(procedureSql, connectionString.Replace("Base", database));

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = row[0].ToString(),
                    SqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), row[1].ToString(), true),
                    Size = int.Parse(row[2].ToString()),
                    Direction = bool.Parse(row[3].ToString()) ? ParameterDirection.Output : ParameterDirection.Input,
                    IsNullable = bool.Parse(row[4].ToString()),
                    Value = objectValues[i].ToString() == "" ? null : objectValues[i].ToString()
                };

                parameters.Add(sqlParameter);
            }

            return parameters;     
        }
    }
}
