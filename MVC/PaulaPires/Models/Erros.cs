using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class Erros
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public Exception Erro { get; set; }

        #endregion
        #region :: Constructors ::

        public Erros()
        {
            Clear();
        }

        public Erros(int pId)
        {
            Clear();
            Load(pId);
        }

        public Erros(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }

        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
        }

        public void Load(int pId)
        {
            SqlParameter[] sqlParametros = new SqlParameter[]{
                new SqlParameter("@IdError", pId)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("CRM.spr_LoadContactById", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("IdError"))
            {
                Id = int.Parse(pRow["IdError"].ToString());
            }
        }

        #endregion
        #region :: Static Methods ::

        public static int Save(Exception pError)
        {
            var sqlParametros = new List<SqlParameter>();

            sqlParametros.Add(new SqlParameter("@erro", pError.Message));
            sqlParametros.Add(new SqlParameter("@pagina", pError.StackTrace));
            sqlParametros.Add(new SqlParameter("@metodo", pError.TargetSite.ToString()));

            SQLServer sql = new SQLServer();
            int retorno = Convert.ToInt32(sql.ExecuteScalar("spSaveError", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return retorno;
        }

        public static List<Erros> List()
        {
            return GenericList("CRM.spr_ListContactByIdCustomer");
        }

        public static List<Erros> GenericList(string pQuery)
        {
            var list = new List<Erros>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Erros(item));
            }

            return list;
        }

        public static List<Erros> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<Erros>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result.Tables[0]))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Erros(item));
            }

            return list;
        }
        #endregion
    }
}