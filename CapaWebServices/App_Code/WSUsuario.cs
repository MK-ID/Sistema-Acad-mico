using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using CapaEntidades;
using CapaNegocios;
using System.Security.Cryptography;
using System.Text;
using System.Data;

/// <summary>
/// Descripción breve de WSUsuario
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WSUsuario : System.Web.Services.WebService {

    public WSUsuario () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    private string generarClaveSHA1(string cadena)
    {

        UTF8Encoding enc = new UTF8Encoding();
        byte[] data = enc.GetBytes(cadena);
        byte[] result;

        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();

        result = sha.ComputeHash(data);


        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {

            // Convertimos los valores en hexadecimal
            // cuando tiene una cifra hay que rellenarlo con cero
            // para que siempre ocupen dos dígitos.
            if (result[i] < 16)
            {
                sb.Append("0");
            }
            sb.Append(result[i].ToString("x"));
        }

        //Devolvemos la cadena con el hash en mayúsculas para que quede más chuli :)
        return sb.ToString().ToUpper();
    }


    [WebMethod(Description="Login Usuario")]
    public string[] Login(String _Usuario, string _Contrasena)
    {
        Usuario usuario = new Usuario();
        UsuarioBL usuarioBL = new UsuarioBL();
        usuario._Usuario = _Usuario;
        usuario._Contrasena = generarClaveSHA1(_Contrasena);
        bool codError = usuarioBL.Login(usuario);
        string[] valores = new string[2];
        // Traer el CodError y el Mensaje del PA
        if (codError == true) valores[0] = "0";
        else valores[0] = "1";
        valores[1] = usuarioBL.Mensaje;
        return valores;
    }

    [WebMethod(Description="Seguimiento academico de un usuario")]
    public DataSet SeguimientoAcademico(string _Usuario)
    {
        Usuario usuario = new Usuario();
        UsuarioBL usuarioBL = new UsuarioBL();
        usuario._Usuario = _Usuario;
        return usuarioBL.SeguimientoAcademico(usuario);
    }   
    
}
