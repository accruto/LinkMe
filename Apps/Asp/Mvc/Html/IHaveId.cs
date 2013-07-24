namespace LinkMe.Apps.Asp.Mvc.Html
{
    public interface IHaveId
    {
        string Id { get; set; }
    }

    public static class HaveIdExtensions
    {
        public static T WithId<T>(this T field, string id)
            where T : IHaveId
        {
            field.Id = id;
            return field;
        }
    }
}
