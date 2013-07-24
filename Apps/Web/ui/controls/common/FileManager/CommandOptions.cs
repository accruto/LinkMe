using System;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class CommandOptions
    {
        [Flags]
        private enum Options
        {
            All = 0xFF,
            Copy = 0x01,
            Move = 0x02,
            Delete = 0x04,
            Rename = 0x08,
            FolderUp = 0x10,
            View = 0x20,
            NewFolder = 0x40,
            Refresh = 0x80,
        }

        private Options options = Options.All;

        internal CommandOptions()
        {
        }

        public bool Copy
        {
            get { return options.IsFlagSet(Options.Copy); }
            set { options = options.SetFlag(Options.Copy, value); }
        }

        public bool Move
        {
            get { return options.IsFlagSet(Options.Move); }
            set { options = options.SetFlag(Options.Move, value); }
        }

        public bool Delete
        {
            get { return options.IsFlagSet(Options.Delete); }
            set { options = options.SetFlag(Options.Delete, value); }
        }

        public bool FolderUp
        {
            get { return options.IsFlagSet(Options.FolderUp); }
            set { options = options.SetFlag(Options.FolderUp, value); }
        }

        public bool Rename
        {
            get { return options.IsFlagSet(Options.Rename); }
            set { options = options.SetFlag(Options.Rename, value); }
        }

        public bool View
        {
            get { return options.IsFlagSet(Options.View); }
            set { options = options.SetFlag(Options.View, value); }
        }

        public bool NewFolder
        {
            get { return options.IsFlagSet(Options.NewFolder); }
            set { options = options.SetFlag(Options.NewFolder, value); }
        }

        public bool Refresh
        {
            get { return options.IsFlagSet(Options.Refresh); }
            set { options = options.SetFlag(Options.Refresh, value); }
        }
    }
}
