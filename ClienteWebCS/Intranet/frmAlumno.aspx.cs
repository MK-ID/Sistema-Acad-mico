using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Intranet_frmAlumno : System.Web.UI.Page
{


    ServiceReferenceUsuario.WSUsuarioSoapClient servicio = new ServiceReferenceUsuario.WSUsuarioSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReporte1_Click(object sender, EventArgs e)
    {
        mvAlumno.ActiveViewIndex = 0;
    }

    private void ExportToExcel(string nameReport, GridView wControl)
    {
        HttpResponse response = Response;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pageToRender = new Page();
        HtmlForm form = new HtmlForm();
        form.Controls.Add(wControl);
        pageToRender.Controls.Add(form);
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport);
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        pageToRender.RenderControl(htw);
        response.Write(sw.ToString());
        response.End();
    }

    protected void btnReporte2_Click(object sender, EventArgs e)
    {
        mvAlumno.ActiveViewIndex = 1;
        // Llamar a los datos del seguimiento academico
        gvSeguimiento.DataSource = servicio.SeguimientoAcademico(User.Identity.Name);
        gvSeguimiento.DataBind();
    }
    protected void btnCambiarUsuario_Click(object sender, EventArgs e)
    {
        mvAlumno.ActiveViewIndex = 2;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel("Informe.xls", gvSeguimiento);
    }
}