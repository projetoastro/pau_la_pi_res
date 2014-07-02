using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class CategoriaSubCategoriaPaginas
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public int CategoriaID { get; set; }
        public Categorias Categoria { get; set; }
        public SubCategorias SubCategoria { get; set; }
        public Paginas Pagina { get; set; }
        public int SubCategoriaID { get; set; }
        public int PaginaID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        #endregion
        #region :: Constructors ::
        public CategoriaSubCategoriaPaginas()
        {
            Clear();
        }

        public CategoriaSubCategoriaPaginas(int id)
        {
            Clear();
            Load(id);
        }

        private CategoriaSubCategoriaPaginas(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }
        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            Categoria = new Categorias();
            SubCategoria = new SubCategorias();
            Pagina = new Paginas();
            CategoriaID = 0;
            SubCategoriaID = 0;
            PaginaID = 0;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public void Load(int pPaginaID)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@PaginaId", pPaginaID)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spListCategoriaSubCategoriaPaginas", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("Id")) { Id = int.Parse(pRow["Id"].ToString()); }
            if (pRow.Table.Columns.Contains("CategoriaID"))
            {
                if (!string.IsNullOrEmpty(pRow["CategoriaID"].ToString()))
                {
                    CategoriaID = int.Parse(pRow["CategoriaID"].ToString());
                    Categoria.Id = CategoriaID;
                    Categoria.Nome = Convert.ToString(pRow["CategoriaNome"].ToString());
                }
            }
            if (pRow.Table.Columns.Contains("SubCategoriaID"))
            {
                if (!string.IsNullOrEmpty(pRow["SubCategoriaID"].ToString()))
                {
                    SubCategoriaID = int.Parse(pRow["SubCategoriaID"].ToString());
                    SubCategoria.Id = SubCategoriaID;
                    SubCategoria.Nome = Convert.ToString(pRow["SubCategoriaNome"].ToString());
                    SubCategoria = new SubCategorias(SubCategoriaID);
                }

            }
            if (pRow.Table.Columns.Contains("PaginaID")) { PaginaID = int.Parse(pRow["PaginaID"].ToString()); }
            if (pRow.Table.Columns.Contains("Created")) { Created = DateTime.Parse(pRow["Created"].ToString()); }
            if (pRow.Table.Columns.Contains("Updated"))
            {
                if (!string.IsNullOrEmpty(pRow["Updated"].ToString()))
                {
                    Updated = DateTime.Parse(pRow["Updated"].ToString());
                }
            }
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));
            sqlParametros.Add(new SqlParameter("@CategoriaID", CategoriaID));
            sqlParametros.Add(new SqlParameter("@SubCategoriaID", SubCategoriaID));
            sqlParametros.Add(new SqlParameter("@PaginaID", PaginaID));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSaveSubCategoria", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        public bool SaveByCategoriaSubCategoria()
        {
            var sqlParametros2 = new List<SqlParameter>();
            sqlParametros2.Add(new SqlParameter("@Id", 0));
            sqlParametros2.Add(new SqlParameter("@PaginaId", PaginaID));
            sqlParametros2.Add(new SqlParameter("@CategoriaId", CategoriaID));
            sqlParametros2.Add(new SqlParameter("@SubCategoriaId", SubCategoriaID));

            SQLServer sql = new SQLServer();
            sql.ExecuteScalar("spSaveCategoriaSubcategoriaPaginas", CommandType.StoredProcedure, sqlParametros2.ToArray());

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

        public static List<CategoriaSubCategoriaPaginas> List()
        {
            return GenericList("spListSubCategorias");
        }

        public static List<CategoriaSubCategoriaPaginas> ListByPagina(int paginaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@PaginaId", paginaId)
            };

            return GenericList("spListCategoriaSubCategoriaPaginas", sqlParametros);
        }

        public static List<CategoriaSubCategoriaPaginas> ListSubcategorias(int categoriaId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@CategoriaId", categoriaId)
            };

            return GenericList("spListSubCategorias", sqlParametros);
        }

        public static List<CategoriaSubCategoriaPaginas> ListRecursivo(int bid)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@BID", bid)
            };

            return GenericList("spListSubCategoriasRecursivoSiteMenu", sqlParametros);
        }

        public static List<CategoriaSubCategoriaPaginas> GenericList(string pQuery)
        {
            var list = new List<CategoriaSubCategoriaPaginas>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new CategoriaSubCategoriaPaginas(item));
            }

            return list;
        }

        public static List<CategoriaSubCategoriaPaginas> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<CategoriaSubCategoriaPaginas>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new CategoriaSubCategoriaPaginas(item));
            }

            return list;
        }

        #endregion
    }
}