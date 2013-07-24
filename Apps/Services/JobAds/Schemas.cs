using System;
using System.Xml.Schema;

namespace LinkMe.Apps.Services.JobAds
{
    public static class Schemas
    {
        public static XmlSchema JobAd
        {
            get
            {
                var schema = ReadSchemaFromResource(typeof(Schemas), "JobAd.xsd");
                var include = new XmlSchemaInclude {Schema = Types};
                schema.Includes.Add(include);
                return schema;
            }
        }

        public static XmlSchema Types
        {
            get { return ReadSchemaFromResource(typeof(Schemas), "Types.xsd"); }
        }

        public static XmlSchema ReadSchemaFromResource(Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            try
            {
                var stream = type.Assembly.GetManifestResourceStream(type, name);
                if (stream == null)
                    throw new ApplicationException(string.Format("There is no embedded resource '{0}' scoped by type '{1}' in assembly '{2}'.", name, type.FullName, type.Assembly.FullName));

                using (stream)
                {
                    return XmlSchema.Read(stream, null);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to read the '" + name + "' XML schema.", ex);
            }
        }

        public static void AddIncludedSchema(XmlSchema rootSchema, XmlSchema includedSchema)
        {
            var include = new XmlSchemaInclude {Schema = includedSchema};
            rootSchema.Includes.Add(include);
        }
    }
}