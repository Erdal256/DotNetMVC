using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _036_MoviesMvcBilgeAdam.Services
{
    //AutoMaper: Bir class tipindeki (Review) bir objeyi baika bir class tipine 
    //(ReviewModel) hiç özelliklerini assing etmeden(Id = r.Id, Content = r.Content, Date = r.Date...) 7
    //tek bir configurasyon yaparak obje dönüştürme işlemi yapan kütüphane.  http://automapper.org/

    public class ReviewService
    {
        private readonly MoviesContext _db;

        public ReviewService(MoviesContext db)
        {
            _db = db;
        }
        public IQueryable<ReviewModel> GetQuery()
        {
            try
            {
                return _db.Reviews.OrderBy(r => r.Movie.Name).Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    Content = r.Content,
                    Date = r.Date,
                    Reviewer = r.Reviewer,
                    Rating = r.Rating,
                    MovieId = r.MovieId,
                    Movie = new MovieModel()
                    {
                        Id = r.Movie.Id,
                        Name = r.Movie.Name,
                        ProductionYear = r.Movie.ProductionYear,
                        BoxOfficeReturn = r.Movie.BoxOfficeReturn,
                        Directors = r.Movie.MovieDirectors.Select(md => new DirectorModel()
                        {
                            Id = md.Director.Id,
                            Name = md.Director.Name,
                            Surname = md.Director.Surname,
                            Retired = md.Director.Retired
                        }).ToList()
                    }
                });
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public void FillAllRating(ReviewModel review)
        {
            review.AllRating = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                review.AllRating.Add(i);
            }
        }
    }
}