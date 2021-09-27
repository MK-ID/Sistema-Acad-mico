using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidades;
using System.Data; // Buffer de memoria 

namespace CapaNegocios.Interfaces
{
    interface IUsuario
    {
        bool Login(Usuario usuario);
        DataSet SeguimientoAcademico(Usuario usuario);
    }
}
