using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Code
{
    public static class Publicaciones
    {

        public static IList GetPublicaciones()
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            var pbs = from p in db.HD_Publicacions
                      where p.activo == true
                      select new
                      {
                          p.publicacion_id,
                          p.encabezado,
                          usuario = p.HD_Usuario.nombres + " " + p.HD_Usuario.apellidos,
                          p.fecha,
                          p.usuario_id
                      };

            return pbs.ToList();
        }

        public static bool CrearPublicacion(int usuario_id, string encabezado, string contenido)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            DB_HD.HD_Publicacion publicacion = new DB_HD.HD_Publicacion();
            DB_HD.HD_Usuario usuario = db.HD_Usuarios.FirstOrDefault(a => a.usuario_id == usuario_id);
            try
            {

                publicacion.usuario_id = usuario_id;
                publicacion.fecha = DateTime.Now;
                publicacion.activo = true;
                publicacion.encabezado = encabezado;
                publicacion.contenido = contenido;
                db.HD_Publicacions.InsertOnSubmit(publicacion);
                db.SubmitChanges();

                foreach (var seg in usuario.HD_Usuario_Seguidores)
                {
                    if (seg.seguidor_id == usuario_id)
                        continue;

                    var notif = new DB_HD.HD_Usuario_Notificacion();
                    notif.fecha = DateTime.Now;
                    notif.usuario_id = seg.seguidor_id;
                    notif.tipo_id = (int)Enums.TipoNotificacion.NuevaPublicacion;
                    notif.comentario = String.Format("{0} ha creado una nueva publicacion", usuario.nombres );
                    notif.id = publicacion.publicacion_id;

                    db.HD_Usuario_Notificacions.InsertOnSubmit(notif);
                }

                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DB_HD.HD_Publicacion GetPublicacion(int pid, int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            var pb = (from p in db.HD_Publicacions
                      where p.publicacion_id == pid
                      select p).FirstOrDefault();

            var ud = from n in db.HD_Usuario_Notificacions
                     where n.usuario_id == usuario_id & n.visto == false
                     select n;

            foreach(var n in ud)
            {
                if(n.id == pid & ( n.tipo_id == (int)Enums.TipoNotificacion.NuevaPublicacion || n.tipo_id == (int)Enums.TipoNotificacion.ComentarioPublicacion))
                {
                    n.visto = true;
                }

            }
            db.SubmitChanges();
            return pb;
        }

        public static bool PublicarComentario(int pid, string comentario, int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            DB_HD.HD_Publicacion_Comentario cmm = new DB_HD.HD_Publicacion_Comentario();

            var publicacion = db.HD_Publicacions.FirstOrDefault(a => a.publicacion_id == pid);
            cmm.publicacion_id = pid;
            cmm.comentario = comentario;
            cmm.fecha = DateTime.Now;
            cmm.usuario_id = usuario_id;
            DB_HD.HD_Usuario usuario = db.HD_Usuarios.FirstOrDefault(a => a.usuario_id == usuario_id);
            try
            {

                db.HD_Publicacion_Comentarios.InsertOnSubmit(cmm);
                db.SubmitChanges();

                foreach (var seg in publicacion.HD_Publicacion_Usuarios)
                {
                    if (seg.usuario_id == usuario_id)
                        continue;

                    var notif = new DB_HD.HD_Usuario_Notificacion();
                    notif.fecha = DateTime.Now;
                    notif.usuario_id = seg.usuario_id;
                    notif.tipo_id = (int)Enums.TipoNotificacion.ComentarioPublicacion;
                    notif.comentario = string.Format("{0} ha comentado una publicacion que sigues.", usuario.nombres);
                    notif.id = pid;

                    db.HD_Usuario_Notificacions.InsertOnSubmit(notif);
                }
                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public static bool SeguirPublicacion(int pid, int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            try
            {
                DB_HD.HD_Publicacion_Usuario puid = db.HD_Publicacion_Usuarios.FirstOrDefault(a => a.publicacion_id == pid & a.usuario_id == usuario_id);

                if (puid == null)
                {
                    puid = new DB_HD.HD_Publicacion_Usuario();
                    db.HD_Publicacion_Usuarios.InsertOnSubmit(puid);
                    puid.publicacion_id = pid;
                    puid.usuario_id = usuario_id;
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


    }
}