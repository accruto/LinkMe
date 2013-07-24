using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class SecurePaySerializerFormatAttribute
        : Attribute, IOperationBehavior
    {
        void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
        {
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
        {
            ReplaceDataContractSerializerOperationBehavior(description);
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
        {
            ReplaceDataContractSerializerOperationBehavior(description);
        }

        void IOperationBehavior.Validate(OperationDescription description)
        {
        }

        private static void ReplaceDataContractSerializerOperationBehavior(OperationDescription description)
        {
            var existing = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (existing != null)
                description.Behaviors.Remove(existing);
            description.Behaviors.Add(new SecurePaySerializerOperationBehavior(description));
        }

        public class SecurePaySerializerOperationBehavior
            : DataContractSerializerOperationBehavior
        {
            public SecurePaySerializerOperationBehavior(OperationDescription operationDescription)
                : base(operationDescription)
            {
            }

            public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
            {
                return new SecurePaySerializer(type);
            }

            public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
            {
                return new SecurePaySerializer(type);
            }
        }
    }
}