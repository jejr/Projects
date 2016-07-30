
using System;
using System.Collections;
using Microsoft.VisualBasic;
using System.Linq;
using System.Web;

namespace HelpDesk.Code
{
    public static class Solicitud
    {

        public static bool CrearSolicitud(string titulo, int departamento_id, int problema_id, string contenido, int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            DB_HD.HD_Solicitud solic = new DB_HD.HD_Solicitud();

            try
            {
                var dep = db.HD_Departamentos.FirstOrDefault(a => a.departamento_id == departamento_id);
                solic.departamento_id = departamento_id;
                solic.descripcion = contenido;
                solic.estado_id = (int)Enums.Estado.Abierto;
                solic.fecha = DateTime.Now;
                solic.problema_id = problema_id;
                solic.titulo = titulo;
                solic.usr_solicita_id = usuario_id;
                db.HD_Solicituds.InsertOnSubmit(solic);
                db.SubmitChanges();

                foreach (var usr in dep.HD_Usuarios)
                {
                    if (usr.usuario_id == usuario_id)
                        continue;

                    var notif = new DB_HD.HD_Usuario_Notificacion();
                    notif.fecha = DateTime.Now;
                    notif.usuario_id = usr.usuario_id;
                    notif.tipo_id = (int)Enums.TipoNotificacion.NuevaSolicitud;
                    notif.comentario = String.Format("{0} ha creado una solictud", solic.HD_Usuario_Solicita.nombres);
                    notif.id = solic.solicitud_id;

                    db.HD_Usuario_Notificacions.InsertOnSubmit(notif);
                }

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public static DB_HD.HD_Solicitud GetSolicitud(int solicitud_id, int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            DB_HD.HD_Solicitud solic = db.HD_Solicituds.FirstOrDefault(a => a.solicitud_id == solicitud_id);


            var ud = from n in db.HD_Usuario_Notificacions
                     where n.usuario_id == usuario_id & n.visto == false
                     select n;

            foreach (var n in ud)
            {
                if (n.id == solicitud_id & (n.tipo_id == (int)Enums.TipoNotificacion.NuevaSolicitud || n.tipo_id == (int)Enums.TipoNotificacion.ComentarioSolicitud))
                {
                    n.visto = true;
                }

            }
            db.SubmitChanges();

            return solic;
        }

        public static bool ComentarSolicitud(int sid, int usuario_id, string comentario, string archivo)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            DB_HD.HD_Solicitud_Comentario scomm = new DB_HD.HD_Solicitud_Comentario();

            scomm.solicitud_id = sid;
            scomm.usuario_id = usuario_id;
            scomm.fecha = DateTime.Now;
            scomm.comentario = comentario;
            scomm.rutaFile = archivo;

            try
            {

                db.HD_Solicitud_Comentarios.InsertOnSubmit(scomm);
                db.SubmitChanges();

                foreach (var usr in scomm.HD_Solicitud.HD_Departamento.HD_Usuarios)
                {
                    if (usr.usuario_id == usuario_id)
                        continue;

                    var notif = new DB_HD.HD_Usuario_Notificacion();
                    notif.fecha = DateTime.Now;
                    notif.usuario_id = usr.usuario_id;
                    notif.tipo_id = (int)Enums.TipoNotificacion.ComentarioSolicitud;

                    if (usr.usuario_id == scomm.HD_Solicitud.usr_solicita_id)
                        notif.comentario = String.Format("{0} ha actualizado una solicitud tuya", scomm.HD_Usuario.nombres);
                    else
                        notif.comentario = String.Format("{0} ha actualizado una solicitud", scomm.HD_Usuario.nombres);
                    notif.id = sid;

                    db.HD_Usuario_Notificacions.InsertOnSubmit(notif);
                }

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static IList GetSolicitudes()
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();

            var pbs = from p in db.HD_Solicituds
                      where p.estado_id == (int)Enums.Estado.Abierto
                      select new
                      {
                          p.solicitud_id,
                          p.titulo,
                          p.HD_Estado.estado,
                          usuario = p.HD_Usuario_Asignado.nombres + " " + p.HD_Usuario_Asignado.apellidos,
                          p.fecha,
                          p.HD_Departamento.departamento,
                          p.HD_Problema.problema
                      };

            return pbs.ToList();
        }

        public static IList GetSolicitudes(object criterio)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            var pbs = from p in db.HD_Solicituds
                      select p;

            if (criterio.ToString() == "No_Asignados")
                pbs = pbs.Where(a => a.estado_id != (int)Enums.Estado.Cerrado & a.usr_asignado_id == null);

            else if (criterio.ToString() == "Asignados")
                pbs = pbs.Where(a => a.estado_id != (int)Enums.Estado.Cerrado & a.usr_asignado_id != null);

            else if (criterio.ToString() == "Abiertos")
                pbs = pbs.Where(a => a.estado_id != (int)Enums.Estado.Cerrado);

            else if (criterio.ToString() == "Cerrados")
                pbs = pbs.Where(a => a.estado_id == (int)Enums.Estado.Cerrado);

            else if (criterio.ToString() == "Suspendidos")
                pbs = pbs.Where(a => a.estado_id == (int)Enums.Estado.Suspendido);


            var lista = from p in pbs
                        select new
                        {
                            p.solicitud_id,
                            p.titulo,
                            p.HD_Estado.estado,
                            usuario = p.HD_Usuario_Asignado.nombres + " " + p.HD_Usuario_Asignado.apellidos,
                            p.fecha,
                            p.HD_Departamento.departamento,
                            p.HD_Problema.problema
                        };

            return lista.ToList();
        }

        public static bool AsignarSolicitud(int sid, int uid, int usuario_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            var solicitud = db.HD_Solicituds.FirstOrDefault(a => a.solicitud_id == sid);

            if (solicitud.usr_asignado_id == uid)
                return true;

            var usuario = db.HD_Usuarios.FirstOrDefault(a => a.usuario_id == uid);
            var usuarioAsignador = db.HD_Usuarios.FirstOrDefault(a => a.usuario_id == usuario_id);
            solicitud.usr_asignado_id = uid;

            try
            {

                DB_HD.HD_Solicitud_Comentario scomm = new DB_HD.HD_Solicitud_Comentario();
                scomm.rutaFile = "";
                scomm.comentario = string.Format("{0} {1} asigno el ticket a {2} {3}", usuarioAsignador.nombres, usuarioAsignador.apellidos, usuario.nombres, usuario.apellidos);
                scomm.fecha = DateTime.Now;
                scomm.solicitud_id = sid;
                scomm.usuario_id = usuario_id;

                db.HD_Solicitud_Comentarios.InsertOnSubmit(scomm);


                foreach (var usr in solicitud.HD_Departamento.HD_Usuarios)
                {
                    if (usr.usuario_id == usuario_id)
                        continue;

                    var notif = new DB_HD.HD_Usuario_Notificacion();
                    notif.fecha = DateTime.Now;
                    notif.usuario_id = usr.usuario_id;
                    notif.tipo_id = (int)Enums.TipoNotificacion.ComentarioSolicitud;

                    if (usr.usuario_id == solicitud.usr_solicita_id)
                        notif.comentario = String.Format("Tu solicitud no. {1}, ha sido asignada a {0}", usuario.nombres, sid);
                    else if (usr.usuario_id == solicitud.usr_asignado_id)
                        notif.comentario = String.Format("La solicitud no. {1} fue asignada a Tí", usuario.nombres, sid);
                    else
                        notif.comentario = String.Format("La solicitud no. {1} fue asignada a {0}", usuario.nombres, sid);
                    notif.id = sid;

                    db.HD_Usuario_Notificacions.InsertOnSubmit(notif);
                }

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool CambiarEstado(int sid, int estado_id, int usuario_id, bool notificar = true)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            var solicitud = db.HD_Solicituds.FirstOrDefault(a => a.solicitud_id == sid);
            var estado = db.HD_Estados.FirstOrDefault(a => a.estado_id == estado_id);
            var usuario = db.HD_Usuarios.FirstOrDefault(a => a.usuario_id == usuario_id);
            string text = "{0} {1} cambio el estado a {2}";
            try
            {

                if (estado_id == 0)
                {
                    solicitud.usr_asignado_id = null;
                    estado = solicitud.HD_Estado;
                    text = "{0} {1} ha desasignado la solicitud.";
                }

                else
                    solicitud.estado_id = estado_id;

                DB_HD.HD_Solicitud_Comentario scomm = new DB_HD.HD_Solicitud_Comentario();
                scomm.rutaFile = "";
                scomm.comentario = string.Format(text, usuario.nombres, usuario.apellidos, estado.estado);
                scomm.fecha = DateTime.Now;
                scomm.solicitud_id = sid;
                scomm.usuario_id = usuario_id;

                db.HD_Solicitud_Comentarios.InsertOnSubmit(scomm);

                if (notificar)
                {

                    foreach (var usr in solicitud.HD_Departamento.HD_Usuarios)
                    {
                        if (usr.usuario_id == usuario_id)
                            continue;

                        var notif = new DB_HD.HD_Usuario_Notificacion();
                        notif.fecha = DateTime.Now;
                        notif.usuario_id = usr.usuario_id;
                        notif.tipo_id = (int)Enums.TipoNotificacion.ComentarioSolicitud;

                        if (usr.usuario_id == solicitud.usr_solicita_id)
                            notif.comentario = String.Format("Tu solicitud no. {0}, ha cambiado al estado {1}", sid, estado.estado);
                        else if (usr.usuario_id == solicitud.usr_asignado_id)
                            notif.comentario = String.Format("Tu solicitud asignada no. {0}, ha cambiado al estado {1}", sid, estado.estado);
                        else
                            notif.comentario = String.Format("La solicitud no. {0}, ha cambiado al estado {1}", sid, estado.estado);
                        notif.id = sid;

                        db.HD_Usuario_Notificacions.InsertOnSubmit(notif);
                    }
                }

                db.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static string Resumen()
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            string result = "[ ";

            foreach(var r in db.VW_Resumens)
            {
                if(r.estado.ToUpper() == "ABIERTO")
                result += string.Format("{{ \"value\" : {0} ,\"label\" : \"{1}\" , \"color\": \"#ff8153\" }}, ", r.cantidad,r.estado);

                else if (r.estado.ToUpper() == "CERRADO")
                    result += string.Format("{{ \"value\" : {0} ,\"label\" : \"{1}\" , \"color\": \"#4acab4\" }}, ", r.cantidad, r.estado);

                else if (r.estado.ToUpper() == "SUSPENDIDO")
                    result += string.Format("{{ \"value\" : {0} ,\"label\" : \"{1}\" , \"color\": \"#ffea88\" }}, ", r.cantidad, r.estado);
            }

            result = result.Substring(0, result.Length - 2);

            result += "]";

            return result;


        }
    }
}