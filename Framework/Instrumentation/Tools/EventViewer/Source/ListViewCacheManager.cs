using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
    internal class ListViewCacheManager<THolder, TData, TIdentifier>
        where THolder : class // References to the cached holder objects are passed to the client
        where TIdentifier : class // Must be nullable
    {
        #region Nested types

        public delegate TData EnsureLoaded(THolder holder, out TIdentifier loadedIdentifier);
        public delegate void LoadRange(int startIndex, int endIndex);

        private class CacheRegion : IComparable<CacheRegion>
        {
            private readonly int startIndex;
            private readonly int endIndex;

            public CacheRegion(int startIndex, int endIndex)
            {
                Debug.Assert(startIndex <= endIndex, "startIndex <= endIndex");

                this.startIndex = startIndex;
                this.endIndex = endIndex;
            }

            public int StartIndex
            {
                get { return startIndex; }
            }

            public int EndIndex
            {
                get { return endIndex; }
            }

            public int Size
            {
                get { return endIndex - startIndex + 1; }
            }

            public int CompareTo(CacheRegion other)
            {
                return StartIndex.CompareTo(other.StartIndex);
            }

            public override string ToString()
            {
                return startIndex + " - " + endIndex + " (" + Size + ")";
            }

            public bool Contains(int index)
            {
                return (index >= startIndex && index <= endIndex);
            }
        }

        #endregion

        private readonly List<THolder> m_placeholders = new List<THolder>();
        private readonly Dictionary<TIdentifier, int> m_indices = new Dictionary<TIdentifier, int>();
        private readonly List<CacheRegion> m_cacheRegions = new List<CacheRegion>();
        private readonly EnsureLoaded m_ensureLoaded;
        private readonly LoadRange m_loadRange;
        private int m_lastRequestedStartIndex = int.MaxValue; // Assume starting from the bottom, scrolling up.
        private int m_minBatchToLoad = 1;

        internal ListViewCacheManager(EnsureLoaded ensureLoaded, LoadRange loadRange)
        {
            if (ensureLoaded == null)
                throw new ArgumentNullException("ensureLoaded");
            if (loadRange == null)
                throw new ArgumentNullException("loadRange");

            m_ensureLoaded = ensureLoaded;
            m_loadRange = loadRange;
        }

        public int Count
        {
            get { return m_placeholders.Count; }
        }

        public THolder GetHolder(TIdentifier identifier)
        {
            int index;
            try
            {
                index = m_indices[identifier];
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("The identifier " + identifier + " was not found."
                    + " It must be added by calling AddPlaceholder() or AddData().", ex);
            }

            return GetHolder(index);
        }

        public THolder GetHolder(int index)
        {
            try
            {
                return m_placeholders[index];
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException("The index " + index + " is invalid. It must be between 0 and "
                    + (m_placeholders.Count - 1), ex);
            }
        }

        public TData GetData(int index)
        {
            return GetData(m_placeholders[index]);
        }

        public TData GetData(THolder holder)
        {
            TIdentifier loadedIdentifier;
            TData data = m_ensureLoaded(holder, out loadedIdentifier);

            if (loadedIdentifier != null)
            {
                // This was a cache miss, but it's cached now, so add a CachedRegion for it.

                int loadedIndex = m_indices[loadedIdentifier];
                Debug.WriteLine("Cache miss for index " + loadedIndex);
                AddCachedRegion(new CacheRegion(loadedIndex, loadedIndex));
            }

            return data;
        }

        public IList<TData> LoadAll()
        {
            IList<TData> all = new List<TData>(Count);

            foreach (THolder holder in m_placeholders)
            {
                all.Add(GetData(holder));
            }

            return all;
        }

        public void Clear()
        {
            m_placeholders.Clear();
            m_indices.Clear();
            m_cacheRegions.Clear();
        }

        public void AddData(THolder holder, TIdentifier identifier, int index)
        {
            AddPlaceholder(holder, identifier, index);
            AddCachedRegion(new CacheRegion(index, index));
        }

        public void AddPlaceholder(THolder holder, TIdentifier identifier, int index)
        {
            m_placeholders.Add(holder);
            m_indices[identifier] = index;
        }

        public void CacheItemRange(int startIndex, int endIndex)
        {
            CacheRegion region = new CacheRegion(startIndex, endIndex);
            IList<CacheRegion> regionsToLoad = FindUncachedRegions(region);

            if (regionsToLoad != null)
            {
                // Since we're loading anyway pad the region a bit to load a decent minimum number of
                // items at a time, avoiding many small load requests. This is ONLY done if a load
                // is required in the first place.

                if (regionsToLoad.Count == 1)
                {
                    regionsToLoad = FindUncachedRegions(PadRegionToLoad(regionsToLoad[0]));
                }

                foreach (CacheRegion regionToLoad in regionsToLoad)
                {
                    Stopwatch watch = Stopwatch.StartNew();
                    m_loadRange(regionToLoad.StartIndex, regionToLoad.EndIndex);
                    Debug.WriteLine(string.Format("Loaded {0} in {1} ms.", regionToLoad, watch.ElapsedMilliseconds));
                    AddCachedRegion(regionToLoad);
                }
            }

            m_lastRequestedStartIndex = startIndex;
            m_minBatchToLoad = Math.Max(m_minBatchToLoad, region.Size); // Next time load at least this many.
        }

        private CacheRegion PadRegionToLoad(CacheRegion region)
        {
            if (region.Size < m_minBatchToLoad)
            {
                // Pad the region with more indexes in the scroll direction up to the minimum batch size.

                if (region.StartIndex > m_lastRequestedStartIndex)
                {
                    region = new CacheRegion(
                        region.StartIndex, Math.Min(region.StartIndex + m_minBatchToLoad, Count) - 1);
                }
                else
                {
                    region = new CacheRegion(
                        Math.Max(region.EndIndex - m_minBatchToLoad + 1, 0), region.EndIndex);
                }
            }

            return region;
        }

        private IList<CacheRegion> FindUncachedRegions(CacheRegion region)
        {
            // Find a point to search from using binary search.

            int iSearchFrom = m_cacheRegions.BinarySearch(region);
            if (iSearchFrom < 0)
            {
                iSearchFrom = Math.Max(~iSearchFrom - 1, 0);
            }

            // Find a cache region that includes the start index and one that includes the end index, if any.

            int iStartRegion = -1, iEndRegion = -1;

            for (int i = iSearchFrom;
                i >= 0 && i < m_cacheRegions.Count && region.StartIndex >= m_cacheRegions[i].StartIndex; i--)
            {
                if (m_cacheRegions[i].Contains(region.StartIndex))
                {
                    iStartRegion = i;
                    break;
                }
            }

            for (int i = iSearchFrom;
                i < m_cacheRegions.Count && region.EndIndex <= m_cacheRegions[i].EndIndex; i++)
            {
                if (m_cacheRegions[i].Contains(region.EndIndex))
                {
                    iEndRegion = i;
                    break;
                }
            }

            Debug.Assert(iStartRegion <= iEndRegion || iEndRegion == -1, "iStartRegion <= iEndRegion || iEndRegion == -1");

            if (iStartRegion == -1)
            {
                if (iEndRegion == -1)
                    return new CacheRegion[] { region }; // No cache hit at all.

                // Partial hit at the end of the range.
                return new CacheRegion[] { new CacheRegion(region.StartIndex, m_cacheRegions[iEndRegion].StartIndex - 1) };
            }
            else if (iEndRegion == -1)
            {
                // Partial hit at the start of the range.
                return new CacheRegion[] { new CacheRegion(m_cacheRegions[iStartRegion].EndIndex + 1, region.EndIndex) };
            }
            else
            {
                // Hits at both the start and end of the range. Is it the same region?

                if (iStartRegion == iEndRegion)
                    return null; // Great, cache hit for the whole region.

                // Return a list of all the "gaps" between the regions we already have in the cache
                // (usually only 1).

                List<CacheRegion> gaps = new List<CacheRegion>();

                for (int i = iStartRegion; i < iEndRegion; i++)
                {
                    gaps.Add(new CacheRegion(m_cacheRegions[i].EndIndex + 1, m_cacheRegions[i + 1].StartIndex - 1));
                }

                return gaps;
            }
        }

        private void AddCachedRegion(CacheRegion region)
        {
            int index = m_cacheRegions.BinarySearch(region);
            Debug.Assert(index < 0, "Region to be added already found in the list.");

            index = ~index;

            // Merge with the previous cached region, if adjacent.

            if (index > 0 && m_cacheRegions[index - 1].EndIndex >= region.StartIndex - 1)
            {
                region = new CacheRegion(m_cacheRegions[--index].StartIndex, region.EndIndex);
                m_cacheRegions.RemoveAt(index);
            }

            // Merge with the next cached region, if adjacent.

            if (index < m_cacheRegions.Count && m_cacheRegions[index].StartIndex <= region.EndIndex + 1)
            {
                region = new CacheRegion(region.StartIndex, m_cacheRegions[index].EndIndex);
                m_cacheRegions.RemoveAt(index);
            }

            m_cacheRegions.Insert(index, region);
        }
    }
}
