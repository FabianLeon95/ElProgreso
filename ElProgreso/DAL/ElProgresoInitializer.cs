using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElProgreso.DAL
{
    public class ElProgresoInitializer : DropCreateDatabaseIfModelChanges<ElProgresoContext>
    {
        protected override void Seed(ElProgresoContext context)
        {
           
        }
    }
}