// See https://aka.ms/new-console-template for more information
using PublisherData;
using PublisherDomain;
using Microsoft.EntityFrameworkCore;

PubContext _context = new PubContext();

// ConnectingExistingArtistAndCoverObjects();

void ConnectingExistingArtistAndCoverObjects()
{
    var artistA = _context.Artists.Find(1);
    var artistB = _context.Artists.Find(2);
    var coverA = _context.Covers.Find(1);
    coverA.Artists.Add(artistA);
    coverA.Artists.Add(artistB);
    _context.SaveChanges();
}

// CreateNewCoverWithExistingArtist();

void CreateNewCoverWithExistingArtist()
{
    var artistA = _context.Artists.Find(1);
    var cover = new Cover { DesignIdeas = "Author has provided a photo" };
    cover.Artists.Add(artistA);
    _context.Covers.Add(cover); // this can be omitted sinced DbContext is tracking Artist;
    _context.SaveChanges();
}

// CreateNewCoverAndArtistTogether();

void CreateNewCoverAndArtistTogether()
{
    var newArtist = new Artist { FirstName = "Kir", LastName = "Talmage" };
    var newCover = new Cover { DesignIdeas = "We like birds!" };
    newArtist.Covers.Add(newCover);
    _context.Artists.Add(newArtist);
    _context.SaveChanges();
}

// RetrieveAnArtistWithTheirCovers();
void RetrieveAnArtistWithTheirCovers()
{
    var artistWithCovers = _context.Artists.Include(a => a.Covers)
                            .FirstOrDefault(a => a.ArtistId == 1);
}

// RetrieveACoverWithItsArtists();
void RetrieveACoverWithItsArtists()
{
    var coverWithArtists = _context.Covers.Include(c => c.Artists)
                            .FirstOrDefault(c => c.CoverId == 1);
}

// UnAssignAnArtistFromACover();
// void UnAssignAnArtistFromACover()
// {
//   var coverwithartist = _context.Covers
//       .Include(c => c.Artists.Where(a => a.ArtistId == 1))
//       .FirstOrDefault(c => c.CoverId == 1);
//   coverwithartist.Artists.RemoveAt(0);
//   _context.ChangeTracker.DetectChanges();
//   var debugview = _context.ChangeTracker.DebugView.ShortView;
//   _context.SaveChanges();
// }

// ReassignACover();

void ReassignACover()
{
    var coverwithartist4 = _context.Covers
    .Include(c => c.Artists.Where(a => a.ArtistId == 4))
    .FirstOrDefault(c => c.CoverId == 5);

    coverwithartist4.Artists.RemoveAt(0);
    var artist3 = _context.Artists.Find(3);
    coverwithartist4.Artists.Add(artist3);
    _context.ChangeTracker.DetectChanges();
}

//GetAllBooksWithTheirCovers();

void GetAllBooksWithTheirCovers()
{
    var booksAndCovers = _context.Books.Include(b => b.Cover).ToList();
    booksAndCovers.ForEach(book =>
    {
        Console.WriteLine(book.Title + (book.Cover == null ? ": No cover" : ": " +  book.Cover.DesignIdeas));
    });
}

//MultiLevelInclude();
void MultiLevelInclude()
{
    var authorGraph = _context.Authors.AsNoTracking()
        .Include(a => a.Books)
        .ThenInclude(b => b.Cover)
        .ThenInclude(c => c.Artists)
        .FirstOrDefault(a => a.AuthorId == 2);

    Console.WriteLine(authorGraph?.FirstName + " " + authorGraph?.LastName);
    foreach (var book in authorGraph.Books)
    {
        Console.WriteLine("Book:" + book.Title);
        if (book.Cover != null)
        {
            Console.WriteLine("Design Ideas: " + book.Cover.DesignIdeas);
            Console.Write("Artist(s):");
            book.Cover.Artists.ForEach(a => Console.Write(a.LastName + " "));

        }
    }
};

MoveCoverFromOneBookToAnother();

void MoveCoverFromOneBookToAnother()
{
    var cover = _context.Covers.Include(c => c.Book).FirstOrDefault(c => c.CoverId == 1);
    var newBook = _context.Books.Find(5);
    cover.Book = newBook;
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
}
