using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class SubCategorias
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public int Bid { get; set; }
        public Categorias CategoriaId { get; set; }
        public Paginas Pagina { get; set; }
        public int PaginaId { get; set; }
        public string CategoriaIdValue { get; set; }
        public string Nome { get; set; }
        public string UrlAmigavel { get; set; }
        public bool MostraSubMenu { get; set; }
        public bool MostraMenuTratamento { get; set; }
        public string StatusMostraSubMenu { get; set; }
        public string StatusMostraMenuTratamento { get; set; }
        public int Orderby { get; set; }
        public bool Actived { get; set; }
        public bool HaveSubMenus { get; set; }
        public DateTime Created { get; set; }
        public List<SubCategorias> SubsCategorias { get; set; }

        #endregion
        #region :: Constructors ::
        public SubCategorias()
        {
            Clear();
        }

        public SubCategorias(int id)
        {
            Clear();
            Load(id);
        }

        public SubCategorias(string pUsuario, string pSenha)
        {
            Clear();
            Load(pUsuario, pSenha);
        }

        private SubCategorias(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }
        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            Bid = 0;
            CategoriaId = new Categorias();
            CategoriaIdValue = string.Empty;
            Pagina = new Paginas();
            PaginaId = 0;
            Nome = string.Empty;
            UrlAmigavel = string.Empty;
            MostraSubMenu = true;
            MostraMenuTratamento = false;
            StatusMostraSubMenu = string.Empty;
            StatusMostraMenuTratamento = string.Empty;
            Orderby = 0;
            Actived = true;
            HaveSubMenus = false;
            Created = DateTime.Now;
        }

        public void Load(int pId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@SubCategoriaId", pId)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadSubCategoria", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void Load(string pUsuario, string pSenha)
        {
            SqlParameter[] sqlParametros =
            {
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
            if (pRow.Table.Columns.Contains("Bid")) { Bid = int.Parse(pRow["Bid"].ToString()); }
            if (pRow.Table.Columns.Contains("CategoriaId"))
            {
                //CategoriaId.Id = int.Parse(pRow["CategoriaId"].ToString());
                CategoriaId = new Categorias(int.Parse(pRow["CategoriaId"].ToString()));
                CategoriaIdValue = Convert.ToString(pRow["CategoriaId"].ToString());
            }
            if (pRow.Table.Columns.Contains("PaginaId"))
            {
                if (!string.IsNullOrEmpty((pRow["PaginaId"].ToString())))
                {
                    PaginaId = int.Parse(pRow["PaginaId"].ToString());
                    Pagina = new Paginas(PaginaId);
                }
            }
            if (pRow.Table.Columns.Contains("CategoriasNome")) { CategoriaId.Nome = Convert.ToString(pRow["CategoriasNome"].ToString()); }
            if (pRow.Table.Columns.Contains("Nome")) { Nome = Convert.ToString(pRow["Nome"].ToString()); }
            if (pRow.Table.Columns.Contains("UrlAmigavel")) { UrlAmigavel = Convert.ToString(pRow["UrlAmigavel"].ToString()); }
            if (pRow.Table.Columns.Contains("MostraSubMenu")) { MostraSubMenu = bool.Parse(pRow["MostraSubMenu"].ToString());}
            if (pRow.Table.Columns.Contains("MostraMenuTratamentos") &&
                !string.IsNullOrEmpty(pRow["MostraMenuTratamentos"].ToString()))
            {
                MostraMenuTratamento = bool.Parse(pRow["MostraMenuTratamentos"].ToString());
            }
            if (pRow.Table.Columns.Contains("Orderby")) { Orderby = int.Parse(pRow["Orderby"].ToString()); }
            if (pRow.Table.Columns.Contains("haveSubMenus")) { HaveSubMenus = Convert.ToBoolean(int.Parse(pRow["haveSubMenus"].ToString())); }
            if (pRow.Table.Columns.Contains("Actived")){Actived = bool.Parse(pRow["Actived"].ToString());}
            if (pRow.Table.Columns.Contains("Created")) { Created = DateTime.Parse(pRow["Created"].ToString()); }
            if (HaveSubMenus)
            {
                SubsCategorias = SubCategorias.ListRecursivo(Id);
            }
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));
            sqlParametros.Add(new SqlParameter("@BId", Bid));
            sqlParametros.Add(new SqlParameter("@CategoriaId", CategoriaId.Id));
            sqlParametros.Add(new SqlParameter("@Nome", Nome));
            sqlParametros.Add(new SqlParameter("@UrlAmigavel", UrlAmigavel));
            sqlParametros.Add(new SqlParameter("@MostraSubMenu", MostraSubMenu));
            sqlParametros.Add(new SqlParameter("@MostraMenuTratamentos", MostraMenuTratamento));
            sqlParametros.Add(new SqlParameter("@Orderby", Orderby));
            sqlParametros.Add(new SqlParameter("@Actived", Actived));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveSubCategoria", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        public void DesabilitarHabilitar(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@SubCategoriaId", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spDesabilitaHabilitaSubCategoria", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public bool Deletar()
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@SubCategoriaId", Id)
            };

            SQLServer sql = new SQLServer();
            sql.ExecuteScalar("spDeletarSubCategoria", CommandType.StoredProcedure, sqlParametros.ToArray());

            return sql.HasError;
        }

        #endregion
        #region :: Static Methods ::

        public static List<SubCategorias> List()
        {
            return GenericList("spListSubCategorias");
        }

        public static List<SubCategorias> List(int categoriaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", categoriaId)
            };

            return GenericList("spListSubCategoriasSiteMenu", sqlParametros);
        }

        public static List<SubCategorias> ListMenuInternoPagina(int categoriaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", categoriaId)
            };

            return GenericList("spListSubCategoriasSiteMenuInternoPagina", sqlParametros);
        }

        public static List<SubCategorias> ListMenuTratamento(int categoriaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", categoriaId)
            };

            return GenericList("spListSubMenuTratamentosSite", sqlParametros);
        }

        public static List<SubCategorias> ListSubcategorias(int categoriaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", categoriaId)
            };

            return GenericList("spListSubCategorias", sqlParametros);
        }

        public static List<SubCategorias> ListRecursivo(int bid)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@BID", bid)
            };

            return GenericList("spListSubCategoriasRecursivoSiteMenu", sqlParametros);
        }

        public static List<SubCategorias> GenericList(string pQuery)
        {
            var list = new List<SubCategorias>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new SubCategorias(item));
            }

            return list;
        }

        public static List<SubCategorias> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<SubCategorias>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new SubCategorias(item));
            }

            return list;
        }

        #endregion
    }
}
