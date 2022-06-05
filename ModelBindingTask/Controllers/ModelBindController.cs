using Microsoft.AspNetCore.Mvc;
using ModelBindingTask.Data;
using ModelBindingTask.Models;

namespace ModelBindingTask.Controllers
{
    
    public class ModelBindController : Controller
    {
        private readonly ApplicatonDbContext _context;
        public ModelBindController(ApplicatonDbContext context)
        {
            _context = context;  
        }
        [HttpGet]
        public IActionResult Index()
        {
            var getAllStd = _context.Students.ToList();
            
            return View(getAllStd);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var std = _context.Students.FirstOrDefault(s => s.Id == id);
                return View(std);
            }
            
        }


        //FromForm
        [HttpPost]
        public async Task<IActionResult> Upsert([FromForm]Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == null)
                {
                    await _context.Students.AddAsync(student);                
                }
                else
                {
                    var model = _context.Students.Find(student.Id);
                    if (model == null)
                    {
                        return NotFound();
                    }
                    _context.Students.Update(student);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
                return View(student);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            var std =  _context.Students.Find(id);
            _context.Students.Remove(std);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //FromQuery
        [HttpGet]
        public IActionResult GetDataFromQuery([FromQuery] Student student,[FromQuery]int?id )
        {
            if(student.Id == null)
            {
                _context.Students.Add(student);
            }
            else if(student.Id == id)
            {
                _context.Students.Update(student);
            }
            else
            {
                return NotFound();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        //("getdatafromroute/{id?}/{Name}/{Class}")
        //FromRoute
        [HttpPost]
        [Route("GetDataFromRoute/{id?}/{Name}/{Class}")]
        public IActionResult GetDataFromRoute([FromRoute] Student student, [FromRoute]int?id)
        {
            //var student = new Student();
            //student.Id = id;
            //student.Name = Name;
            //student.Class = Class;

            if (student.Id == null)
            {
                _context.Students.Add(student);
            }
            else if (student.Id == id)
            {
                _context.Students.Update(student);
            }
            else
            {
                return NotFound();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //GetDataFromBody

        [HttpPost]
        public IActionResult GetDataFromBody([FromBody] Student student,[FromBody] int? id)
        {
            if (student.Id == null)
            {
                _context.Students.Add(student);
            }
            else if (student.Id == id)
            {
                _context.Students.Update(student);
            }
            else
            {
                return NotFound();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        //GetDataFromHeader

        [HttpPost]
        public IActionResult GetDataFromHeader([FromHeader] string Name, [FromHeader] string Class,[FromHeader] int? id  )
        {
            var student = new Student();
            student.Id = id;
            student.Name = Name;
            student.Class = Class;
            if (student.Id == null)
            {
                _context.Students.Add(student);
            }
            else if (student.Id == id)
            {
                _context.Students.Update(student);
            }
            else
            {
                return NotFound();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
