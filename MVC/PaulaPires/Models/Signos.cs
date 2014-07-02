using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class Signos
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CssClass { get; set; }
        public int Orderby { get; set; }        

        #endregion
        #region :: Constructors ::

        public Signos()
        {
            Clear();
        }

        public Signos(string pUsuario, string pSenha)
        {
            Clear();
            Load(pUsuario, pSenha);
        }

        private Signos(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }

        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            Nome = string.Empty;
            CssClass = string.Empty;
            Orderby = 0;
        }

        public void Load(string pUsuario, string pSenha)
        {
            SqlParameter[] sqlParametros = new SqlParameter[]{
                new SqlParameter("@username", pUsuario),
                new SqlParameter("@password", pSenha)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spValidaLogin", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("Id")) { Id = int.Parse(pRow["Id"].ToString()); }
            if (pRow.Table.Columns.Contains("Nome")) { Nome = Convert.ToString(pRow["Nome"].ToString()); }
            if (pRow.Table.Columns.Contains("CssClass")) { CssClass = Convert.ToString(pRow["CssClass"].ToString()); }
            if (pRow.Table.Columns.Contains("Orderby")) { Orderby = int.Parse(pRow["Orderby"].ToString()); }
        }

        #endregion
        #region :: Static Methods ::

        public static List<Signos> List()
        {
            return GenericList("spListSignos");
        }

        public static List<Signos> GenericList(string pQuery)
        {
            var list = new List<Signos>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Signos(item));
            }

            return list;
        }

        public static List<Signos> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<Signos>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Signos(item));
            }

            return list;
        }

        #endregion
    }
}