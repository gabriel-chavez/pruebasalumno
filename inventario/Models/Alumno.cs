namespace inventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumno()
        {
            Adjunto = new HashSet<Adjunto>();
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        public int Sexo { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjunto> Adjunto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }
        /*************************************************************/
        public List<Alumno> Listar()
        {
            List <Alumno> alumnos = new List<Alumno>();
            try
            {
                using (var ctx = new TestContext())
                {
                    alumnos = ctx.Alumno.ToList();
                }
            }
            catch (Exception e)
            {
               
                throw;
            }
            return alumnos;
        }
        public Alumno Obtener(int id)
        {
            Alumno alumno = new Alumno();
            try
            {
                using (var ctx = new TestContext())
                {
                    //para retornar solo de la tabla alumno
                    // alumno = ctx.Alumno.Where(x => x.id == id)
                    //                   .SingleOrDefault();
                    //para retornar la relacion
                    alumno = ctx.Alumno.Include("AlumnoCurso")
                                       .Include("AlumnoCurso.Curso")
                                       .Where(x => x.id == id)
                                       .SingleOrDefault();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return alumno;
        }
        //para  sincronico no ajax
        //public void Guardar()
        //{           
        //    try
        //    {
        //        using (var ctx = new TestContext())
        //        {
        //            if(this.id>0)
        //            {
        //                ctx.Entry(this).State = System.Data.Entity.EntityState.Modified;
        //            }
        //            else
        //            {
        //                ctx.Entry(this).State = System.Data.Entity.EntityState.Added;
        //            }
        //            ctx.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }            
        //}

        //para ajax
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new TestContext())
                {
                    if (this.id > 0)
                    {
                        ctx.Entry(this).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        ctx.Entry(this).State = System.Data.Entity.EntityState.Added;
                    }
                    rm.SetResponse(true);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public void Eliminar()
        {
            try
            {
                using (var ctx = new TestContext())
                {
                    ctx.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

    }
}
