using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class ModeloFAQ
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public string CategoriaNome { get; set; }
        public Paginas Pagina { get; set; }
        public List<SubCategorias> SubCategoria { get; set; }
        public List<ModeloFAQ> FAQS { get; set; }
        public int PaginaId { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public int Orderby { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }        

        #endregion
        #region :: Constructors ::
        public ModeloFAQ()
        {
            Clear();
        }

        public ModeloFAQ(int id)
        {
            Clear();
            Load(id);
        }

        private ModeloFAQ(DataRow pRow)
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
            FAQS = new List<ModeloFAQ>();
            Pagina = new Paginas();
            PaginaId = 0;
            Pergunta = string.Empty;
            Resposta = string.Empty;
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
            DataSet result = sql.Select("spLoadConteudoFAQByPaginaIdSite", CommandType.StoredProcedure, sqlParametros);

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
                    FAQS = List(int.Parse(pRow["PaginaId"].ToString()));
            }
            if (pRow.Table.Columns.Contains("Pergunta")) { Pergunta = Convert.ToString(pRow["Pergunta"].ToString()); }
            if (pRow.Table.Columns.Contains("Resposta")) { Resposta = Convert.ToString(pRow["Resposta"].ToString()); }
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
            sql.ExecuteScalar("spDeletarLinks", CommandType.StoredProcedure, sqlParametros.ToArray());

            return sql.HasError;
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));
            sqlParametros.Add(new SqlParameter("@PaginaId", PaginaId));
            sqlParametros.Add(new SqlParameter("@Pergunta", Pergunta));
            sqlParametros.Add(new SqlParameter("@Resposta", Resposta));
            sqlParametros.Add(new SqlParameter("@Orderby", Orderby));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveModeloFAQ", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        #endregion
        #region :: Static Methods ::

        public static List<ModeloFAQ> List()
        {
            return GenericList("spListCategorias");
        }

        public static List<ModeloFAQ> List(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", paginaId)
            };

            return GenericList("spListFAQByPaginaSite", sqlParametros);
        }

        public static List<ModeloFAQ> ListMenuSite(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategoriasSiteMenu", sqlParametros);
        }

        public static List<ModeloFAQ> ListbyPagina(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", paginaId)
            };

            return GenericList("spListFAQByPaginaSite", sqlParametros);
        }

        public static List<ModeloFAQ> GenericList(string pQuery)
        {
            var list = new List<ModeloFAQ>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloFAQ(item));
            }

            return list;
        }

        public static List<ModeloFAQ> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<ModeloFAQ>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new ModeloFAQ(item));
            }

            return list;
        }

        #endregion
    }
}