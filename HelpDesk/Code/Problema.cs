using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Code
{
    public class Problema
    {
        public static List<DB_HD.HD_Problema> GetProblema(int departamento_id)
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            var result = db.HD_Problemas.Where(a=> a.departamento_id == departamento_id).ToList();

            if (result.Count > 1)
            {
                result.Insert(0, new DB_HD.HD_Problema { problema_id = 0, problema = "--*--" });
            }

            return result;
        }

    }
}