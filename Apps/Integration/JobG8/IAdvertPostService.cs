using System;
using System.ServiceModel;
using System.Xml.Serialization;

namespace LinkMe.Apps.Integration.JobG8
{
    // NOTE: If you change the interface name "IAdvertPostService" here, you must also update the reference to "IAdvertPostService" in Web.config.
    [ServiceContract(Name = "AdvertPostServiceInterface", Namespace = "http://jobg8.com")]
    public interface IAdvertPostService
    {
        [OperationContract(Action = "http://jobg8.com:postAdvertIn")]
        [XmlSerializerFormat]
        PostAdvertResponseMessage PostAdvert(PostAdvertRequestMessage request);

        [OperationContract(Action = "http://jobg8.com:amendAdvertIn")]
        [XmlSerializerFormat]
        AmendAdvertResponseMessage AmendAdvert(AmendAdvertRequestMessage request);

        [OperationContract(Action = "http://jobg8.com:deleteAdvertIn")]
        [XmlSerializerFormat]
        DeleteAdvertResponseMessage DeleteAdvert(DeleteAdvertRequestMessage request);
    }

    [MessageContract(IsWrapped = false)]
    public class PostAdvertRequestMessage
    {
        [MessageHeader(Namespace = "http://jobg8.com/postadvertheaders")]
        public Credentials UserCredentials;

        [MessageBodyMember(Namespace = "http://jobg8.com/messages")]
        public PostAdvertRequest PostAdvert;
    }

    [MessageContract(IsWrapped = false)]
    public class PostAdvertResponseMessage
    {
        [MessageBodyMember(Namespace = "http://jobg8.com/messages")]
        public Response PostAdvertResponse;
    }

    [MessageContract(IsWrapped = false)]
    public class AmendAdvertRequestMessage
    {
        [MessageHeader(Namespace = "http://jobg8.com/postadvertheaders")]
        public Credentials UserCredentials;

        [MessageBodyMember(Namespace = "http://jobg8.com/messages")]
        public AmendAdvertRequest AmendAdvert;
    }

    [MessageContract(IsWrapped = false)]
    public class AmendAdvertResponseMessage
    {
        [MessageBodyMember(Namespace = "http://jobg8.com/messages")]
        public Response AmendAdvertResponse;
    }

    [MessageContract(IsWrapped = false)]
    public class DeleteAdvertRequestMessage
    {
        [MessageHeader(Namespace = "http://jobg8.com/postadvertheaders")]
        public Credentials UserCredentials;

        [MessageBodyMember(Namespace = "http://jobg8.com/messages")]
        public DeleteAdvertRequest DeleteAdvert;
    }

    [MessageContractAttribute(IsWrapped = false)]
    public class DeleteAdvertResponseMessage
    {
        [MessageBodyMember(Namespace = "http://jobg8.com/messages")]
        public Response DeleteAdvertResponse;
    }

    [Serializable]
    [XmlType(Namespace = "http://jobg8.com/postadvertheaders")]
    public class Credentials
    {
        public string Username;
        public string Password;
    }

    [Serializable]
    [XmlType(Namespace = "http://jobg8.com/")]
    public class PostAdvertRequest
    {
        public PostAdverts Adverts;
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://jobg8.com/")]
    public class PostAdverts
    {
        [XmlElement] public PostAdvert[] PostAdvert;
        [XmlAttribute] public string AccountNumber;
    }
    
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class PostAdvert
    {
        public string JobReference;
        public string ClientReference;
        public string Classification;
        public string SubClassification;
        public string Position;
        public string Description;
        public string Location;
        public string Area;
        public string PostCode;
        public string Country;
        public EmploymentType EmploymentType;
        public string StartDate;
        public string Duration;
        public WorkHours WorkHours; [XmlIgnore] public bool WorkHoursSpecified;
        public VisaRequired VisaRequired;
        public PayPeriod? PayPeriod;
        public decimal PayAmount; [XmlIgnore] public bool PayAmountSpecified;
        public decimal PayMinimum; [XmlIgnore] public bool PayMinimumSpecified;
        public decimal PayMaximum; [XmlIgnore] public bool PayMaximumSpecified;
        public string Currency;
        public string PayAdditional;
        public string Contact;
        public string Telephone;
        public string ApplicationURL;
        public string RedirectionUrl;
        public string ApplicationFormXML;
        public string JobSource;
        public string AdvertiserName;
        public AdvertiserType AdvertiserType; [XmlIgnore] public bool AdvertiserTypeSpecified;
    }

    [Serializable]
    [XmlType(Namespace = "http://jobg8.com/")]
    public class AmendAdvertRequest
    {
        public AmendAdverts Adverts;
    }

    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jobg8.com/")]
    public class AmendAdverts
    {
        [XmlElement] public AmendAdvert[] AmendAdvert;
        [XmlAttribute] public string AccountNumber;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class AmendAdvert
    {
        public string JobReference;
        public string Position;
        public string Description;
        public string Location;
        public string Area;
        public string PostCode;
        public string Country;
        public EmploymentType EmploymentType;
        public string StartDate;
        public string Duration;
        public WorkHours WorkHours; [XmlIgnore] public bool WorkHoursSpecified;
        public VisaRequired VisaRequired;
        public PayPeriod PayPeriod; [XmlIgnore] public bool PayPeriodSpecified;
        public decimal PayAmount; [XmlIgnore] public bool PayAmountSpecified;
        public decimal PayMinimum; [XmlIgnore] public bool PayMinimumSpecified;
        public decimal PayMaximum; [XmlIgnore] public bool PayMaximumSpecified;
        public string Currency;
        public string PayAdditional;
        public string Contact;
        public string Telephone;
    }

    [Serializable]
    [XmlType(Namespace = "http://jobg8.com/")]
    public class DeleteAdvertRequest
    {
        public DeleteAdverts Adverts;
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://jobg8.com/")]
    public class DeleteAdverts
    {
        [XmlElement] public DeleteAdvert[] DeleteAdvert;
        [XmlAttribute] public string AccountNumber;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeleteAdvert
    {
        public string JobReference;
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum EmploymentType
    {
        Permanent,
        Contract,
        Temporary,
        Any,
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum WorkHours
    {
        [XmlEnum("Part Time")] PartTime,
        [XmlEnum("Full Time")] FullTime,
        [XmlEnum("Not Specified")] NotSpecified,
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum VisaRequired
    {
        [XmlEnum("Applications welcome from candidates who require a work visa")]
        Welcome,
        [XmlEnum("Applications considered from candidates who require a work visa")]
        Considered,
        [XmlEnum("Applicants must be eligible to work in the specified location")]
        MustBeEligible,
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum PayPeriod
    {
        Hourly,
        Weekly,
        Monthly,
        Annual,
        Day,
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum AdvertiserType
    {
        Company,
        Agency,
        AdvertisingAgency,
    }

    [Serializable]
    [XmlType(Namespace = "http://jobg8.com/messages")]
    public class Response
    {
        public string Success = "";
    }
}
