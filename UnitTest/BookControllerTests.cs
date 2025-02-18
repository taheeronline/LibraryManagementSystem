using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagementSystem.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _mockBookService;
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mockBookService = new Mock<IBookService>();
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new BookController(_mockBookService.Object, _mockCategoryService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfBooks()
        {
            // Arrange
            var books = new List<Book> { new Book { Id = 1, Title = "Test Book" } };
            _mockBookService.Setup(service => service.GetAllBooksAsync()).ReturnsAsync(books);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenBookNotFound()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsViewResult_WithCategories()
        {
            // Arrange
            var categories = new List<Category> { new Category { Id = 1, Name = "Test Category" } };
            _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewBagCategories = Assert.IsAssignableFrom<List<SelectListItem>>(((dynamic)viewResult).ViewBag.Categories);
            Assert.Single(viewBagCategories);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };
            _mockBookService.Setup(service => service.AddBookAsync(book)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(book);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");
            var book = new Book { Id = 1, Title = "Test Book" };

            // Act
            var result = await _controller.Create(book);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(book, model);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenBookNotFound()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };
            _mockBookService.Setup(service => service.UpdateBookAsync(book)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Edit(1, book);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Post_ReturnsNotFound_WhenIdMismatch()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };

            // Act
            var result = await _controller.Edit(2, book);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");
            var book = new Book { Id = 1, Title = "Test Book" };

            // Act
            var result = await _controller.Edit(1, book);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(book, model);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book" };
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenBookNotFound()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            // Arrange
            _mockBookService.Setup(service => service.DeleteBookAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
