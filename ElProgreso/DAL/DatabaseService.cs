using ElProgreso.BCCRWebService;
using ElProgreso.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ElProgreso.DAL
{
    public class DatabaseService
    {
        private ElProgresoContext db = new ElProgresoContext();

        public void UpdateDatabase()
        {
            string[] codes = { "317", "318", "423", "3541" };

            if (!db.Updates.Any())
            {
                List<IndicadorEconomico> indicadores = new List<IndicadorEconomico>();

                Parallel.ForEach(codes, (code) =>
                {
                    List<IndicadorEconomico> response = GetIndicadores(code, DateTime.Today.AddYears(-5).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "El Progreso", "N");
                    indicadores.AddRange(response);
                });

                db.IndicadoresEconomicos.AddRange(indicadores);

                db.Updates.Add(new Update
                {
                    UpdatedAt = DateTime.Now
                });

                db.SaveChanges();
            }
            else
            {
                Update lastUpdate = db.Updates.Find(1);

                if (lastUpdate.UpdatedAt.Date != DateTime.Now.Date)
                {
                    List<IndicadorEconomico> indicadores = new List<IndicadorEconomico>();

                    Parallel.ForEach(codes, (code) =>
                    {
                        List<IndicadorEconomico> response = GetIndicadores(code, DateTime.Today.AddYears(-5).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "El Progreso", "N");
                        indicadores.AddRange(response);
                    });

                    db.IndicadoresEconomicos.AddRange(indicadores);

                    Update newUpdate = new Update { Id = 1, UpdatedAt = DateTime.Now };
                    db.Entry(lastUpdate).CurrentValues.SetValues(newUpdate);

                    db.SaveChanges();
                }
            }
        }

        private List<IndicadorEconomico> GetIndicadores(string code, string startDate, string endDate, string name, string subLevel)
        {
            wsIndicadoresEconomicosSoapClient client = new wsIndicadoresEconomicosSoapClient();
            DataSet response = client.ObtenerIndicadoresEconomicos(code, startDate, endDate, name, subLevel);

            List<IndicadorEconomico> indicadores = new List<IndicadorEconomico>();

            foreach (DataRow item in response.Tables[0].Rows)
            {
                indicadores.Add(new IndicadorEconomico(
                    item.ItemArray[0].ToString(),
                    DateTime.Parse(item.ItemArray[1].ToString()),
                    double.Parse(item.ItemArray[2].ToString())
                    ));
            }

            return indicadores;
        }
    }
}