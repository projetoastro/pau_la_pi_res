using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class ModeloHome
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public Paginas Pagina { get; set; }
        public int PaginaId { get; set; }
        public string Conteudo { get; set; }
        public string Imagem { get; set; }
        public string DescImagem { get; set; }        
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }        

        #endregion
        #region :: Constructors ::
        public ModeloHome()
        {
            Clear();
        }

        public ModeloHome(int id)
        {
            Clear();
            Load(id);
        }

        private ModeloHome(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }
        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            Pagina = new Paginas();
            PaginaId = 0;
            Conteudo = string.Empty;
            Imagem = string.Empty;
            DescImagem = string.Empty;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public void Load(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadCategoria", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void LoadbyPagina(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", paginaId)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadConteudoHTMLPaginaSite", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void LoadbyModelo(int modeloId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@modeloId", modeloId)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadConteudoHTMLModeloSite", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("ID")) { Id = int.Parse(pRow["ID"].ToString()); }
            if (pRow.Table.Columns.Contains("PaginaId"))
            {
                PaginaId = int.Parse(pRow["PaginaId"].ToString());
                Pagina = new Paginas(PaginaId);
            }
            if (pRow.Table.Columns.Contains("Conteudo")) { Conteudo = Convert.ToString(pRow["Conteudo"].ToString()); }
            if (pRow.Table.Columns.Contains("DescImagem")) { DescImagem = Convert.ToString(pRow["DescImagem"].ToString()); }
            if (pRow.Table.Columns.Contains("Imagem"))
            {
                Imagem = Convert.ToString(pRow["Imagem"].ToString());
                if (!string.IsNullOrEmpty(Imagem))
                {
                    Imagem = CheckFileExist.ReturnFileUrl(CheckFileExist.MyPaths.paginasHTML, Imagem);
                }
            }
            if (pRow.Table.Columns.Contains("Created")) { Created = DateTime.Parse(pRow["Created"].ToString()); }
            if (pRow.Table.Columns.Contains("Updated") && !string.IsNullOrEmpty(pRow["Updated"].ToString())) { Updated = DateTime.Parse(pRow["Updated"].ToString()); }
        }

        public void DesabilitarHabilitar(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spDesabilitaHabilitaCategoria", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public bool Deletar()
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", Id)
            };

            SQLServer sql = new SQLServer();
            sql.ExecuteScalar("spDeletarCategoria", CommandType.StoredProcedure, sqlParametros.ToArray());

            return sql.HasError;
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveCategoria", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        #endregion
        #region :: Static Methods ::

        public static List<ModeloHome> List()
        {
            return GenericList("spListCategorias");
        }

        public static List<ModeloHome> List(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategorias", sqlParametros);
        }

        public static List<ModeloHome> ListMenuSite(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategoriasSiteMenu", sqlParametros);
        }

        public static List<ModeloHome> GenericList(string pQuery)
        {
            var list = new List<ModeloHome>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloHome(item));
            }

            return list;
        }

        public static List<ModeloHome> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<ModeloHome>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloHome(item));
            }

            return list;
        }

        #endregion
    }
}