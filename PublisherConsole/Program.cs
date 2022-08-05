// See https://aka.ms/new-console-template for more information
using PublisherData;
using PublisherDomain;
using Microsoft.EntityFrameworkCore;

PubContext _context = new PubContext();

// InsertNewAuthorWithNewBook();

void InsertNewAuthorWithNewBook()
{
  var author = new Author() { FirstName = "Lynda", LastName = "Rutledge" };
  author.Books.Add(new Book
  {
    Title = "West With Giraffes",
    PublishDate = new DateTime(2021, 2, 1)
  });
  _context.Authors.Add(author);
  _context.SaveChanges();
}

EagerLoadBooksWithAuthors();

void EagerLoadBooksWithAuthors()
{
  var authors = _context.Authors.Include(a => a.Books
                                .Where(b => b.PublishDate >= new DateTime(1989, 1, 1))
                                .OrderBy(b => b.Title)
  ).ToList();

  authors.ForEach(a =>
  {
    Console.WriteLine($"{a.LastName} ({a.Books.Count})");
    a.Books.ForEach(b => Console.WriteLine("\t" + b.Title));
  });
}
// ModifyingRelatedDataWhenTraked();

void ModifyingRelatedDataWhenTraked()
{
  var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == 1);
  // author.Books[0].BasePrice = (decimal)10.00;
  // author.Books.Remove(author.Books[1]);
  _context.ChangeTracker.DetectChanges();
  var state = _context.ChangeTracker.DebugView.ShortView;
}

ModifyingRelatedDataWhenNotTracked();

void ModifyingRelatedDataWhenNotTracked()
{
  var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == 1);
  author.Books[0].BasePrice = (decimal)10.00;


  // simulate disconnected application
  var newContext = new PubContext();
  newContext.Books.Update(author.Books[0]);
  var state = newContext.ChangeTracker.DebugView.ShortView;
}