using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LibraryManagementSystem.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;

        public LoanController(ILoanService loanService, IBookService bookService, IMemberService memberService)
        {
            _loanService = loanService;
            _bookService = bookService;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return View(loans);
        }

        public async Task<IActionResult> Create()
        {
            // Fetch books from the database
            var books = await _bookService.GetAllBooksAsync(); // Replace with your service method

            // Convert books to SelectListItems
            ViewBag.Books = books.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(), // The value for the dropdown option
                Text = b.Title           // The display text for the dropdown option
            }).ToList();

            // Fetch members for the dropdown
            var members = await _memberService.GetAllMembersAsync(); // Replace with your service method
            ViewBag.Members = members.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                await _loanService.AddLoanAsync(loan);
                return RedirectToAction(nameof(Index));
            }
            var books = await _bookService.GetAllBooksAsync();
            // Convert books to SelectListItems
            ViewBag.Books = books.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // The value for the dropdown option
                Text = c.Title           // The display text for the dropdown option
            }).ToList();

            var members = await _memberService.GetAllMembersAsync();
            // Convert books to SelectListItems
            ViewBag.Members = members.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // The value for the dropdown option
                Text = c.Name           // The display text for the dropdown option
            }).ToList();
            return View(loan);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            var books = await _bookService.GetAllBooksAsync();
            // Convert books to SelectListItems
            ViewBag.Books = books.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // The value for the dropdown option
                Text = c.Title           // The display text for the dropdown option
            }).ToList();

            var members = await _memberService.GetAllMembersAsync();
            // Convert books to SelectListItems
            ViewBag.Members = members.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // The value for the dropdown option
                Text = c.Name           // The display text for the dropdown option
            }).ToList();

            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Loan loan)
        {
            if (ModelState.IsValid)
            {
                await _loanService.UpdateLoanAsync(loan);
                return RedirectToAction(nameof(Index));
            }
            var books = await _bookService.GetAllBooksAsync();
            // Convert books to SelectListItems
            ViewBag.Books = books.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // The value for the dropdown option
                Text = c.Title           // The display text for the dropdown option
            }).ToList();

            var members = await _memberService.GetAllMembersAsync();
            // Convert books to SelectListItems
            ViewBag.Members = members.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // The value for the dropdown option
                Text = c.Name           // The display text for the dropdown option
            }).ToList();
            return View(loan);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _loanService.DeleteLoanAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
