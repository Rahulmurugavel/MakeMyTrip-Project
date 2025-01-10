using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RahulCrud.Data;
using RahulCrud.Models;
using RahulCrud.Models.Entities;

namespace RahulCrud.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDdContext dbContext;

        public StudentsController(ApplicationDdContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student()
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Location= viewModel.Location,
                Subscription= viewModel.Subscription

            };
            await dbContext.students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.students.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student=await dbContext.students.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student=await dbContext.students.FindAsync(viewModel.Id);
            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Phone = viewModel.Phone;
                student.Email = viewModel.Email;
                student.Location = viewModel.Location;
                student.Subscription = viewModel.Subscription;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.students
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id==viewModel.Id);
            if(student is not null)
            {
                dbContext.students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
