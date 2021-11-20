using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollectorzCorner.Utilities
{
    public partial class DBUtility
    {
        /// <summary>
        /// Gets a dictionary of item types.
        /// </summary>
        /// <returns>A dictionary of item types.</returns>
        public static Dictionary<string, string> GetItemTypes()
        {
            using (CCEntities db = new CCEntities())
            {
                var typeDict = (from row in db.ItemTypes
                                select new
                                {
                                    row.ID,
                                    row.Name
                                }).ToDictionary(x => x.ID, x => x.Name);

                return typeDict;
            }
        }
        
        /// <summary>
        /// Finds an item type.
        /// </summary>
        /// <param name="typeId">ID of item type.</param>
        /// <returns>An item type of specified ID.</returns>
        public static ItemType GetItemTypeById(string typeId)
        {
            using (CCEntities db = new CCEntities())
            {
                var type = (from t in db.ItemTypes
                            where t.ID == typeId
                            select t).Single();

                return type;
            }
        }

        /// <summary>
        /// Finds a user.
        /// </summary>
        /// <param name="username">Username of user.</param>
        /// <returns>A user of specified username, or null.</returns>
        public static User FindUserByUsername(string username)
        {
            using (CCEntities db = new CCEntities())
            {
                User user = (from s in db.Users
                             where s.Username == username
                             select s).SingleOrDefault();

                return user;
            }
        }

        /// <summary>
        /// Finds a user from userID.
        /// </summary>
        /// <param name="userId">Id of user.</param>
        /// <returns>A user of specified ID or null.</returns>
        public static User FindUserByUserId(int userId)
        {
            using (CCEntities db = new CCEntities())
            {
                User user = (from s in db.Users
                             where s.ID == userId
                             select s).SingleOrDefault();

                return user;
            }
        }

        /// <summary>
        /// Finds all collections for a specific user.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        /// <returns>A list of collections for specified user.</returns>
        public static List<Collection> FindCollectionsForSpecificUser(int userId)
        {
            using (CCEntities db = new CCEntities())
            {
                var collections = (from c in db.Collections
                                   where c.UserID == userId
                                   select c).ToList<Collection>();

                return collections;
            }
        }

        /// <summary>
        /// Finds collections for a specific user and type.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="typeId">Type of collection.</param>
        /// <returns>A list of collections for the specified user and type of collection.</returns>
        public static List<Collection> FindCollectionsForSpecificUserAndType(int userId, string typeId)
        {
            using (CCEntities db = new CCEntities())
            {
                var collections = (from c in db.Collections
                                   where c.UserID == userId
                                   && c.TypeID == typeId
                                   select c).ToList<Collection>();

                return collections;
            }
        }

        /// <summary>
        /// Finds a list of all series.
        /// </summary>
        /// <returns>A list of all series.</returns>
        public static List<Series> FindAllSeries()
        {
            using (CCEntities db = new CCEntities())
            {
                var series = (from s in db.Series
                              select s).ToList();

                return series;
            }
        }

        /// <summary>
        /// Finds a single series.
        /// </summary>
        /// <param name="seriesId">Id of series.</param>
        /// <returns>A series object of specified ID.</returns>
        public static Series FindSeriesById(int seriesId)
        {
            using (CCEntities db = new CCEntities())
            {
                var series = (from s in db.Series
                              where s.ID == seriesId
                              select s).Single();

                return series;
            }
        }

        /// <summary>
        /// Created this class, so I could return just the id and name of a series object.
        /// </summary>
        public class SeriesObj{
            public int id;
            public string name;
        }

        /// <summary>
        /// Gets a list of Series from the database.
        /// </summary>
        /// <param name="typeID">Type of item.</param>
        /// <returns>A list of SeriesObj for a certain type of item.</returns>
        public static List<SeriesObj> FindAllSeriesForItemType(string typeID)
        {
            List<SeriesObj> series;
            using (CCEntities db = new CCEntities())
            {
                series = (from s in db.Series
                          from i in db.Items
                          where s.ID == i.SeriesID
                          && i.TypeID == typeID
                          select new SeriesObj { id = s.ID, name = s.Name }).Distinct().ToList<SeriesObj>();

                return series;
            }
        }

        /// <summary>
        /// Finds a series from the database.
        /// </summary>
        /// <param name="name">Name of series.</param>
        /// <param name="typeID">Type of item.</param>
        /// <returns>A series of specified name and item type, or null.</returns>
        public static Series FindSeriesByNameAndType(string name, string typeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var series = (from s in db.Series
                              from i in db.Items
                              where s.ID == i.SeriesID
                              && i.TypeID == typeID
                              && s.Name == name
                              select s).Distinct().SingleOrDefault();
                return series;
            }
        }

        /// <summary>
        /// Finds an item from the database.
        /// </summary>
        /// <param name="title">Title of item.</param>
        /// <param name="typeID">Type of item.</param>
        /// <param name="seriesID">Series ID of item.</param>
        /// <returns>An item that matches the specified parameters or null.</returns>
        internal static object FindItemByTitleTypeIDAndSeriesID(string title, string typeID, int? seriesID)
        {
            using (CCEntities db = new CCEntities())
            {
                var item = (from i in db.Items
                            where i.Title == title
                            && i.TypeID == typeID
                            && i.SeriesID == seriesID
                            select i).SingleOrDefault();

                return item;
            }
        }

        /// <summary>
        /// Gets a dictionary of genres.
        /// </summary>
        /// <param name="TypeID">Type of item (ie. COM,BOO,MOV,GAM)</param>
        /// <returns>A dictionary of genres for specified item type.</returns>
        public static Dictionary<string, string> GetGenreListByType(string TypeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var genres = (from g in db.Genres
                              where g.TypeID == TypeID
                              select new
                                {
                                    g.ID,
                                    g.Description
                                }).ToDictionary(x => x.ID.ToString(), x => x.Description);

                return genres;
            }
        }

        /// <summary>
        /// Searches the dbase for the platforms and creates a dictionary from them.
        /// </summary>
        /// <returns>A dictionary of platforms.</returns>
        public static Dictionary<string, string> GetPlatformList()
        {
            using (CCEntities db = new CCEntities())
            {
                var platforms = (from p in db.Platforms
                                 select new { 
                                     p.ID, 
                                     p.Description})
                                     .ToDictionary(x => x.ID.ToString(), x => x.Description);

                return platforms;
            }
        }

        /// <summary>
        /// Returns a dictionary of the content ratings.
        /// </summary>
        /// <param name="typeID">Type of item (ie. COM,BOO,MOV,GAM)</param>
        /// <returns>A dictionary of content ratings for specified item type.</returns>
        public static Dictionary<string, string> GetContentRatingList(string typeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var contentRatings = (from cr in db.ContentRatings
                                      where cr.Type == typeID
                                 select new
                                 {
                                     cr.ID,
                                     cr.Description
                                 })
                                     .ToDictionary(x => x.ID, x => x.Description);

                return contentRatings;
            }
        }

        /// <summary>
        /// Gets a genre from the database.
        /// </summary>
        /// <param name="genreId">ID of genre.</param>
        /// <returns>A Genre of specified ID</returns>
        internal static Genre GetGenreFromID(int genreId)
        {
            using (CCEntities db = new CCEntities())
            {
                var genre = (from g in db.Genres
                             where g.ID == genreId
                             select g).Single();

                return genre;
            }
        }

        /// <summary>
        /// Finds item in database.
        /// </summary>
        /// <param name="itemID">ID of item.</param>
        /// <returns>An item of specified itemID, or null.</returns>
        public static Item GetItemByID(int itemID)
        {
            using (CCEntities db = new CCEntities())
            {
                var item = (from i in db.Items
                            where i.ID == itemID
                            select i).SingleOrDefault();
                return item;
            }
        }


        internal static ComicBook GetComicBookByID(int itemID)
        {
            using (CCEntities db = new CCEntities())
            {
                var cBook = (from cb in db.ComicBooks
                            where cb.ItemID == itemID
                            select cb).SingleOrDefault();
                return cBook;
            }
        }

        internal static Book GetBookByID(int itemID)
        {
            using (CCEntities db = new CCEntities())
            {
                var book = (from b in db.Books
                            where b.ItemID == itemID
                            select b).SingleOrDefault();
                return book;
            }
        }

        internal static Movie GetMovieByID(int itemID)
        {
            using (CCEntities db = new CCEntities())
            {
                var movie = (from m in db.Movies
                            where m.ItemID == itemID
                            select m).SingleOrDefault();
                return movie;
            }
        }

        internal static Game GetGameByID(int itemID)
        {
            using (CCEntities db = new CCEntities())
            {
                var game = (from g in db.Games
                            where g.ItemID == itemID
                            select g).SingleOrDefault();
                return game;
            }
        }

        public static List<String> GetItemGenresByID(int itemID)
        {
            using(CCEntities db = new CCEntities())
            {
                List<string> genres = new List<string>();
                var item = (from g in db.Items
                                       where g.ID == itemID
                                       select g).Single();

                foreach (Genre genre in item.Genres)
                {
                    genres.Add(genre.Description);
                }

                return genres;
            }
        }

        internal static string GetCollectionTypeNameByID(string TypeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var type = (from t in db.ItemTypes
                            where t.ID == TypeID
                            select t).Single();

                return type.Name;
            }
        }

        internal static string AddItemToCollection(int ItemID, int CollectionID, int UserID, string CollectionName, string TypeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var collection = (from c in db.Collections
                                  where c.ID == CollectionID
                                  && c.UserID == UserID
                                  select c).SingleOrDefault();

                if (collection == null)
                {
                    var collectionByName = FindCollectionByName(CollectionName, TypeID);

                    if (collectionByName == null)
                    {
                        Collection newCollection = new Collection();
                        newCollection.Name = CollectionName;
                        newCollection.TypeID = TypeID;
                        newCollection.UserID = UserID;
                        newCollection.IsComplete = false;
                        newCollection.IsPublic = true;

                        db.Collections.AddObject(newCollection);
                        db.SaveChanges();
                        collection = newCollection;
                    }
                    else
                    {
                        collection = collectionByName;
                    }
                }

                if(collection != null)
                {
                    CollectionItem cItem = new CollectionItem();
                    cItem.CollectionID = collection.ID;
                    cItem.ItemID = ItemID;
                    cItem.StatusID = "IN";
                    try
                    {
                        if (IsCollectionItemUnique(cItem))
                        {
                            db.CollectionItems.AddObject(cItem);
                            db.SaveChanges();
                            return "Item was successfully added to your collection <b>'" + collection.Name + "'</b>."; // Item saved properly in dbase.
                        }
                        return "This item cannot be added to your collection <b>'" + collection.Name + "'</b> because it already exists.";
                    }
                    catch (Exception ex)
                    {
                        return "There was an error while trying to add the item to your collection <b>'" + collection.Name + "'</b>.";
                    }
                }
                return "Error, collection <b>'" + collection.Name + "'</b> was not found."; // Something went wrong.
            }
        }

        private static Collection FindCollectionByName(string CollectionName, string TypeID)
        {
            var cUser = Utilities.Utility.GetUserToken();
            using (CCEntities db = new CCEntities())
            {
                var collection = (from c in db.Collections
                                  where c.Name == CollectionName
                                  && c.TypeID == TypeID
                                  && c.UserID == cUser.UserId
                                  select c).SingleOrDefault();

                return collection;
            }
        }

        private static bool IsCollectionItemUnique(CollectionItem cItem)
        {
            using (CCEntities db = new CCEntities())
            {
                var item = (from ci in db.CollectionItems
                            where ci.ItemID == cItem.ItemID
                            && ci.CollectionID == cItem.CollectionID
                            select ci).SingleOrDefault();

                if (item == null)
                    return true;
                else
                    return false;
            }
        }

        internal static List<CollectionItem> FindCollectionItems(int id)
        {
            using (CCEntities db = new CCEntities())
            {
                var collectionItems = (from ci in db.CollectionItems
                                       where ci.CollectionID == id
                                       select ci).ToList();

                return collectionItems;
            }
        }

        internal static string FindCollectionNameById(int id)
        {
            using (CCEntities db = new CCEntities())
            {
                var collection = (from c in db.Collections
                                  where c.ID == id
                                  select c).SingleOrDefault();

                return collection.Name;
            }
        }

        internal static CollectionItemStatu GetStatusByID(string StatusID)
        {
            using (CCEntities db = new CCEntities())
            {
                var status = (from s in db.CollectionItemStatus
                                  where s.ID == StatusID
                                  select s).SingleOrDefault();

                return status;
            }
        }

        internal static void RemoveItemFromCollection(int CollectionID, int ItemID)
        {
            using (CCEntities db = new CCEntities())
            {
                var collectionItem = (from ci in db.CollectionItems
                                      where ci.CollectionID == CollectionID
                                      && ci.ItemID == ItemID
                                      select ci).SingleOrDefault();

                db.CollectionItems.DeleteObject(collectionItem);
                db.SaveChanges();
            }
        }

        internal static void RemoveCollection(int CollectionID)
        {
            using (CCEntities db = new CCEntities())
            {
                try
                {
                    var collectionItems = (from ci in db.CollectionItems
                                           where ci.CollectionID == CollectionID
                                           select ci).ToList();

                    foreach (var item in collectionItems)
                    {
                        db.CollectionItems.DeleteObject(item);
                    }

                    var collection = (from c in db.Collections
                                      where c.ID == CollectionID
                                      select c).SingleOrDefault();

                    if (collection != null)
                    {
                        db.Collections.DeleteObject(collection);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
        }

        internal static string GetPlatformName(int PlatformID)
        {
            using (CCEntities db = new CCEntities())
            {
                var platform = (from p in db.Platforms
                              where p.ID == PlatformID
                              select p).SingleOrDefault();

                return platform.Description;
            }
        }

        internal static string GetRatingName(string RatingID)
        {
            if (RatingID == "") return "";
            using (CCEntities db = new CCEntities())
            {
                var rating = (from r in db.ContentRatings
                                where r.ID == RatingID
                                select r).SingleOrDefault();

                return rating.Description;
            }
        }

        internal static int GetCollectionItemCount(int CollectionID)
        {
            using (CCEntities db = new CCEntities())
            {
                var collectionItems = (from c in db.CollectionItems
                                  where c.CollectionID == CollectionID
                                  select c).ToList();

                return collectionItems.Count;
            }
        }

        internal static string EditCollection(int CollectionID, string cName,string cType, string cDesc, bool isPublic, bool isComplete)
        {
            var collection = FindCollectionByName(cName, cType);
            if (collection == null || collection.ID == CollectionID)
            {
                using(CCEntities db = new CCEntities()){
                    collection = (from c in db.Collections
                                  where c.ID == CollectionID
                                  select c).Single();

                    try {
                        collection.Name = cName;
                        collection.Description = cDesc;
                        collection.IsPublic = isPublic;
                        collection.IsComplete = isComplete;

                        db.SaveChanges();
                        return "success";
                    }
                    catch (Exception ex) {
                        return "Service is currently unavailable: " + ex.Message;
                    }
                }
            }
            else
            {
                return "A " + GetCollectionTypeNameByID(cType) + " Collection already exists with this name.";
            }
        }

        internal static byte[] GetPictureByID(int? id)
        {
            using (CCEntities db = new CCEntities())
            {
                var picture = (from p in db.Pictures
                               where p.ID == id
                               select p).SingleOrDefault();

                if (picture != null) return picture.pictureBytes;
                return null;
            }
        }

        internal static double FindItemRatingsAverage(int id)
        {
            // get list of user items with item id
            // sum and divide by count
            // if 0, it has not been rating yet
            throw new NotImplementedException();
        }

        internal static string EditCollectionItem(int collectionID, int itemID, string statusID, string statusNote, byte rating, string review)
        {
            using (CCEntities db = new CCEntities())
            {
                var collectionItem = (from ci in db.CollectionItems
                                      where ci.ItemID == itemID
                                      && ci.CollectionID == collectionID
                                      select ci).Single();
                try
                {
                    collectionItem.StatusID = statusID;
                    collectionItem.StatusNote = statusNote;
                    collectionItem.Rating = rating;
                    collectionItem.Review = review;

                    db.SaveChanges();
                    return "success";
                }
                catch (Exception ex)
                {
                    return "Service is currently unavailable: " + ex.Message;
                }
            }
        }

        internal static Dictionary<string,string> GetItemStatus()
        {
            using (CCEntities db = new CCEntities())
            {
                var status = (from s in db.CollectionItemStatus
                              select new
                              {
                                  s.ID,
                                  s.Description
                              })
                                     .ToDictionary(x => x.ID, x => x.Description);

                return status;
            }
                          
        }

        internal static int? FindCollectionPicture(int id)
        {
            using (CCEntities db = new CCEntities())
            {
                var collectionitem = (from ci in db.CollectionItems
                                      where ci.CollectionID == id
                                      select ci).ToList();

                // Gets random item in the collection
                Random rnd = new Random();
                if (collectionitem.Count > 0)
                {
                    int r = rnd.Next(collectionitem.Count);
                    int itemID = collectionitem[r].ItemID;
                    var item = (from i in db.Items
                                where i.ID == itemID
                                select i).SingleOrDefault();
                    return item.PictureID;
                }
                return null;                
            }
        }
    }
}