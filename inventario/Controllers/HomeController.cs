using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventario.Models;

namespace inventario.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.titulo = "Sistema Lista de alumnos";
            return View(alumno.Listar());
        }
        public ActionResult Ver(int id)
        {           
            return View(alumno.Obtener(id));
        }
        public ActionResult Crud(int id=0)
        {
            return View(
                id==0? new Alumno():alumno.Obtener(id)
                );
        }
        //public ActionResult Guardar(Alumno model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        model.Guardar();
        //        return Redirect("~/home");
        //    }
        //    else
        //    {
        //        return View("~/views/home/crud.cshtml", model);                
        //    }            
        //}
        public JsonResult Guardar(Alumno model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm=model.Guardar();
                if(rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
            }
            return Json(rm);
        }
        public ActionResult Eliminar(int id)
        {
            alumno.id = id;
            alumno.Eliminar();
            return Redirect("~/home");
        }

    }
}