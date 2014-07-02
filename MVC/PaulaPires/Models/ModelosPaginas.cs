using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class ModelosPaginas
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Arquivo { get; set; }
        public string PartialView { get; set; }
        public string Controller { get; set; }

        #endregion
        #region :: Constructors ::

        public ModelosPaginas()
        {
            Clear();
        }

        public ModelosPaginas(int id)
        {
            Clear();
            Load(id);
        }

        private ModelosPaginas(DataRow pRow)
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
            Arquivo = string.Empty;
            Controller = string.Empty;
        }

        public void Load(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@ModeloId", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadModeloPagina", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("Id")) { Id = int.Parse(pRow["Id"].ToString()); }
            if (pRow.Table.Columns.Contains("Nome")) { Nome = Convert.ToString(pRow["Nome"].ToString()); }
            if (pRow.Table.Columns.Contains("Arquivo")) { Arquivo = Convert.ToString(pRow["Arquivo"].ToString()); }
            if (pRow.Table.Columns.Contains("PartialView")) { PartialView = Convert.ToString(pRow["PartialView"].ToString()); }
            if (pRow.Table.Columns.Contains("Controller")) { Controller = Convert.ToString(pRow["Controller"].ToString()); }
        }

        #endregion
        #region :: Static Methods ::

        public static List<ModelosPaginas> List()
        {
            return GenericList("spListModelosPaginas");
        }

        public static List<ModelosPaginas> GenericList(string pQuery)
        {
            var list = new List<ModelosPaginas>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModelosPaginas(item));
            }

            return list;
        }

        public static List<ModelosPaginas> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<ModelosPaginas>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModelosPaginas(item));
            }

            return list;
        }

        #endregion
    }
}