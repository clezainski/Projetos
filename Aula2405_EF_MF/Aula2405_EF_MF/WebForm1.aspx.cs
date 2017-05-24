using Aula2405_EF_MF.Models;
using Aula2405_EF_MF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aula2405_EF_MF
{
    public partial class WebForm1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Nome = txtNome.Text;
            categoria.Descricao = txtDescricao.Text;

            contexto.Categorias.Add(categoria);
            contexto.SaveChanges();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDescricao.Text = string.Empty;
            txtNome.Text = string.Empty;
        }
    }
}