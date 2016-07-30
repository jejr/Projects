using DB_HD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Code
{
    public static class Login
    {

        public static DB_HD.HD_Usuario Verificar(string user , string clave)
        {
            DB_HD.DBDataContext db = new DBDataContext();

            var usuario = db.HD_Usuarios.FirstOrDefault(a => a.usuario == user);
            System.Web.HttpContext.Current.Session["user"] = usuario;

            if (usuario != null && usuario.clave == clave)
                return usuario;
            else
                return null;
        }


        public static HD_Usuario GetUsuario(string user)
        {
            DB_HD.DBDataContext db = new DBDataContext();

            if (System.Web.HttpContext.Current.Session["user"] != null)
                return (System.Web.HttpContext.Current.Session["user"] as HD_Usuario);


            var usuario = db.HD_Usuarios.FirstOrDefault(a => a.usuario == user);
            System.Web.HttpContext.Current.Session["user"] = usuario;

            return usuario;
        }


    }
}