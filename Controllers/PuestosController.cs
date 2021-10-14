using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_db.Models;
namespace web_api_db.Models.Controllers{
    [Route("api/[controller]")]
    public class PuestosController : Controller{

         private Conexion dbConexion;

         public PuestosController(){
            dbConexion = Conectar.Create();
        }
        //consulta todos los datos ----metodo get
        [HttpGet]
        public ActionResult Get(){
            return Ok(dbConexion.Puestos.ToArray());
        }
        //consulta por id ----metodo get
         [HttpGet ("{id}")]
        public async Task<ActionResult> Get(int id){
            var puestos = await dbConexion.Puestos.FindAsync(id);        
            if (puestos !=null)  {
                return Ok(puestos);
            }else
            {
                return NotFound();
            }
        }
        //insertar datos---metodo post fromBody--
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Puestos puestos){
            if(ModelState.IsValid){
                dbConexion.Puestos.Add(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok(puestos);

            }else
            {
                return NotFound();
            }
        
        }
        //modificar datos---metodo put fromBody--
         [HttpPut]
        public async Task<ActionResult> Put([FromBody] Puestos puestos){
            var v_puestos = dbConexion.Puestos.SingleOrDefault(a => a.id_puesto == puestos.id_puesto);
            if(v_puestos !=null && ModelState.IsValid){
                dbConexion.Entry(v_puestos).CurrentValues.SetValues(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();
            } else
            {
                return NotFound();
            }
        }
        //Eliminar datos---metodo put 
        [HttpDelete ("{id}")]

        public async Task<ActionResult> Delete(int id){
             var puestos = dbConexion.Puestos.SingleOrDefault(a => a.id_puesto == id);
             if(puestos !=null){
                 dbConexion.Puestos.Remove(puestos);
                 await dbConexion.SaveChangesAsync();
                 return Ok();
                 
             }else
             {
                 return NotFound();
             }
        }

    }
}