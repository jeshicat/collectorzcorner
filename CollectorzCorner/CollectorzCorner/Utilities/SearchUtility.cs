using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CollectorzCorner.Utilities
{
    public static class SearchUtility
    {
        public static List<Item> SearchByKeyword(string keyword)
        {
            using (CCEntities db = new CCEntities())
            {
                var search = (from i in db.Items
                              from s in db.Series
                              where i.Title.ToLower().Contains(keyword)
                              || (s.Name.ToLower().Contains(keyword) && i.SeriesID == s.ID)
                              || i.Publisher.ToLower().Contains(keyword)                              
                              select i).Distinct().ToList();

                return search;
            }
        }

        public static List<Item> SearchByKeywordAndTypeID(string keyword, string typeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var search = (from i in db.Items
                              from s in db.Series
                              where i.Title.Contains(keyword)
                              || (s.Name.Contains(keyword) && i.SeriesID == s.ID)
                              || i.Publisher.Contains(keyword)
                              && i.TypeID == typeID                              
                              select i).Distinct().ToList();

                return search;
            }
        }

        public static List<Item> SearchAll()
        {
            using (CCEntities db = new CCEntities())
            {
                var search = (from i in db.Items                              
                              select i).Distinct().ToList();

                return search;
            }
        }

        public static List<Item> SearchByTypeID(string typeID)
        {
            using (CCEntities db = new CCEntities())
            {
                var search = (from i in db.Items
                              where i.TypeID == typeID                              
                              select i).Distinct().ToList();

                return search;
            }
        }
        /*
search for item:
title
release date
seriesid
typeid
publisher

search for movie:
director
writer
producer
studio

search for comic:
writer
artist
colorist
coverartist
editor

search for book:
author
illustrator
editor
isbn

search for game:
developer
contentRating
platform

if all fields from item are blank, send error
if typeid is not = 0 then do search for other fields*/
    }
}