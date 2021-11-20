using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CollectorzCorner.Utilities;

namespace CollectorzCorner.Models
{
    public class SearchModel
    {
        public string Keyword { get; set; }
        public string TypeID { get; set; }

        /*
        //Generic items
        public string UserName {get; set;}
        public string Title {get; set;}
        public string ReleaseDate {get; set;}
        public string SeriesID {get; set;}   
        public string TypeID {get; set;}
        public string Publisher {get; set;}
        
        //specific collection search
        public string Collection {get; set;}

        //Movies
        public string MovieDirector {get; set;}
        public string MovieWrite {get; set;}
        public string MoveProducer {get; set;}
        public string MovieStudio {get; set;}
        
        //ComicBooks
        public string ComicWriter { get; set; }
        public string ComicArtist { get; set; }
        public string ComicColorist {get; set;}
        public string ComicCoverArtrist {get; set;}
        public string ComicEditor {get; set;}
        
        //Books
        public string BookAuthor {get; set;}
        public string BookIllustrator {get; set;}
        public string BookEditor {get; set;}
        public string BookISBN {get; set;}

        //Games
        public string GameDeveloper {get; set;}
        public string GameContentRating {get; set;}
        public string GamePlatform {get; set;}*/

    }

    public class SearchResultsModel
    {
        public List<Item> ResultList { get; private set; }

        public SearchResultsModel() {}

        public SearchResultsModel(List<Item> results)
        {
            this.ResultList = results;
        }
    }

    public class AddItemToDB_LoadingModel
    {
        public AddItemToDB_LoadingModel()
        {
            this.Types = DBUtility.GetItemTypes();
        }

        public Dictionary<string, string> Types { get; private set; }
        
    } 

   // TODO: MOVE THIS TO ITEMMODEL EVENTUALLY
    public class AddItemToDatabaseModel
    {
        public AddItemToDatabaseModel()
        {
            this.loadingModel = new AddItemToDB_LoadingModel();
            this.itemModel = new AddItemToDB();
            this.comicModel = new AddComicBookToDB();
            this.bookModel = new AddBookToDB();
            this.gameModel = new AddGameToDB();
            this.movieModel = new AddMovieToDB();
        }

        public AddItemToDB_LoadingModel loadingModel { get; private set; }
        public AddItemToDB itemModel { get; set; }
        public AddComicBookToDB comicModel { get; set; }
        public AddBookToDB bookModel { get; set; }
        public AddGameToDB gameModel { get; set; }
        public AddMovieToDB movieModel { get; set; }
    }

    // TODO: MOVE THIS TO ITEMMODEL EVENTUALLY
    public class AddItemToDB
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Release Date")]
        public string ReleaseDate { get; set; }

        [Required]
        public string TypeID { get; set; }

        [Display(Name="Publishing Company")]
        public string Publisher { get; set; }

        public string Genres { get; set; } // Will need to send back json or something
        
        public int SeriesID { get; set; }

        [Display(Name = "Series Name *")]
        public string SeriesName { get; set; }
    }

    // TODO: MOVE THIS TO ITEMMODEL EVENTUALLY
    public class AddComicBookToDB
    {
        [Required]
        public string Writer { get; set; }

        [Required]
        public string Artist { get; set; }

        public string Colorist { get; set; }

        [Display(Name="Cover Artist")]
        public string CoverArtist { get; set; }

        public string Editor { get; set; }
    }

    // TODO: MOVE THIS TO ITEMMODEL EVENTUALLY
    public class AddBookToDB
    {
        [Required]
        public string Author { get; set; }

        public string Illustrator { get; set; }

        public string Editor { get; set; }

        public string Plot { get; set; }

        [Display(Name = "Page Count")]
        [Range (0, 100000, ErrorMessage="Only numbers are valid.")]
        public string NoOfPages { get; set; }

        [Display(Name = "Reading Level")]
        public string ReadLevel { get; set; }

        public string ISBN { get; set; }

        [Display(Name = "Yes, this is an E-Book")]
        public bool IsEBook { get; set; }
    }

    // TODO: MOVE THIS TO ITEMMODEL EVENTUALLY
    public class AddMovieToDB
    {
        [Required]
        public string Director { get; set; }

        public string Writer { get; set; }

        public string Producer { get; set; }

        public string Format { get; set; }

        public string ContentRatingID { get; set; }
    }

    // TODO: MOVE THIS TO ITEMMODEL EVENTUALLY
    public class AddGameToDB
    {
        [Required]
        public string Developer { get; set; }

        public string ContentRatingID { get; set; }

        [Required]
        public int PlatformID { get; set; }
    }


}