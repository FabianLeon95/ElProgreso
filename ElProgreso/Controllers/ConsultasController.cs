using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElProgreso.DAL;
using ElProgreso.Models;

namespace ElProgreso.Controllers
{
    public class ConsultasController : Controller
    {
        private ElProgresoContext db = new ElProgresoContext();

        public ActionResult Index()
        {
            return View(db.Preguntas.ToList());
        }

        public ActionResult Pregunta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pregunta([Bind(Include = "Id,PreguntaId,NombreUsuario,Contenido,Fecha")] Respuesta respuesta)
        {
            respuesta.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Respuestas.Add(respuesta);
                db.SaveChanges();
            }

            return RedirectToAction("Pregunta", respuesta.PreguntaId);
        }

        public ActionResult NuevaPregunta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaPregunta([Bind(Include = "Id,NombreUsuario,Titulo,Descripcion,Fecha")] Pregunta pregunta)
        {

            pregunta.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Preguntas.Add(pregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pregunta);
        }
    }
}