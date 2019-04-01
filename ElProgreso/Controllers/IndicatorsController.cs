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
    public class IndicatorsController : Controller
    {  
        // GET: Indicators
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TipoCambioVenta()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CambioVenta()
        {
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Fecha", System.Type.GetType("System.String"));
            dt.Columns.Add("Valor", System.Type.GetType("System.Double"));

            DataRow dr;
            List<IndicadorEconomico> indicadores = GetIndicadores("317", "01/01/2019", "24/03/2019", "Fabian", "N");

            foreach (var indicador in indicadores)
            {
                dr = dt.NewRow();
                dr["Fecha"] = indicador.Fecha.ToString("dd/MM/yyyy");
                dr["Valor"] = indicador.Valor;
                dt.Rows.Add(dr);
            }
            
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public List<IndicadorEconomico> GetIndicadores(string code, string startDate, string endDate, string name, string subLevel)
        {
            wsIndicadoresEconomicosSoapClient client = new wsIndicadoresEconomicosSoapClient();
            string response = client.ObtenerIndicadoresEconomicosXML(code, startDate, endDate, name, subLevel);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);

            List<IndicadorEconomico> indicadores = new List<IndicadorEconomico>();
            const string Xpath = "Datos_de_INGC011_CAT_INDICADORECONOMIC/INGC011_CAT_INDICADORECONOMIC";

            foreach (XmlNode node in xml.SelectNodes(Xpath))
            {
                indicadores.Add(new IndicadorEconomico(
                    node["COD_INDICADORINTERNO"].InnerText,
                    DateTime.Parse(node["DES_FECHA"].InnerText),
                    double.Parse(node["NUM_VALOR"].InnerText, CultureInfo.InvariantCulture)
                    ));
            }

            return indicadores;
        }
    }
}