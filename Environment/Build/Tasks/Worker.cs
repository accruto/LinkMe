using System;

namespace LinkMe.Environment.Build.Tasks
{
    internal abstract class Worker
        : IDisposable
    {
        protected Worker()
        {
        }

        protected GacUtil GetGac()
        {
            if ( m_gac == null )
                m_gac = new GacUtil();
            return m_gac;
        }

        public virtual void Dispose()
        {
            // Clean up if needed.

            if ( m_gac != null )
            {
                m_gac.Dispose();
                m_gac = null;
            }
        }

        private GacUtil m_gac;
    }
}