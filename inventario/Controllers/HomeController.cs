using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventario.Models;
using System.IO;

namespace inventario.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();
        private AlumnoCurso alumno_curso= new AlumnoCurso();
        private Curso curso = new Curso();
        public Adjunto adjunto = new Adjunto();
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
        public PartialViewResult Cursos(int Alumno_id)
        {
            //listamos los cursos de un alumno
            ViewBag.cursosElegidos = alumno_curso.Listar(Alumno_id);
            //listamos todos los cursos disponible
            ViewBag.cursos = curso.Todos(Alumno_id);
            //modelo
            alumno_curso.Alumno_id = Alumno_id;
            return PartialView(alumno_curso);//revisar, sin variable retornada tambien funciona
        }
        public PartialViewResult Adjuntos(int Alumno_id)
        {
            ViewBag.Adjuntos = adjunto.Listar(Alumno_id);
            return PartialView();
        }
        public JsonResult GuardarCurso(AlumnoCurso model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.function = "CargarCursos()";
                    
                }
            }
            return Json(rm);
        }
        public JsonResult GuardarAdjunto(Adjunto model, HttpPostedFileBase Archivo)
        {
            var rm = new ResponseModel();
            if(Archivo !=  null)
            {
                //nombre del archivo, lo renombramos para evitar su repeticion
                string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Archivo.FileName);
                //ruta donde se guarda
                Archivo.SaveAs(Server.MapPath("~/uploads/" + archivo));
               // Archivo.SaveAs("~/uploads/" + archivo);
                //establecemos el nombre
                model.Archivo = archivo;
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.function = "CargarAdjuntos()";

                }
            }
            rm.SetResponse(false,"Debe adjuntar un archivo");
            return Json(rm);
        }
        public ActionResult prueba(int id)
        {
            return View();
        }
        public ActionResult Crud(int id = 0)
        {
            return View(
                id == 0 ? new Alumno() : alumno.Obtener(id)
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
                    rm.function = "soyalgo()";
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