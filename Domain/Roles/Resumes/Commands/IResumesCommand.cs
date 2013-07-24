namespace LinkMe.Domain.Roles.Resumes.Commands
{
    /// <summary>
    /// Direct access to create or update the resume
    /// Except in special circumstances updating the resume 
    /// should be through CandidateResumesCommand
    /// </summary>
    public interface IResumesCommand
    {
        /// <summary>
        /// Create a resume WITHOUT firing events
        /// ie. search engine won't be updated
        /// </summary>
        /// <param name="resume">The resume to create</param>
        void CreateResume(Resume resume);
        /// <summary>
        /// Update a resume WITHOUT firing events
        /// ie. search engine won't be updated
        /// </summary>
        /// <param name="resume">The resume to update</param>
        void UpdateResume(Resume resume);
    }
}
