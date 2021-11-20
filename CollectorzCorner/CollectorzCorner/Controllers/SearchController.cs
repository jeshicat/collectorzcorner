using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectorzCorner.Utilities;
using CollectorzCorner.Models;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.Helpers;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace CollectorzCorner.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/ General search function
        //send the model
        //funky sql stuff goes here


        public ActionResult Index()
        {
            ViewData["CollectionTypes"] = DBUtility.GetItemTypes();
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            if (!ModelState.IsValid) return View("Index");
            List<Item> results;

            if (model.Keyword == null && model.TypeID.Equals("ALL")) // Keyword null, TypeID == ALL
                results = SearchUtility.SearchAll();
            else if (model.Keyword == null && !model.TypeID.Equals("ALL")) // Keyword null, TypeID != ALL
                results = SearchUtility.SearchByTypeID(model.TypeID);
            else if (model.TypeID.Equals("ALL")) // Keyword != null, TypeID == ALL
                results = SearchUtility.SearchByKeyword(model.Keyword.ToLower());
            else // Keyword != null, TypeID != ALL
                results = SearchUtility.SearchByKeywordAndTypeID(model.Keyword.ToLower(), model.TypeID);

            return PartialView("SearchResults_Partial", new SearchResultsModel(results));
            // Send back partial view 
            // Partial can have its own model
        }
        // GET: /Search/AddItem
        [Authorize]
        public ActionResult AddItem()
        {
            AddItemToDatabaseModel model = new AddItemToDatabaseModel();
            return View(model);
        }

        // TODO: Make series an autocomplete box? Bootstrap
        // TODO: Check for required fields on the partial classes
        [HttpPost]
        [Authorize]
        public ActionResult AddItem(AddItemToDatabaseModel model, HttpPostedFileBase file)
        {
            int id = 0;
            var user = Utility.GetUserToken();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Debug.Write(error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return RedirectToAction("AddItem");

            using (CCEntities db = new CCEntities())
            {
                // For later, to stop duplicates with same tite, typeid and series (ask guys about this)
                //var item = DBUtility.FindItemByTitleTypeIDAndSeriesID(model.itemModel.Title, model.itemModel.TypeID,model.itemModel.SeriesID);
                
                Item newItem = new Item();
                newItem.Title = model.itemModel.Title;
                newItem.Description = model.itemModel.Description;
                if(model.itemModel.ReleaseDate != null)
                    newItem.ReleaseDate = DateTime.Parse(model.itemModel.ReleaseDate);
                newItem.Publisher = model.itemModel.Publisher;
                newItem.TypeID = model.itemModel.TypeID;
                newItem.UserID = user.UserId;
                if (model.itemModel.SeriesID == -1)
                {
                    var series = DBUtility.FindSeriesByNameAndType(model.itemModel.SeriesName, model.itemModel.TypeID);
                    if (series != null)
                    {
                        newItem.SeriesID = series.ID;
                    }
                    else
                    { 
                        Series newSeries = new Series();
                        newSeries.Name = model.itemModel.SeriesName;
                        db.Series.AddObject(newSeries);
                        db.SaveChanges();
                        newItem.SeriesID = newSeries.ID;
                    }
                }
                else
                    newItem.SeriesID = null;

                /* ---- Makes image to byte[] ---- */
                if (file != null && file.ContentLength > 0)
                {
                    byte[] data;
                    using (Stream inputStream = file.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();

                        // Saves byte array to the dbase
                        // TODO:// When the server works, change to save on location on server, and save the href in dbase
                        Picture pic = new Picture();
                        pic.pictureBytes = data;
                        db.AddToPictures(pic);
                        newItem.PictureID = pic.ID;
                    }
                }
                // Add Genres to ItemGenre table
                
                String genreIdString = model.itemModel.Genres;
                if (genreIdString != null)
                {
                    var genreIdArray = genreIdString.Split(';');
                    foreach (var genreId in genreIdArray)
                    {
                        Genre genre = DBUtility.GetGenreFromID(Int32.Parse(genreId));
                        db.Genres.Attach(genre);
                        newItem.Genres.Add(genre);
                    }
                }
                
                db.Items.AddObject(newItem);
                db.SaveChanges(); // Need to do this to get itemID
                id = newItem.ID;
                try
                {
                    switch (model.itemModel.TypeID)
                    {
                        case "COM":
                            ComicBook comic = new ComicBook();
                            comic.ItemID = newItem.ID;
                            comic.Writer = model.comicModel.Writer;
                            comic.Artist = model.comicModel.Artist;
                            comic.Colorist = model.comicModel.Colorist;
                            comic.CoverArtist = model.comicModel.CoverArtist;
                            comic.Editor = model.comicModel.Editor;
                            db.ComicBooks.AddObject(comic);
                            break;
                        case "BOO":
                            Book book = new Book();
                            book.ItemID = newItem.ID;
                            book.Author = model.bookModel.Author;
                            book.Editor = model.bookModel.Editor;
                            book.Illustrator = model.bookModel.Illustrator;
                            book.ISBN = model.bookModel.ISBN;
                            book.IsEBook = model.bookModel.IsEBook;
                            book.Plot = model.bookModel.Plot;
                            book.ReadLevel = model.bookModel.ReadLevel;
                            if (model.bookModel.NoOfPages != null)
                                book.NoOfPages = Int32.Parse(model.bookModel.NoOfPages);
                            db.Books.AddObject(book);
                            break;
                        case "MOV":
                            Movie movie = new Movie();
                            movie.ItemID = newItem.ID;
                            movie.ContentRatingID = model.movieModel.ContentRatingID;
                            movie.Director = model.movieModel.Director;
                            movie.Format = model.movieModel.Format;
                            movie.Producer = model.movieModel.Producer;
                            movie.Writer = model.movieModel.Writer;
                            db.Movies.AddObject(movie);
                            break;
                        case "GAM":
                            Game game = new Game();
                            game.ItemID = newItem.ID;
                            game.ContentRatingID = model.gameModel.ContentRatingID;
                            game.Developer = model.gameModel.Developer;
                            game.PlatformID = model.gameModel.PlatformID;
                            db.Games.AddObject(game);
                            break;
                        default:
                            db.DeleteObject(newItem); //If it get here, theres something wrong - delete the item object.
                            break;
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.DeleteObject(newItem);
                    throw ex;
                }
            }
            if (id > 0)
                return RedirectToAction("ViewItem", new { id = id });
            else
                return RedirectToAction("AddItem");
        }

        public ActionResult SendItemPartial(string TypeID)
        {
            switch (TypeID)
            {
                case "COM":
                    return PartialView("AddComicBook_Partial");
                case "BOO":
                    return PartialView("AddBook_Partial");
                case "GAM":
                    return PartialView("AddGame_Partial");
                case "MOV":
                    return PartialView("AddMovie_Partial");
                default:
                    return null;
            }
        }

        public string PopulateSeriesList(string typeID)
        {
            var series = DBUtility.FindAllSeriesForItemType(typeID);
            var JSON = new JavaScriptSerializer();
            string serialized = JSON.Serialize(series);
            return serialized; 
        }

        public string PopulateGenres(string typeID)
        {
            var genres = DBUtility.GetGenreListByType(typeID);
            var JSON = new JavaScriptSerializer();
            string serialized = JSON.Serialize(genres);
            return serialized;
        }

        public string PopulateRatingList(string typeID)
        {
            var ratings = DBUtility.GetContentRatingList(typeID);
            var JSON = new JavaScriptSerializer();
            string serialized = JSON.Serialize(ratings);
            return serialized;
        }

        public string PopulatePlatformList()
        {
            var platforms = DBUtility.GetPlatformList();
            var JSON = new JavaScriptSerializer();
            string serialized = JSON.Serialize(platforms);
            return serialized;
        }

        // Get: /Search/ViewItem
        public ActionResult ViewItem(int id)
        {
            ViewData["PreviousURL"] = Request.UrlReferrer.ToString();
            ItemModel model = new ItemModel(id);
            if (model.Item != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }

        public FileContentResult DisplayImage(int id)
        {
            byte[] imgByteArray = DBUtility.GetPictureByID(id);
            if (imgByteArray != null)
            {
                return new FileContentResult(imgByteArray, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
        public FileContentResult DisplayThumbImageForItemID(int ItemID, int width, int height)
        {
            var item = DBUtility.GetItemByID(ItemID);
            if (item.PictureID != null)
            {
                var picture = DBUtility.GetPictureByID(item.PictureID);
                byte[] imgByteArray = DBUtility.GetPictureByID(item.PictureID);

                if (imgByteArray != null)
                {
                    WebImage wImage = new WebImage(imgByteArray);
                    wImage.Resize(width, height);
                    // Extract byte array.
                    var image = wImage.GetBytes("image/jpeg");

                    // Return byte array as jpeg.
                    return new FileContentResult(image, "image/jpeg");
                }
            }
            return null;
        }

        public FileContentResult DisplayThumbImage(int id, int width, int height)
        {
            byte[] imgByteArray = DBUtility.GetPictureByID(id);

            if (imgByteArray != null)
            {
                WebImage wImage = new WebImage(imgByteArray);
                wImage.Resize(width, height);
                // Extract byte array.
                var image = wImage.GetBytes("image/jpeg");

                // Return byte array as jpeg.
                return new FileContentResult(image, "image/jpeg");
            }
            return null;
        }

        public string GenerateItemToolTip(int id)
        {
            var items = DBUtility.FindItemRatingsAverage(id);
            return "";
        }

        public string GetPictureID(int id)
        {
            var item = DBUtility.GetItemByID(id);
            return item.PictureID.ToString(); ;
        }
    }
}
