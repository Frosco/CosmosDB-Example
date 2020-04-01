using CosmosExample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosExample.Services
{
	public interface IBookRepository
	{
		Task<List<Book>> GetAllBooks();
		Task<Book> GetBook(string isbn);
		Task CreateBook(Book book);
		Task UpdateBook(string isbn, Book book);
		Task Delete(string isbn);
	}
}
