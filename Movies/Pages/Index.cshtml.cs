using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {

        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }
        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax)
        {
            // Nullable conversion workaround
            /*
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            */

            Movies = MovieDatabase.All;
            //search movie titles for the SearchTerms
            if(SearchTerms != null)
            {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
            }
            //Filter by MPAA rating
            if(MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }
            //Filter by Genres
            if(Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }
            //filter by price neither not null
            if(IMDBMax != null && IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax && movie.IMDBRating >= IMDBMin);
            }
            //Filter by Price max not null
            else if(IMDBMax != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
            }
            //filter by price min not null
            else if(IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMin);
            }

        }

    }
}
