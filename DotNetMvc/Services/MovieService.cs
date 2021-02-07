using DotNetMvc.Contexts;
using DotNetMvc.Models;
using System.Linq;

namespace DotNetMvc.Services
{
    public class MovieService
    {
        //CRUD: Create, Read, Updatei Delete
        //private MovideContext _db = new MoviesContext(); // _db objesi bu class'ta new'leyip kullanmak yerine Depencdency Injection üzerinden dışarıdan alıp kullanmak daha iyi bir yöntem.
        private readonly MoviesContext _db;
        public MovieService(MoviesContext db)
        {
            _db = db;
        }
        public IQueryable<MovieModel> GetQuery()
        {
            try
            {
                return _db.Movies.OrderBy(m => m.Name).Select(m => new MovieModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    BoxOfficeReturn = m.BoxOfficeReturn,
                    ProductionYear = m.ProductionYear,
                    Directors = m.MovieDirectors.Select(md => new DirectorModel()
                    {
                        Id = md.Director.Id,
                        Name = md.Director.Name,
                        Surname = md.Director.Surname,
                        Retired = md.Director.Retired
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewModel()
                    {
                        Id = r.Id,
                        Content = r.Content,
                        Rating = r.Rating,
                        Reviewer = r.Reviewer,
                        MovieId = r.MovieId
                    }).ToList()
                });

            }
            catch (Exception exc)
            {

                throw;
            }
            
        }
    }
}