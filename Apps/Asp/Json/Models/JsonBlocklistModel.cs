using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Asp.Json.Models
{
    public class BlockListModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class JsonBlockListsModel
        : JsonResponseModel
    {
        public IList<BlockListModel> BlockLists { get; set; }
    }
}