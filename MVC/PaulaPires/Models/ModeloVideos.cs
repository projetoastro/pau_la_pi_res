using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class ModeloVideos
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public string CategoriaNome { get; set; }
        public Paginas Pagina { get; set; }
        public List<SubCategorias> SubCategoria { get; set; }
        public List<SubCategorias> SubMenuTratamento { get; set; }
        public int PaginaId { get; set; }
        public string Conteudo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }        

        #endregion
        #region :: Constructors ::
        public ModeloVideos()
        {
            Clear();
        }

        public ModeloVideos(int id)
        {
            Clear();
            Load(id);
        }

        private ModeloVideos(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }
        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            CategoriaNome = string.Empty;
            SubCategoria = new List<SubCategorias>();
            SubMenuTratamento = new List<SubCategorias>();
            Pagina = new Paginas();
            PaginaId = 0;
            Conteudo = string.Empty;
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
            DataSet result = sql.Select("spLoadConteudoVideosPaginaSite", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void LoadbyPagina(string paginaUrlAmigavel)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@pagina", paginaUrlAmigavel)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadConteudoHtmlSite", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void LoadbyPaginaId(int paginaId, int categoriaId, int subcategoriaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@pagina", paginaId),
                new SqlParameter("@categoria", categoriaId),
                new SqlParameter("@subcategoria", subcategoriaId)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadConteudoVideosByPaginaIdSite", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void LoadbyPaginaAdmin(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", paginaId)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadConteudoHTMLPagina", CommandType.StoredProcedure, sqlParametros);

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
            if (pRow.Table.Columns.Contains("categoriaNome"))
            {
                CategoriaNome = Convert.ToString(pRow["categoriaNome"].ToString());
            }
            if (pRow.Table.Columns.Contains("categoriaId"))
            {
                if (!string.IsNullOrEmpty(pRow["categoriaId"].ToString()))
                    SubCategoria = SubCategorias.ListMenuInternoPagina(int.Parse(pRow["categoriaId"].ToString()));
            }
            if (pRow.Table.Columns.Contains("categoriaId"))
            {
                if (!string.IsNullOrEmpty(pRow["categoriaId"].ToString()))
                    SubMenuTratamento = SubCategorias.ListMenuTratamento(int.Parse(pRow["categoriaId"].ToString()));
            }
            if (pRow.Table.Columns.Contains("Conteudo")) { Conteudo = Convert.ToString(pRow["Conteudo"].ToString()); }
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
            sqlParametros.Add(new SqlParameter("@PaginaId", PaginaId));
            sqlParametros.Add(new SqlParameter("@Conteudo", Conteudo));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveModeloVideo", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        #endregion
        #region :: Static Methods ::

        public static List<ModeloVideos> List()
        {
            return GenericList("spListCategorias");
        }

        public static List<ModeloVideos> List(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategorias", sqlParametros);
        }

        public static List<ModeloVideos> ListMenuSite(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategoriasSiteMenu", sqlParametros);
        }

        public static List<ModeloVideos> GenericList(string pQuery)
        {
            var list = new List<ModeloVideos>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloVideos(item));
            }

            return list;
        }

        public static List<ModeloVideos> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<ModeloVideos>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloVideos(item));
            }

            return list;
        }

        #endregion
    }
}