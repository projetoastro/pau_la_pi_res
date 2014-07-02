using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class Categorias
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public Secoes SecaoId { get; set; }
        public Paginas Pagina { get; set; }
        public int PaginaId { get; set; }
        public string SecaoIdValue { get; set; }
        public string Nome { get; set; }
        public string UrlAmigavel { get; set; }
        public string CssClass { get; set; }
        public string Thumb { get; set; }
        public int Orderby { get; set; }
        public bool Actived { get; set; }
        public List<SubCategorias> SubCategoria { get; set; }
        public bool HaveSubMenus { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string RemoveThumb { get; set; }
        public string ThumbValue { get; set; }

        #endregion
        #region :: Constructors ::
        public Categorias()
        {
            Clear();
        }

        public Categorias(int id)
        {
            Clear();
            Load(id);
        }

        private Categorias(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }
        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            SecaoId = new Secoes();
            Pagina = new Paginas();
            PaginaId = 0;
            SecaoIdValue = string.Empty;
            SubCategoria = new List<SubCategorias>();
            Nome = string.Empty;
            UrlAmigavel = string.Empty;
            CssClass = string.Empty;
            Thumb = string.Empty;
            Orderby = 0;
            Actived = true;
            HaveSubMenus = false;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            RemoveThumb = string.Empty;
            ThumbValue = string.Empty;
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

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("ID")) { Id = int.Parse(pRow["ID"].ToString()); }
            if (pRow.Table.Columns.Contains("SecaoId"))
            {
                SecaoId.Id = int.Parse(pRow["SecaoId"].ToString());
                SecaoIdValue = Convert.ToString(pRow["SecaoId"].ToString());
            }
            if (pRow.Table.Columns.Contains("Nome")) { Nome = Convert.ToString(pRow["Nome"].ToString()); }
            if (pRow.Table.Columns.Contains("UrlAmigavel")) { UrlAmigavel = Convert.ToString(pRow["UrlAmigavel"].ToString()); }
            if (pRow.Table.Columns.Contains("CssClass")) { CssClass = Convert.ToString(pRow["CssClass"].ToString()); }
            if (pRow.Table.Columns.Contains("Thumb"))
            {
                Thumb = Convert.ToString(pRow["Thumb"].ToString());
                if (!string.IsNullOrEmpty(Thumb))
                {
                    Thumb = CheckFileExist.ReturnFileUrl(CheckFileExist.MyPaths.tecnicas, Thumb);
                }
            }

            if (pRow.Table.Columns.Contains("PaginaId"))
            {
                if (!string.IsNullOrEmpty((pRow["PaginaId"].ToString())))
                {
                    PaginaId = int.Parse(pRow["PaginaId"].ToString());
                    Pagina = new Paginas(PaginaId);
                }
            }

            if (pRow.Table.Columns.Contains("Orderby")) { Orderby = int.Parse(pRow["Orderby"].ToString()); }
            if (pRow.Table.Columns.Contains("Actived")){Actived = bool.Parse(pRow["Actived"].ToString());}
            if (pRow.Table.Columns.Contains("haveSubMenus")) { HaveSubMenus = Convert.ToBoolean(int.Parse(pRow["haveSubMenus"].ToString())); }
            if (pRow.Table.Columns.Contains("Created")) { Created = DateTime.Parse(pRow["Created"].ToString()); }
            if (pRow.Table.Columns.Contains("Updated") && !string.IsNullOrEmpty(pRow["Updated"].ToString())) { Updated = DateTime.Parse(pRow["Updated"].ToString()); }
            if (pRow.Table.Columns.Contains("SecaoNome")) { SecaoId.Nome = Convert.ToString(pRow["SecaoNome"].ToString()); }
            if (HaveSubMenus)
            {
                SubCategoria = SubCategorias.List(Id);
            }
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
            sqlParametros.Add(new SqlParameter("@SecaoId", SecaoId.Id));
            sqlParametros.Add(new SqlParameter("@Nome", Nome));
            sqlParametros.Add(new SqlParameter("@UrlAmigavel", UrlAmigavel));
            sqlParametros.Add(new SqlParameter("@CssClass", CssClass));
            sqlParametros.Add(new SqlParameter("@Thumb", Thumb));
            sqlParametros.Add(new SqlParameter("@Orderby", Orderby));
            sqlParametros.Add(new SqlParameter("@Actived", Actived));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveCategoria", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        public bool ValidaOrdemCategoria(int pId, int pSecaoId, int pOrderby)
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", pId));
            sqlParametros.Add(new SqlParameter("@SecaoId", pSecaoId));
            sqlParametros.Add(new SqlParameter("@Orderby", pOrderby));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spValidaOrdemCategoria", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        #endregion
        #region :: Static Methods ::

        public static List<Categorias> List()
        {
            return GenericList("spListCategorias");
        }

        public static List<Categorias> List(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategorias", sqlParametros);
        }

        public static List<Categorias> ListMenuSite(int secaoId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@secaoId", secaoId)
            };

            return GenericList("spListCategoriasSiteMenu", sqlParametros);
        }

        public static List<Categorias> GenericList(string pQuery)
        {
            var list = new List<Categorias>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Categorias(item));
            }

            return list;
        }

        public static List<Categorias> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<Categorias>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Categorias(item));
            }

            return list;
        }

        #endregion
    }
}