using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectorzCorner.Utilities;
using System.Web.Script.Serialization;

namespace CollectorzCorner.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public ActionResult Index()
        {
            return View();
        }

        public String GetSeriesName(int seriesID)
        {
            var series = DBUtility.FindSeriesById(seriesID);
            return series.Name;
        }

        public String GetTypeName(string typeID)
        {
            var type = DBUtility.GetItemTypeById(typeID);
            return type.Name;
        }

        public ActionResult SendItemPartial(int ItemID, string TypeID)
        {
            switch (TypeID)
            {
                case "COM":
                    return PartialView("ComicBook_Partial", DBUtility.GetComicBookByID(ItemID));
                case "BOO":
                    return PartialView("Book_Partial", DBUtility.GetBookByID(ItemID));
                case "GAM":
                    return PartialView("Game_Partial", DBUtility.GetGameByID(ItemID));
                case "MOV":
                    return PartialView("Movie_Partial", DBUtility.GetMovieByID(ItemID));
                default:
                    return null;
            }
        }

        public string GetPlatform(int PlatformID)
        {
            return DBUtility.GetPlatformName(PlatformID);
            
        }

        public string GetContentRating(string RatingID)
        {
            return DBUtility.GetRatingName(RatingID);
        }

        public string GetItemGenres(int ItemID)
        {
            var genres = DBUtility.GetItemGenresByID(ItemID);
            var JSON = new JavaScriptSerializer();
            string serialized = JSON.Serialize(genres);
            return serialized;
        }

        [Authorize]
        public string GetUserCollectionsByType(string TypeID)
        {
            Dictionary<string, string> collectionDict = new Dictionary<string, string>();
            var user = Utility.GetUserToken();
            if(user != null)
            {
                var collections = DBUtility.FindCollectionsForSpecificUserAndType(user.UserId, TypeID);
                foreach (var collection in collections)
                {
                    collectionDict.Add(collection.ID.ToString(), collection.Name);
                }
                var JSON = new JavaScriptSerializer();
                string serialized = JSON.Serialize(collectionDict);
                return serialized;
            }
            return null;
        }

        [Authorize]
        public string AddItemToCollection(int ItemID, int CollectionID, string CollectionName, string TypeID)
        {
            var user = Utility.GetUserToken(); // For completeness, to make sure the collection belongs to that user.
            var status = DBUtility.AddItemToCollection(ItemID, CollectionID, user.UserId, CollectionName, TypeID);
            return status;
        }
    }
}
