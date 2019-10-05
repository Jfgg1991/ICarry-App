using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICarryApp.Models;

namespace ICarryApp.Controllers
{
    public class serviciosController : Controller
    {
        private ICarryAppDBEntities db = new ICarryAppDBEntities();

        // GET: servicios
        public ActionResult Index()
        {
            var servicio = db.servicio.Include(s => s.persona).Include(s => s.transportador1);
            return View(servicio.ToList());
        }

        // GET: servicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servicio servicio = db.servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // GET: servicios/Create
        public ActionResult Create()
        {
            ViewBag.cliente = new SelectList(db.persona, "cedula", "nombre");
            ViewBag.transportador = new SelectList(db.transportador, "id", "vehiculo");
            return View();
        }

        // POST: servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,descripcionServicio,cliente,transportador,carga,direccionCague,direccionEntrega,costo")] servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.servicio.Add(servicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cliente = new SelectList(db.persona, "cedula", "nombre", servicio.cliente);
            ViewBag.transportador = new SelectList(db.transportador, "id", "vehiculo", servicio.transportador);
            return View(servicio);
        }

        // GET: servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servicio servicio = db.servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente = new SelectList(db.persona, "cedula", "nombre", servicio.cliente);
            ViewBag.transportador = new SelectList(db.transportador, "id", "vehiculo", servicio.transportador);
            return View(servicio);
        }

        // POST: servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,descripcionServicio,cliente,transportador,carga,direccionCague,direccionEntrega,costo")] servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente = new SelectList(db.persona, "cedula", "nombre", servicio.cliente);
            ViewBag.transportador = new SelectList(db.transportador, "id", "vehiculo", servicio.transportador);
            return View(servicio);
        }

        // GET: servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servicio servicio = db.servicio.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            servicio servicio = db.servicio.Find(id);
            db.servicio.Remove(servicio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
