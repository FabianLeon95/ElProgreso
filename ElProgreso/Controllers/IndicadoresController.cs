using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElProgreso.BCCRWebService;
using System.Xml;
using ElProgreso.Models;
using System.Globalization;
using System.Data;

namespace ElProgreso.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicators
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TipoDeCambio()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTipoDeCambio()
        {
            List<object> response = new List<object>();
            List<string> dates = new List<string>();
            List<double> compraValues = new List<double>();
            List<double> ventaValues = new List<double>();

            List<IndicadorEconomico> compra = GetIndicadores("317", DateTime.Today.AddYears(-3).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "El Progreso", "N");
            foreach (var dato in compra)
            {
                dates.Add(dato.Fecha.ToString("dd/MM/yyyy"));
                compraValues.Add(dato.Valor);
            }

            List<IndicadorEconomico> venta = GetIndicadores("318", DateTime.Today.AddYears(-3).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "El Progreso", "N");
            foreach (var dato in venta)
            {
                ventaValues.Add(dato.Valor);
            }

            response.Add(dates);
            response.Add(compraValues);
            response.Add(ventaValues);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaPoliticaMonetaria()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTasaPoliticaMonetaria()
        {
            List<object> response = new List<object>();
            List<string> dates = new List<string>();
            List<double> values = new List<double>();

            List<IndicadorEconomico> indicadores = GetIndicadores("3541", "01/01/2019", DateTime.Today.ToString("dd/MM/yyyy"), "El Progreso", "N");
            foreach (var dato in indicadores)
            {
                dates.Add(dato.Fecha.ToString("dd/MM/yyyy"));
                values.Add(dato.Valor);
            }

            response.Add(dates);
            response.Add(values);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaBasicaPasiva()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTasaBasicaPasiva()
        {
            List<object> response = new List<object>();
            List<string> dates = new List<string>();
            List<double> values = new List<double>();

            List<IndicadorEconomico> indicadores = GetIndicadores("423", "01/01/2019", DateTime.Today.ToString("dd/MM/yyyy"), "El Progreso", "N");
            foreach (var dato in indicadores)
            {
                dates.Add(dato.Fecha.ToString("dd/MM/yyyy"));
                values.Add(dato.Valor);
            }

            response.Add(dates);
            response.Add(values);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public List<IndicadorEconomico> GetIndicadores(string code, string startDate, string endDate, string name, string subLevel)
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