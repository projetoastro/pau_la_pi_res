using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class ModeloImagens
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public string CategoriaNome { get; set; }
        public Paginas Pagina { get; set; }
        public List<SubCategorias> SubCategoria { get; set; }
        public List<ModeloImagens> Imagens { get; set; }
        public int PaginaId { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public int Orderby { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }        

        #endregion
        #region :: Constructors ::
        public ModeloImagens()
        {
            Clear();
        }

        public ModeloImagens(int id)
        {
            Clear();
            Load(id);
        }

        private ModeloImagens(DataRow pRow)
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
            Imagens = new List<ModeloImagens>();
            Pagina = new Paginas();
            PaginaId = 0;
            Imagem = string.Empty;
            Descricao = string.Empty;
            Orderby = 0;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public void Load(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@Id", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadConteudoFAQ", CommandType.StoredProcedure, sqlParametros);

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
            DataSet result = sql.Select("spLoadConteudoFAQPaginaSite", CommandType.StoredProcedure, sqlParametros);

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
            DataSet result = sql.Select("spLoadConteudoImagensByPaginaIdSite", CommandType.StoredProcedure, sqlParametros);

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

            if (pRow.Table.Columns.Contains("categoriaNome"))
            {
                CategoriaNome = Convert.ToString(pRow["categoriaNome"].ToString());
            }

            if (pRow.Table.Columns.Contains("PaginaId"))
            {
                PaginaId = int.Parse(pRow["PaginaId"].ToString());
                Pagina = new Paginas(PaginaId);
            }
            if (pRow.Table.Columns.Contains("categoriaId"))
            {
                if (!string.IsNullOrEmpty(pRow["categoriaId"].ToString()))
                    SubCategoria = SubCategorias.List(int.Parse(pRow["categoriaId"].ToString()));
            }
            if (pRow.Table.Columns.Contains("loadFaqs"))
            {
                if (!string.IsNullOrEmpty(pRow["loadFaqs"].ToString()))
                    Imagens = List(int.Parse(pRow["PaginaId"].ToString()));
            }
            if (pRow.Table.Columns.Contains("Imagem")) { Imagem = Convert.ToString(pRow["Imagem"].ToString()); }
            if (pRow.Table.Columns.Contains("Descricao")) { Descricao = Convert.ToString(pRow["Descricao"].ToString()); }
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
                new SqlParameter("@Id", Id)
            };

            SQLServer sql = new SQLServer();
            sql.ExecuteScalar("spDeletarImagem", CommandType.StoredProcedure, sqlParametros.ToArray());

            return sql.HasError;
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));
            sqlParametros.Add(new SqlParameter("@PaginaId", PaginaId));
            sqlParametros.Add(new SqlParameter("@Imagem", Imagem));
            sqlParametros.Add(new SqlParameter("@Descricao", Descricao));
            sqlParametros.Add(new SqlParameter("@Orderby", Orderby));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveModeloImagens", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        #endregion
        #region :: Static Methods ::

        public static List<ModeloImagens> List()
        {
            return GenericList("spListCategorias");
        }

        public static List<ModeloImagens> List(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", paginaId)
            };

            return GenericList("spListImagensByPaginaSite", sqlParametros);
        }

        public static List<ModeloImagens> ListMenuSite(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategoriasSiteMenu", sqlParametros);
        }

        public static List<ModeloImagens> ListbyPagina(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", paginaId)
            };

            return GenericList("spListImagensByPaginaSite", sqlParametros);
        }

        public static List<ModeloImagens> GenericList(string pQuery)
        {
            var list = new List<ModeloImagens>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloImagens(item));
            }

            return list;
        }

        public static List<ModeloImagens> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<ModeloImagens>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloImagens(item));
            }

            return list;
        }

        #endregion
    }
}