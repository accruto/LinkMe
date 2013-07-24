using System;

namespace LinkMe.Query.Search.Test.Engine
{
    public class Document
    {
        public readonly Guid id;
        public readonly string xml;

        public Document(string id, string xml)
        {
            this.xml = xml;
            this.id = new Guid(id);
        }

        private static readonly Document[] _docs = new Document[]
                                                       {
                                                           new Document(
                                                               "26d63fca-6b63-4873-82b0-bf88347c46b4",
                                                               "<ResDoc><resume><experience><job>Accountant</job></experience></resume></ResDoc>"),
                                                           new Document(
                                                               "15707e2a-9fe9-4605-9e8b-8aca3e0d218c",
                                                               "<ResDoc><resume><experience><job>Truant officer</job></experience></resume></ResDoc>"),
                                                           new Document(
                                                               "31f5f787-a313-437e-88fd-848aa4d08c62",
                                                               "<ResDoc><resume><experience><job>Technical Officer</job></experience></resume></ResDoc>"),
                                                           new Document(
                                                               "f1c658ce-01b7-4339-ab03-7c1eb75f5363",
                                                               @"<ResDoc><resume><experience>
                          <job>Technical Assistant Officer</job>
                          <job>Assistant officer</job>
                          <job>Assistant officer</job>
                          <job>Assistant officer</job>
                          <job>Assistant officer</job>
                          <job>Technical Assistant</job>
                      </experience></resume></ResDoc>"),
                                                           new Document(
                                                               "f3914193-92ed-4938-a339-adb4a1a4003d",
                                                               "<ResDoc><resume><experience><job>Technical Manager</job></experience></resume></ResDoc>"),
                                                           new Document(
                                                               "84d5ce64-e51f-49b1-a8a1-016c1cf247d2",
                                                               "<ResDoc><resume><experience><job>Manager Officer Officer</job></experience></resume></ResDoc>")
                                                       };

        public static Document[] SampleCollection
        {
            get { return _docs; }
        }
    }
}