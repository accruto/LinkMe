namespace LinkMe.Apps.Pageflows
{
    internal interface IPageflow
    {
        void MoveFirst();
        void MoveToNext();
        void MoveToPrevious();
        void MoveTo(string step);
    }
}
