using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Asp.Json.Models
{
    public class FolderModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool CanRename { get; set; }
        public bool CanDelete { get; set; }
        public int Count { get; set; }
    }

    public class JsonFolderModel
        : JsonResponseModel
    {
        public FolderModel Folder { get; set; }
    }

    public class JsonFoldersModel
        : JsonResponseModel
    {
        public IList<FolderModel> Folders { get; set; }
    }
}