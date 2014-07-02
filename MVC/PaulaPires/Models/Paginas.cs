using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using DBFactory;
using RotasMVC;

namespace PaulaPires.Models
{
    public class Paginas
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public ModelosPaginas Modelos { get; set; }
        public int SubCategoriaId { get; set; }
        public int CategoriaId { get; set; }
        public int ModeloId { get; set; }
        public string UrlAmigavel { get; set; }
        public string TituloMenu { get; set; }
        public string TituloPagina { get; set; }
        public string ImagemCompartilha { get; set; }
        public string Descricao { get; set; }
        public string PalavrasChaves { get; set; }
        public bool AtivaDuvidas { get; set; }
        public bool Actived { get; set; }
        public int Orderby { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string RemoveThumb { get; set; }
        public string SubCategoriasPagina { get; set; }
        public string CategoriasPagina { get; set; }

        #endregion
        #region :: Constructors ::

        public Paginas()
        {
            Clear();
        }

        public Paginas(int id)
        {
            Clear();
            Load(id);
        }

        private Paginas(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }

        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            Modelos = new ModelosPaginas();
            SubCategoriaId = 0;
            CategoriaId = 0;
            ModeloId = 0;
            UrlAmigavel = string.Empty;
            TituloMenu = string.Empty;
            TituloPagina = string.Empty;
            ImagemCompartilha = string.Empty;
            Descricao = string.Empty;
            PalavrasChaves = string.Empty;
            AtivaDuvidas = false;
            Actived = true;
            Orderby = 0;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            RemoveThumb = string.Empty;
        }

        public void Load(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@PaginaId", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadPagina", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void LoadbyUrlAmigavel(string paginaUrlAmigavel, string categoriaUrlAmigavel, string subcategoriaUrlAmigavel)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@pagina", paginaUrlAmigavel),
                new SqlParameter("@Categoria", categoriaUrlAmigavel),
                new SqlParameter("@SubCategoria", subcategoriaUrlAmigavel)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spLoadPaginaByUrlSite", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("Id")) { Id = int.Parse(pRow["Id"].ToString()); }
            if (pRow.Table.Columns.Contains("ModeloID"))
            {
                ModeloId = int.Parse(pRow["ModeloId"].ToString());
                Modelos = new ModelosPaginas(ModeloId);
            }
            if (pRow.Table.Columns.Contains("SubCategoriaId"))
            {
                if (!string.IsNullOrEmpty((pRow["SubCategoriaId"].ToString())))
                {
                    SubCategoriaId = int.Parse(pRow["SubCategoriaId"].ToString());
                }
            }
            if (pRow.Table.Columns.Contains("SubCategoriaId1"))
            {
                if (!string.IsNullOrEmpty((pRow["SubCategoriaId1"].ToString())))
                {
                    SubCategoriaId = int.Parse(pRow["SubCategoriaId1"].ToString());
                }
            }
            if (pRow.Table.Columns.Contains("CategoriaId"))
            {
                if (!string.IsNullOrEmpty((pRow["CategoriaId"].ToString())))
                {
                    CategoriaId = int.Parse(pRow["CategoriaId"].ToString());
                }
            }
            if (pRow.Table.Columns.Contains("ModeloNome")) { Modelos.Nome = Convert.ToString(pRow["ModeloNome"].ToString()); }
            if (pRow.Table.Columns.Contains("UrlAmigavel")) { UrlAmigavel = Convert.ToString(pRow["UrlAmigavel"].ToString()); }
            if (pRow.Table.Columns.Contains("TituloMenu")) { TituloMenu = Convert.ToString(pRow["TituloMenu"].ToString()); }
            if (pRow.Table.Columns.Contains("TituloPagina")) { TituloPagina = Convert.ToString(pRow["TituloPagina"].ToString()); }
            if (pRow.Table.Columns.Contains("ImagemCompartilha")) { ImagemCompartilha = Convert.ToString(pRow["ImagemCompartilha"].ToString()); }
            if (pRow.Table.Columns.Contains("Descricao")) { Descricao = Convert.ToString(pRow["Descricao"].ToString()); }
            if (pRow.Table.Columns.Contains("PalavrasChave")) { PalavrasChaves = Convert.ToString(pRow["PalavrasChave"].ToString()); }
            if (pRow.Table.Columns.Contains("AtivaDuvidas")) { AtivaDuvidas = bool.Parse(pRow["AtivaDuvidas"].ToString()); }
            if (pRow.Table.Columns.Contains("Actived")) { Actived = bool.Parse(pRow["Actived"].ToString()); }
            if (pRow.Table.Columns.Contains("Orderby"))
            {
                if (!string.IsNullOrEmpty(pRow["Orderby"].ToString()))
                    Orderby = int.Parse(pRow["Orderby"].ToString());
            }
            if (pRow.Table.Columns.Contains("Created")) { Created = DateTime.Parse(pRow["Created"].ToString()); }
            if (pRow.Table.Columns.Contains("Updated")) { Updated = string.IsNullOrEmpty(pRow["Updated"].ToString()) ? DateTime.Now : DateTime.Parse(pRow["Updated"].ToString()); }
        }

        public void DesabilitarHabilitar(int id)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@PaginaId", id)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spDesabilitaHabilitaPagina", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public bool Save()
        {
            var sqlParametros = new List<SqlParameter>();
            sqlParametros.Add(new SqlParameter("@Id", Id));
            sqlParametros.Add(new SqlParameter("@ModeloId", ModeloId));
            sqlParametros.Add(new SqlParameter("@SubCategoriaId", SubCategoriaId));
            sqlParametros.Add(new SqlParameter("@UrlAmigavel", URLFriendly.URLFriendlyConvert(UrlAmigavel)));
            sqlParametros.Add(new SqlParameter("@TituloMenu", TituloMenu));
            sqlParametros.Add(new SqlParameter("@TituloPagina", TituloPagina));
            sqlParametros.Add(new SqlParameter("@ImagemCompartilha", ImagemCompartilha));
            sqlParametros.Add(new SqlParameter("@Descricao", Descricao));
            sqlParametros.Add(new SqlParameter("@PalavrasChaves", PalavrasChaves));
            sqlParametros.Add(new SqlParameter("@AtivaDuvidas", AtivaDuvidas));
            sqlParametros.Add(new SqlParameter("@Actived", Actived));
            sqlParametros.Add(new SqlParameter("@Orderby", Orderby));

            SQLServer sql = new SQLServer();
            Id = Convert.ToInt32(sql.ExecuteScalar("spSavePagina", CommandType.StoredProcedure, sqlParametros.ToArray()));

            return (Id > 0);
        }

        public bool Deletar()
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@paginaId", Id)
            };

            SQLServer sql = new SQLServer();
            sql.ExecuteScalar("spDeletarPagina", CommandType.StoredProcedure, sqlParametros.ToArray());

            return sql.HasError;
        }

        #endregion
        #region :: Static Methods ::

        public static List<Paginas> List()
        {
            return GenericList("spListPaginas");
        }

        public static List<Paginas> ListByModelo(int pModeloId)
        {
            SqlParameter[] sqlParametros =
            {
                new SqlParameter("@modeloId", pModeloId)
            };

            return GenericList("spListPaginasByModelo", sqlParametros);
        }

        public static List<Paginas> GenericList(string pQuery)
        {
            var list = new List<Paginas>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Paginas(item));
            }

            return list;
        }

        public static List<Paginas> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<Paginas>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Paginas(item));
            }

            return list;
        }

        #endregion
    }
}