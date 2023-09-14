using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab1.Models;

namespace Lab1.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();

        private static int id = 0;

        public StudentController()
        {
            if (listStudents.Count == 0)

                listStudents.AddRange(new List<Student>
    {
                new Student() { Id = ++id, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male,
                IsRegular=true, Address = "A1-2018", Email = "nam@g.com", Image="/Image/user_male.jpg"},

                new Student() { Id = ++id, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female,
                    IsRegular=true, Image="/Image/user_female.jpg",
                    Address = "A1-2019", Email = "tu@g.com" },
                new Student() { Id = ++id, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male,
                IsRegular=false,
                Address = "A1-2020", Email = "phong@g.com",  Image="/Image/user_female.jpg" },
                new Student() { Id = ++id, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female,
                IsRegular = false,  Image="/Image/user_female.jpg",
                Address = "A1-2021", Email = "mai@g.com" }

                });
        }

        [HttpGet("List")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [Route("Add")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders =
               Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }


        [HttpPost("Add")]
        public IActionResult Create(Student s, IFormFile ImagePath)

        {
            if (ImagePath != null)
            {
                var fileName = ImagePath.FileName;
                fileName = Path.GetFileName(fileName);
                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Image", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                ImagePath.CopyToAsync(stream);
                ViewBag.Message = "Image Uplload Successfully";

                s.Image = "/Image/" + fileName;
            }
            else
            {
                s.Image = s.Gender == Gender.Male ? "/Image/user_male.jpg" : "/Image/user_female.jpg";
            }
            s.Id = ++id;
            listStudents.Add(s);

            return View("Index", listStudents);
        }
    }
}
