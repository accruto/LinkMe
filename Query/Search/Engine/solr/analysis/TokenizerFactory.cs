using java.util;
using org.apache.lucene.analysis;

namespace org.apache.solr.analysis
{
  public interface TokenizerFactory
  {
    /** <code>init</code> will be called just once, immediately after creation.
     * <p>The args are user-level initialization parameters that
     * may be specified when declaring a the factory in the
     * schema.xml
     */
    void init(Map/*<String, String>*/ args);

    /**
     * Accessor method for reporting the args used to initialize this factory.
     * <p>
     * Implementations are <strong>strongly</strong> encouraged to return 
     * the contents of the Map passed to to the init method
     * </p>
     */
    Map/*<String, String>*/ getArgs();

    /** Creates a TokenStream of the specified input */
    Tokenizer create(java.io.Reader input);
  }
}
