using System;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.analysis.tokenattributes;
using org.apache.solr.common;

namespace LinkMe.Query.Search.Engine.Test.Members
{
    [TestClass]
    public class AnalysisDemo
    {
        private static AnalyzerFactory _factory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _factory = new AnalyzerFactory(new ResourceLoaderImpl(@"Apps\Config"));
        }

        [TestMethod, Ignore]
        public void Demo()
        {
            DisplayTokens(_factory.CreateContentAnalyzer(), FieldName.FirstName, "Alexander");
            DisplayTokens(_factory.CreateContentAnalyzer(), FieldName.FirstName, "Alex");
            DisplayTokens(_factory.CreateContentAnalyzer(), FieldName.FirstName, "Aleksandr");
            DisplayTokens(_factory.CreateContentAnalyzer(), FieldName.LastName, "Chiviliov");
            DisplayTokens(_factory.CreateContentAnalyzer(), FieldName.LastName, "Tchivilev");
            DisplayTokens(_factory.CreateContentAnalyzer(), FieldName.Content, "Senior IT Architect at LinkMe");

            DisplayTokens(_factory.CreateQueryAnalyzer(), FieldName.Name, "Alexander");
            DisplayTokens(_factory.CreateQueryAnalyzer(), FieldName.Name, "Alex");
            DisplayTokens(_factory.CreateQueryAnalyzer(), FieldName.Name, "Aleksandr");
            DisplayTokens(_factory.CreateQueryAnalyzer(), FieldName.Name, "Chiviliov");
            DisplayTokens(_factory.CreateQueryAnalyzer(), FieldName.Name, "Tchivilev");
            DisplayTokens(_factory.CreateQueryAnalyzer(), FieldName.Content, "Senior IT Architect at LinkMe");
        }

        private static void DisplayTokens(Analyzer analyzer, string fieldName, string text)
        {
            var stream = analyzer.tokenStream(fieldName, new java.io.StringReader(text));
            var term = (TermAttribute)stream.addAttribute(typeof(TermAttribute));
            var posIncr = (PositionIncrementAttribute)stream.addAttribute(typeof(PositionIncrementAttribute));
            var offset = (OffsetAttribute)stream.addAttribute(typeof(OffsetAttribute));
            var type = (TypeAttribute)stream.addAttribute(typeof(TypeAttribute));

            Console.WriteLine("Analyzing {0}:{1}", fieldName, text);
            int position = 0;
            while (stream.incrementToken())
            {
                int increment = posIncr.getPositionIncrement();
                if (increment > 0)
                {
                    position = position + increment;
                    Console.WriteLine();
                    Console.Write(position + ": ");
                }

                Console.Write("[" +
                              term.term() + ":" +
                              offset.startOffset() + "->" +
                              offset.endOffset() + ":" +
                              type.type() + "] ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}