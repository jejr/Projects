using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Code
{
    public static class Departamento
    {

        public static List<DB_HD.HD_Departamento> GetDepartamentos()
        {
            DB_HD.DBDataContext db = new DB_HD.DBDataContext();
            var result = db.HD_Departamentos.ToList();

            if(result.Count > 1)
            {
                result.Insert(0, new DB_HD.HD_Departamento { departamento_id = 0, departamento = "--*--" });
            }

            return result;
        }

    }
}