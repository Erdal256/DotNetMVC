using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetMvc.Contexts;
using DotNetMvc.Models;
using DotNetMvc.Services;

namespace DotNetMvc.Controllers
{
    public class MoviesController : Controller
    {
        private readonly  MoviesContext _db;
        private readonly MovieService _movieService;
        // GET: Movies
        public MoviesController()
        {
            _db = new MoviesContext();
            _movieService = new MovieService(_db);
        }
        public ActionResult Index()
        {
            var movies = _db.Movies.ToList();
            return View(movies);
        }

        //GET: Movies/List
        public ActionResult List()
        {
            try
            {
                List<MovieModel> model = _movieService.GetQuery().ToList();
                return View("MovieList", model);
            }
            catch (Exception exc)
            {

                return View("_Exception");
            }
           
        }
    }
}