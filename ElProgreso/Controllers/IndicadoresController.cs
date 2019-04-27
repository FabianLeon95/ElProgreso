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
using ElProgreso.DAL;

namespace ElProgreso.Controllers
{
    public class IndicadoresController : Controller
    {
        private ElProgresoContext db = new ElProgresoContext();

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

            DateTime since = DateTime.Today.AddYears(-1);

            List<IndicadorEconomico> compra = db.IndicadoresEconomicos.Where(i => i.Codigo == "317" && i.Fecha > since).ToList();
            foreach (var dato in compra)
            {
                dates.Add(dato.Fecha.ToString("dd/MM/yyyy"));
                compraValues.Add(dato.Valor);
            }

            List<IndicadorEconomico> venta = db.IndicadoresEconomicos.Where(i => i.Codigo == "318" && i.Fecha > since).ToList(); ;
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

            List<IndicadorEconomico> indicadores = db.IndicadoresEconomicos.Where(i => i.Codigo == "317" && i.Fecha > DateTime.Today.AddYears(-1)).ToList();
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

            List<IndicadorEconomico> indicadores = db.IndicadoresEconomicos.Where(i => i.Codigo == "317" && i.Fecha > DateTime.Today.AddYears(-1)).ToList();
            foreach (var dato in indicadores)
            {
                dates.Add(dato.Fecha.ToString("dd/MM/yyyy"));
                values.Add(dato.Valor);
            }

            response.Add(dates);
            response.Add(values);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}