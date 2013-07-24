
using System.Diagnostics.CodeAnalysis;
using LinkMe.Framework.Utility;

namespace org.apache.solr.common
{
  using InputStream = java.io.InputStream;
  using java.util;
  using File = java.io.File;
  using BufferedReader = java.io.BufferedReader;
  using FileInputStream = java.io.FileInputStream;
  using InputStreamReader = java.io.InputStreamReader;
  using Charset = java.nio.charset.Charset;

  public class ResourceLoaderImpl : ResourceLoader
  {
    private static readonly Charset UTF_8 = Charset.forName("UTF-8");
    private readonly string instanceDir;

    public ResourceLoaderImpl(string path)
    {
      this.instanceDir = normalizeDir(FileSystem.GetSourcePath(path));
    }

    /** Opens any resource by its name.
     * By default, this will look in multiple locations to load the resource:
     * $configDir/$resource (if resource is not absolute)
     * $CWD/$resource
     * otherwise, it will look for it in any jar accessible through the class loader.
     * Override this method to customize loading resources. 
     *@return the stream for the named resource
     */
    public InputStream openResource(string resource) {
#pragma warning disable 219
      InputStream stream=null;
#pragma warning restore 219
      try {
        File f0 = new File(resource);
        File f = f0;
        if (!f.isAbsolute()) {
          // try $CWD/$configDir/$resource
          f = new File(getConfigDir() + resource);
        }
        if (f.isFile() && f.canRead()) {
          return new FileInputStream(f);
        } else if (f != f0) { // no success with $CWD/$configDir/$resource
          if (f0.isFile() && f0.canRead())
            return new FileInputStream(f0);
        }

      } catch (java.lang.Exception e) {
          throw new System.ApplicationException("Error opening " + resource, e);
      }

      throw new System.ApplicationException("Can't find resource '" + resource + "' in '" + getConfigDir() + "'");
    }

    /**
     * Accesses a resource by name and returns the (non comment) lines
     * containing data.
     *
     * <p>
     * A comment line is any line that starts with the character "#"
     * </p>
     *
     * @param resource
     * @return a list of non-blank non-comment lines with whitespace trimmed
     * from front and back.
     * @throws IOException
     */
    public List/*<String>*/ getLines(string resource) {
      return getLines(resource, UTF_8);
    }

    /** Ensures a directory name always ends with a '/'. */
    private static string normalizeDir(string path)
    {
      return (path != null && (!(path.EndsWith("/") || path.EndsWith("\\")))) ? path + File.separator : path;
    }

    private string getConfigDir()
    {
      return instanceDir;
    }

    private List/*<String>*/ getLines(string resource, Charset charset)
    {
      BufferedReader input = null;
      ArrayList/*<String>*/ lines;
      try
      {
        input = new BufferedReader(new InputStreamReader(openResource(resource),
            charset));

        lines = new ArrayList/*<String>*/();
        for (string word = null; (word = input.readLine()) != null; )
        {
          // skip comments
          if (word.StartsWith("#")) continue;
          word = word.Trim();
          // skip blank lines
          if (word.Length == 0) continue;
          lines.add(word);
        }
      }
      finally
      {
        if (input != null)
          input.close();
      }
      return lines;
    }
  }
}
