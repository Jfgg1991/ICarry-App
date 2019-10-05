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
    public class transportadorsController : Controller
    {
        private ICarryAppDBEntities db = new ICarryAppDBEntities();

        // GET: transportadors
        public ActionResult Index()
        {
            var transportador = db.transportador.Include(t => t.persona).Include(t => t.vehiculo1);
            return View(transportador.ToList());
        }

        // GET: transportadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportador transportador = db.transportador.Find(id);
            if (transportador == null)
            {
                return HttpNotFound();
            }
            return View(transportador);
        }

        // GET: transportadors/Create
        public ActionResult Create()
        {
            ViewBag.cedula = new SelectList(db.persona, "cedula", "nombre");
            ViewBag.vehiculo = new SelectList(db.vehiculo, "Placa", "numeroChasis");
            return View();
        }

        // POST: transportadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cedula,vehiculo,estado")] transportador transportador)
        {
            if (ModelState.IsValid)
            {
                db.transportador.Add(transportador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedula = new SelectList(db.persona, "cedula", "nombre", transportador.cedula);
            ViewBag.vehiculo = new SelectList(db.vehiculo, "Placa", "numeroChasis", transportador.vehiculo);
            return View(transportador);
        }

        // GET: transportadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportador transportador = db.transportador.Find(id);
            if (transportador == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula = new SelectList(db.persona, "cedula", "nombre", transportador.cedula);
            ViewBag.vehiculo = new SelectList(db.vehiculo, "Placa", "numeroChasis", transportador.vehiculo);
            return View(transportador);
        }

        // POST: transportadors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cedula,vehiculo,estado")] transportador transportador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedula = new SelectList(db.persona, "cedula", "nombre", transportador.cedula);
            ViewBag.vehiculo = new SelectList(db.vehiculo, "Placa", "numeroChasis", transportador.vehiculo);
            return View(transportador);
        }

        // GET: transportadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transportador transportador = db.transportador.Find(id);
            if (transportador == null)
            {
                return HttpNotFound();
            }
            return View(transportador);
        }

        // POST: transportadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            transportador transportador = db.transportador.Find(id);
            db.transportador.Remove(transportador);
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
