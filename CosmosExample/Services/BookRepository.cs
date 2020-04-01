using CosmosExample.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosExample.Services
{
    public class BookRepository : IBookRepository
	{
		private readonly string databaseName = "Logistics";
		private readonly string collectionName = "Books";
		public async Task<List<Book>> GetAllBooks()
		{
			using var client = await GetClient();

			FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

			var query =  client.CreateDocumentQuery<Book>(
					UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
				.AsDocumentQuery();
			var result = new List<Book>();

			while(query.HasMoreResults)
			{
				foreach (Book b in await query.ExecuteNextAsync<Book>())
				{
					result.Add(b);
				}
			}

			return result;
		}
		public async Task<Book> GetBook(string isbn)
		{
			using var client = await GetClient();

			FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

			return client.CreateDocumentQuery<Book>(
					UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
				.Where(b => b.Isbn == isbn)
				.FirstOrDefault();
		}

		public async Task CreateBook(Book book)
		{
			using var client = await GetClient();

			FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

			var existingBook = client.CreateDocumentQuery<Book>(
					UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
				.Where(b => b.Isbn == book.Isbn)
				.ToList()
				.FirstOrDefault();

			if (existingBook == null)
			{
				await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), book);
			}			
		}

		public Task UpdateBook(string isbn, Book book)
		{
				return null;
		}

		public Task Delete(string isbn)
		{
			return null;
		}

		private async Task<DocumentClient> GetClient()
		{
			var accountEndpoint = "some endpoint";
			var accountKey = "some account key";

			var client = new DocumentClient(new Uri(accountEndpoint), accountKey);

			await client.CreateDatabaseIfNotExistsAsync(new Database { Id = "Logistics" });
			await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("Logistics"), new DocumentCollection { Id = "Books" });
			return client;
		}
	}
}
