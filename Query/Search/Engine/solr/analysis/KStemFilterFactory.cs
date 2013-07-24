namespace org.apache.solr.analysis
{

  /**
   * <p>Title: </p>
   * <p>Description: This filter transforms an input word into its stemmed form
   * using Bob Krovetz' kstem algorithm.</p>
   * <p>Copyright: Copyright (c) 2003</p>
   * <p>Company: CIIR Umass Amherst (http://ciir.cs.umass.edu) </p>
   * @author Sergio Guzman-Lara
   * @version 1.0
   */

  using CharArraySet = org.apache.lucene.analysis.CharArraySet;
  using TokenStream = org.apache.lucene.analysis.TokenStream;
  using ResourceLoader = org.apache.solr.common.ResourceLoader;
  using StrUtils = org.apache.solr.common.util.StrUtils;
  using ResourceLoaderAware = org.apache.solr.util.plugin.ResourceLoaderAware;
  using LucidKStemFilter = com.lucidimagination.luceneworks.analysis.LucidKStemFilter;

  using IOException = java.io.IOException;
  using File = java.io.File;
  using List = java.util.List;

  public class KStemFilterFactory : BaseTokenFilterFactory, ResourceLoaderAware
  {
    public static readonly string PROTECTED_TOKENS = "protected";

    public void inform(ResourceLoader loader)
    {
      string wordFiles = (string)args.get(PROTECTED_TOKENS);
      if (wordFiles != null)
      {
        try
        {
          File protectedWordFiles = new File(wordFiles);
          if (protectedWordFiles.exists())
          {
            List/*<String>*/ wlist = loader.getLines(wordFiles);
            //This cast is safe in Lucene
            protectedWords = new CharArraySet(wlist, false);//No need to go through StopFilter as before, since it just uses a List internally
          }
          else
          {
            List/*<String>*/ files = StrUtils.splitFileNames(wordFiles);
            for (var iter = files.iterator(); iter.hasNext(); )
            {
              string file = (string)iter.next();
              List/*<String>*/ wlist = loader.getLines(file.Trim());
              if (protectedWords == null)
                protectedWords = new CharArraySet(wlist, false);
              else
                protectedWords.addAll(wlist);
            }
          }
        }
        catch (IOException e)
        {
            throw new System.ApplicationException("Unexpected exception", e);
        }
      }
    }

    private CharArraySet protectedWords = null;

    public override TokenStream create(TokenStream input) 
    {
      return new LucidKStemFilter(input, protectedWords);
    }
  }
}