﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Falcon.Service.Common;
using Falcon.Entity;
using Constants;

namespace FalconSchool.Caching
{
    [System.ComponentModel.DataObject]
    public class MasterDataCache
    {
        public static void LoadStaticCache(Dictionary<string,List<DropdownData>> masterData)
        {
            foreach (var keyValue in masterData)
            {
                HttpRuntime.Cache.Insert(
              /* key */                keyValue.Key,
              /* value */              keyValue.Value,
              /* dependencies */       null,
              /* absoluteExpiration */ Cache.NoAbsoluteExpiration,
              /* slidingExpiration */  Cache.NoSlidingExpiration,
              /* priority */           CacheItemPriority.NotRemovable,
              /* onRemoveCallback */   null);
            }

        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static List<DropdownData> GetCachedDataByKey(string key)
        {
            return HttpRuntime.Cache[key] as List<DropdownData>;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static bool InsertCachedDataByKey(string key, object _obj)
        {
            HttpRuntime.Cache.Insert(
              /* key */                key,
              /* value */              _obj,
              /* dependencies */       null,
              /* absoluteExpiration */ Cache.NoAbsoluteExpiration,
              /* slidingExpiration */  Cache.NoSlidingExpiration,
              /* priority */           CacheItemPriority.NotRemovable,
              /* onRemoveCallback */   null);

            return true;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static List<DropdownData> DeleteCacheByKey(string key)
        {
            return HttpRuntime.Cache[key] as List<DropdownData>;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static bool UpdateAllCacheObjects(Dictionary<string, List<DropdownData>> masterData)
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }

            LoadStaticCache(masterData);

            return true;
        }
    }
}