using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class GaleriaMidiasImagens
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public ModeloMidiasImagens ModeloMidiaImagem { get; set; }
        public int ModeloMidiaImagensId { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public int Orderby { get; set; }

        #endregion
        #region :: Constructors ::

        public GaleriaMidiasImagens()
        {
            Clear();
        }

        public GaleriaMidiasImagens(int id)
        {
            Clear();
            Load(id);
        }

        private GaleriaMidiasImagens(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }

        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            ModeloMidiaImagem = new ModeloMidiasImagens();
            ModeloMidiaImagensId = 0;
            Imagem = string.Empty;
            Descricao = string.Empty;
            Orderby = 0;            
        }

        public void Load(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@Id", id)
            };

            var sql = new SQLServer();
            DataSet result = sql.Select("spLoadGaleriaMidiaImagens", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("ID"))
            {
                Id = int.Parse(pRow["ID"].ToString());
            }            
            if (pRow.Table.Columns.Contains("Imagem"))
            {
                Imagem = Convert.ToString(pRow["Imagem"].ToString());
            }
            if (pRow.Table.Columns.Contains("Descricao"))
            {
                Descricao = Convert.ToString(pRow["Descricao"].ToString());
            }            
        }

        public bool Deletar()
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@Id", Id)
            };

            var sql = new SQLServer();
            sql.ExecuteScalar("spDeletarGaleriaMidiaImagens", CommandType.StoredProcedure, sqlParametros.ToArray());

            return sql.HasError;
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));
            sqlParametros.Add(new SqlParameter("@Imagem", Imagem));
            sqlParametros.Add(new SqlParameter("@Descricao", Descricao));
            sqlParametros.Add(new SqlParameter("@Orderby", Orderby));

            var sql = new SQLServer();
            Id =
                Convert.ToInt32(sql.ExecuteScalar("spSaveModeloImagens", CommandType.StoredProcedure,
                    sqlParametros.ToArray()));

            return (Id > 0);
        }

        #endregion
        #region :: Static Methods ::

        public static List<GaleriaMidiasImagens> List()
        {
            return GenericList("spListGaleriaImagensByMidiaImagens");
        }

        public static List<GaleriaMidiasImagens> ListbyMidiaImagens(int midiaImagensId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@midiaImagensId", midiaImagensId)
            };

            return GenericList("spListGaleriaImagensByMidiaImagens", sqlParametros);
        }

        private static List<GaleriaMidiasImagens> GenericList(string pQuery)
        {
            var list = new List<GaleriaMidiasImagens>();

            var sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new GaleriaMidiasImagens(item));
            }

            return list;
        }

        private static List<GaleriaMidiasImagens> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<GaleriaMidiasImagens>();

            var sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new GaleriaMidiasImagens(item));
            }

            return list;
        }

        #endregion
    }
}