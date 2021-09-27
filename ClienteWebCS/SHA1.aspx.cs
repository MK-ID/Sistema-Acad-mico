using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Text;

public partial class SHA1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string clave = TextBox1.Text;
        Response.Write(generarClaveSHA1(clave));
    }
}