namespace inventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }
        /***********************************/
        public List<Curso> Todos(int Alumno_id = 0)
        {
            var cursos = new List<Curso>();
            try
            {                
                using (var ctx = new TestContext())
                {
                    if(Alumno_id>0)
                    {
                        var cursos_tomados = ctx.AlumnoCurso.Where(x => x.Alumno_id == Alumno_id)
                                                        .Select(x => x.Curso_id)
                                                        .ToList();
                        cursos = ctx.Curso.Where(x => !cursos_tomados.Contains(x.id)).ToList();
                    }
                    else
                    {
                        cursos = ctx.Curso.ToList();
                    }
                    
                    //SELECT * FROM Curso c
                    //WHERE c.id NOT IN(SELECT Curso_id FROM AlumnoCurso ac WHERE ac.Curso_id = c.id AND ac.Alumno_id = 2)
                    
                    //forma mas sencilla
                    /*cursos = ctx.Database.SqlQuery<Curso>("SELECT c.* FROM Curso c WHERE c.id NOT IN(SELECT Curso_id FROM AlumnoCurso ac WHERE ac.Curso_id = c.id AND ac.Alumno_id = @Alumno_id)", new SqlParameter("Alumno_id", Alumno_id));
                                        .ToList();*/
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return cursos;
        }
    }
    
}
