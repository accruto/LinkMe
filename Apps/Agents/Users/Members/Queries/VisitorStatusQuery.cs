namespace LinkMe.Apps.Agents.Users.Members.Queries
{
    public class VisitorStatusQuery
        : IVisitorStatusQuery
    {
        private readonly int[] _occasionalPrompts;
        private readonly int[] _casualPrompts;

        public VisitorStatusQuery(int[] occasionalPrompts, int[] casualPrompts)
        {
            _casualPrompts = casualPrompts;
            _occasionalPrompts = occasionalPrompts;
        }

        VisitorStatus IVisitorStatusQuery.GetVisitorStatus(int sessionViews, int sessionApplications, int occasionalPrompts, int casualPrompts)
        {
            // If enough casual prompts have already been shown then don't show any more.

            if (casualPrompts >= _casualPrompts.Length)
                return new VisitorStatus { Frequency = VisitorFrequency.Casual, ShouldPrompt = false };

            // Enough applications in a session triggers a prompt.

            if (sessionApplications >= _casualPrompts[casualPrompts])
                return new VisitorStatus { Frequency = VisitorFrequency.Casual, ShouldPrompt = true };

            // If a casual prompt has already been shown then are a casual visitor.

            if (casualPrompts > 0)
                return new VisitorStatus { Frequency = VisitorFrequency.Casual, ShouldPrompt = false };

            // If enough occasional prompts have already been shown then don't show any more.

            if (occasionalPrompts >= _occasionalPrompts.Length)
                return new VisitorStatus { Frequency = VisitorFrequency.Occasional, ShouldPrompt = false };

            // Enough views in a session triggers a prompts.

            if (sessionViews >= _occasionalPrompts[occasionalPrompts])
                return new VisitorStatus { Frequency = VisitorFrequency.Occasional, ShouldPrompt = true };

            // If an occasional prompt has already been shown then are an occasional visitor.

            if (occasionalPrompts > 0)
                return new VisitorStatus { Frequency = VisitorFrequency.Occasional, ShouldPrompt = false };

            return new VisitorStatus
            {
                Frequency = VisitorFrequency.New,
                ShouldPrompt = false,
            };
        }
    }
}
