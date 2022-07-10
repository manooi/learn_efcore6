// See https://aka.ms/new-console-template for more information
using PublisherData;
using PublisherDomain;
using Microsoft.EntityFrameworkCore;

using (PubContext context = new PubContext())
{
  context.Database.EnsureCreated();
}

// AddAuthor();
// GetAuthors();
// AddAuthorWithBook();
GetAuthorsWithBooks();


void AddAuthor()
{
  var author = new Author { FirstName = "Joise", LastName = "New" };
  using var context = new PubContext();
  context.Authors.Add(author);
  context.SaveChanges();
}

void AddAuthorWithBook()
{
  var author = new Author { FirstName = "Julie", LastName = "Lerman" };
  author.Books.Add(new Book { Title = "Programming EF", PublishDate = new DateTime(2009, 1, 1) });
  author.Books.Add(new Book { Title = "Programming EF 2nd ED", PublishDate = new DateTime(2010, 8, 1) });
  using var context = new PubContext();
  context.Authors.Add(author);
  context.SaveChanges();
}

void GetAuthors()
{
  using var context = new PubContext();
  var authors = context.Authors.ToList();
  foreach (var author in authors)
  {
    Console.WriteLine(author.FirstName + " " + author.LastName);
  }
}

void GetAuthorsWithBooks()
{
  using var context = new PubContext();
  var authors = context.Authors.Include(a => a.Books).ToList();
  foreach (var author in authors)
  {
    Console.WriteLine(author.FirstName + " " + author.LastName);
    foreach (var book in author.Books)
    {
        Console.WriteLine("\t" + book.Title);
    }
  }
}