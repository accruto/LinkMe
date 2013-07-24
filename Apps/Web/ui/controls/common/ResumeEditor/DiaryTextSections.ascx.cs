using System;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class DiaryTextSections
        : LinkMeUserControl
    {
        public const string DiaryEntriesField = "R0";

        public void DisplayDiary(Guid? diaryId, bool allowEditing)
        {
            ucDiaryEntries.DisplayDiary(diaryId, allowEditing);
        }

        public void StartEditingEntries()
        {
            ucDiaryEntries.StartEditingOnLoad = true;
        }
    }
}
