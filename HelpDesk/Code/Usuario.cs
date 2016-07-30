using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Code
{
    public static class Usuario
    {

        public static bool SeguirUsuario(int usuario_id ,int seguidor_id)
        {

            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            try
            {
                DB_HD.HD_Usuario_Seguidore puid = db.HD_Usuario_Seguidores.FirstOrDefault(a => a.usuario_id == usuario_id & a.seguidor_id == seguidor_id);

                if (puid == null)
                {
                    puid = new DB_HD.HD_Usuario_Seguidore();
                    db.HD_Usuario_Seguidores.InsertOnSubmit(puid);
                    puid.usuario_id = usuario_id;
                    puid.seguidor_id = seguidor_id;
                }


                puid.fecha = DateTime.Now;

                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<DB_HD.HD_Usuario_Notificacion> NotificacionUsuario(int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            var result = (from n in db.HD_Usuario_Notificacions
                         where n.usuario_id == usuario_id & n.visto == false
                         select n).ToList();

            return result;
        }

        public static List<DB_HD.HD_Usuario> GetUsuariosPorDepartamento(int usuario_id , int departamento_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            var result = db.HD_Usuarios.Where(a => a.departamento_id == departamento_id  ).ToList();

            return result;
        }

    }
}