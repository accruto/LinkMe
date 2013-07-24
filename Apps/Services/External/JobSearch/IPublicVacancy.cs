using System;
using System.ServiceModel;
using System.Xml.Serialization;

namespace LinkMe.Apps.Services.External.JobSearch
{
    [ServiceContract(Namespace = "http://www.dewr.gov.au/")]
    public interface IPublicVacancy
    {
        // CODEGEN: Generating message contract since message VacancyAddandMatchRequest has headers
        [OperationContract(Action = "http://www.dewr.gov.au/VacancyAddandMatch", ReplyAction = "*")]
        [XmlSerializerFormat]
        AddVacancyResponseMessage AddVacancy(AddVacancyRequestMessage request);

        // CODEGEN: Generating message contract since message VacancyUpdateRequest has headers
        [OperationContract(Action = "http://www.dewr.gov.au/VacancyUpdate", ReplyAction = "*")]
        [XmlSerializerFormat]
        UpdateVacancyResponseMessage UpdateVacancy(UpdateVacancyRequestMessage request);

        // CODEGEN: Generating message contract since message VacancyDeleteRequest has headers
        [OperationContract(Action = "http://www.dewr.gov.au/VacancyDelete", ReplyAction = "*")]
        [XmlSerializerFormat]
        DeleteVacancyResponseMessage DeleteVacancy(DeleteVacancyRequestMessage request);

        // CODEGEN: Generating message contract since message VacancyGetDetailsRequest has headers
        [OperationContract(Action = "http://www.dewr.gov.au/VacancyGetDetails", ReplyAction = "*")]
        [XmlSerializerFormat]
        GetVacancyDetailsResponseMessage GetVacancyDetails(GetVacancyDetailsRequestMessage request);
    }

    [Serializable]
    [XmlType(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Security
    {
        [XmlElement(Order = 0)] public SecurityUsernameToken UsernameToken;
        [XmlElement(Order = 1)] public SecurityBinarySecurityToken BinarySecurityToken;
        [XmlElement(Order = 2)] public SecurityChangePasswordToken ChangePasswordToken;
    }

    [Serializable]
    [XmlType(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class SecurityUsernameToken
    {
        [XmlElement(Order = 0)] public Username Username;
        [XmlElement(Order = 1)] public Password Password;
        [XmlAttribute] public string ValueType;
    }

    [Serializable]
    [XmlType(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Username
    {
        [XmlAttribute] public string Id;
        [XmlText] public string Value;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class licenceItem
    {
        [XmlElement(Order = 0)] public string licenceType;
        [XmlElement(Order = 1)] public string licenceState;
        [XmlElement(Order = 2)] public string licenceDescription;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class CommentsList
    {
        [XmlElement(Order = 0)] public string commentId;
        [XmlElement(Order = 1)] public string commentType;
        [XmlElement(Order = 2)] public string commentOrgCode;
        [XmlElement(Order = 3)] public string commentSeqNum;
        [XmlElement(Order = 4)] public string commentStatus;
        [XmlElement(Order = 5)] public string commentText;
        [XmlElement(Order = 6)] public bool commentPublicFlag;
        [XmlElement(Order = 7)] public string commentTopic;
        [XmlElement(Order = 8)] public bool commentReadOnlyFlag;
        [XmlElement(Order = 9)] public bool commentLongTextExistsFlag;
        [XmlElement(Order = 10)] public string commentUpdateUserId;
        [XmlElement(Order = 11)] public DateTime commentUpdateDate;
        [XmlElement(Order = 12)] public string commentUpdateTime;
        [XmlElement(Order = 13)] public string commentCreateUserId;
        [XmlElement(Order = 14)] public DateTime commentCreateDate;
        [XmlElement(Order = 15)] public string commentCreateTime;
        [XmlElement(Order = 16)] public string commentIntCntlNum;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class CommentsListResponse
    {
        [XmlElement(Order = 0)] public string nextBlockSequence;
        [XmlElement(Order = 1)] public string nextBlockDate;
        [XmlElement(Order = 2)] public string nextBlockTime;
        [XmlArray(Order = 3)] [XmlArrayItem(IsNullable = false)]
        public CommentsList[] commentLists;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class GetVacancyDetailsResponseBody
    {
        [XmlElement(Order = 0)] public bool jobInJeopardy;
        [XmlElement(Order = 1)] public string outcomeLevel;
        [XmlElement(Order = 2)] public int detailsIntegrityControlNumber;
        [XmlElement(Order = 3)] public int agentIntegrityControlNumber;
        [XmlElement(Order = 4)] public int contactIntegrityControlNumber;
        [XmlElement(Order = 5)] public int contactAddressIntegrityControlNumber;
        [XmlElement(Order = 6)] public string vacancyState;
        [XmlElement(Order = 7)] public string contactAddressState;
        [XmlElement(Order = 8)] public long vacancyID;
        [XmlElement(Order = 9)] public int integrityControlNumber;
        [XmlElement(Order = 10)] public string organisationCode;
        [XmlElement(Order = 11)] public string siteCode;
        [XmlElement(Order = 12)] public long employerID;
        [XmlElement(Order = 13)] public string vacancyTitle;
        [XmlElement(Order = 14)] public string occupationCode;
        [XmlElement(Order = 15)] public string vacancyLocation;
        [XmlElement(Order = 16)] public string postCode;
        [XmlElement(Order = 17)] public string VacancyDescription;
        [XmlElement(Order = 18)] public string vacancyType;
        [XmlElement(Order = 19)] public string skills;
        [XmlElement(Order = 20)] public string workType;
        [XmlElement(Order = 21)] public int positionLimit;
        [XmlElement(Order = 22)] public int referralLimit;
        [XmlElement(Order = 23)] public int referralsMade;
        [XmlElement(Order = 24)] public string duration;
        [XmlElement(Order = 25)] public DateTime expiryDate;
        [XmlElement(Order = 26)] public string howToApplyCode;
        [XmlElement(Order = 27)] public string clientType;
        [XmlElement(Order = 28)] public bool followUpFlag;
        [XmlElement(Order = 29)] public bool thirdPartyDisplayFlag;
        [XmlElement(Order = 30)] public bool kioskDisplayFlag;
        [XmlElement(Order = 31)] public string display;
        [XmlElement(Order = 32)] public string contactName;
        [XmlElement(Order = 33)] public string contactPhoneAreaCode;
        [XmlElement(Order = 34)] public string contactPhoneNumber;
        [XmlElement(Order = 35)] public string contactFaxAreaCode;
        [XmlElement(Order = 36)] public string contactFaxNumber;
        [XmlElement(Order = 37)] public string contactEmailAddress;
        [XmlElement(Order = 38)] public string contactAddressLine1;
        [XmlElement(Order = 39)] public string contactAddressLine2;
        [XmlElement(Order = 40)] public string contactAddressLine3;
        [XmlElement(Order = 41)] public string contactAddressSuburb;
        [XmlElement(Order = 42)] public string contactAddressPostCode;
        [XmlElement(Order = 43)] public string numberOfNotificationsSent;
        [XmlElement(Order = 44)] public bool onHireCompanyFlag;
        [XmlElement(Order = 45)] public string relationshipToOnHire;
        [XmlElement(Order = 46)] public bool autoMatchFlag;
        [XmlElement(Order = 47)] public string stateCode;
        [XmlElement(Order = 48)] public DateTime createDate;
        [XmlElement(Order = 49)] public DateTime modifiedDate;
        [XmlElement(Order = 50)] public string jobHoursDescription;
        [XmlElement(Order = 51)] public string userDefinedVacancyID;
        [XmlElement(Order = 52)] public bool indigenousJobFlag;
        [XmlElement(Order = 53)] public bool aecFlag;
        [XmlElement(Order = 54)] public bool disableFriendly;
        [XmlElement(Order = 55)] public string statusCode;
        [XmlElement(Order = 56)] public string employerName;
        [XmlElement(Order = 57)] public string comment;
        [XmlElement(Order = 58)] public int positionsAvailable;
        [XmlElement(Order = 59)] public int referralsAvailable;
        [XmlElement(Order = 60)] public bool jobseekerFlexFlag;
        [XmlElement(Order = 61)] public string areaDisplayCode;
        [XmlElement(Order = 62)] public long numUnregisteredPlacements;
        [XmlElement(Order = 63)] public DateTime inactivatedDate;
        [XmlElement(Order = 64)] public bool claimFlag;
        [XmlElement(Order = 65)] public string esaCode;
        [XmlElement(Order = 66)] public string newspaperMatchingArea;
        [XmlElement(Order = 67)] public string salary;
        [XmlElement(Order = 68)] public string agentName;
        [XmlElement(Order = 69)] public string agentContactName;
        [XmlElement(Order = 70)] public string agentPhoneContactNumber;
        [XmlElement(Order = 71)] public string agentFaxNum;
        [XmlElement(Order = 72)] public string agentEmailAddr;
        [XmlElement(Order = 73)] public bool ignoreSiteUpdateFlag;
        [XmlElement(Order = 74)] public bool caseloadMatchFlag;
        [XmlElement(Order = 75)] public bool anticipatedVacancyFlag;
        [XmlElement(Order = 76)] public bool migrantFriendly;
        [XmlArray(Order = 77)] [XmlArrayItem(IsNullable = false)] public licenceItem[] licences;
        [XmlElement(Order = 78)] public string anticipatedToStart;
        [XmlElement(Order = 79)] public bool JobWAEligible;
        [XmlElement(Order = 80)] public int wageAssistPlaces;
        [XmlElement(Order = 81)] public string placementType;
        [XmlElement(Order = 82)] public string visaType;
        [XmlElement(Order = 83)] public bool poppplFlag;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class GetVacancyDetailsRequestBody
    {
        [XmlElement(Order = 0)] public long vacancyID;
        [XmlElement(Order = 1)] public string userDefinedVacancyID;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class DeleteVacancyResponseBody
    {
        [XmlElement(Order = 0)] public int recordsAffected;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class DeleteVacancyRequestBody
    {
        [XmlElement(Order = 0)] public long vacancyID;
        [XmlElement(Order = 1)] public int integrityControlNumber;
        [XmlElement(Order = 2)] public bool afterHourAccess;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class UpdateVacancyResponseBody
    {
        [XmlElement(Order = 0)] public int recordsAffected;
        [XmlElement(Order = 1)] public int integrityControlNumber;
        [XmlElement(Order = 3)] public int detailsIntegrityControlNumber;
        [XmlElement(Order = 4)] public int agentIntegrityControlNumber;
        [XmlElement(Order = 5)] public int contactIntegrityControlNumber;
        [XmlElement(Order = 6)] public int contactAddressIntegrityControlNumber;
        [XmlElement(Order = 7)] public string ErrorString;
        [XmlElement(Order = 8)] public bool UpdateSuccessFlag;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class UpdateVacancyRequestBody
    {
        [XmlElement(Order = 0)] public int detailsIntegrityControlNumber;
        [XmlElement(Order = 1)] public int agentIntegrityControlNumber;
        [XmlElement(Order = 2)] public int contactIntegrityControlNumber;
        [XmlElement(Order = 3)] public int contactAddressIntegrityControlNumber;
        [XmlElement(Order = 4)] public string contactAddressState;
        [XmlElement(Order = 5)] public string vacancyType;
        [XmlElement(Order = 6)] public long vacancyID;
        [XmlElement(Order = 7)] public int integrityControlNumber;
        [XmlElement(Order = 8)] public string orgCode;
        [XmlElement(Order = 9)] public string siteCode;
        [XmlElement(Order = 10)] public long employerID;
        [XmlElement(Order = 11)] public string vacancyTitle;
        [XmlElement(Order = 12)] public string occupationCode;
        [XmlElement(Order = 13)] public string yourReference;
        [XmlElement(Order = 14)] public string vacancySuburb;
        [XmlElement(Order = 15)] public string vacancyPostcode;
        [XmlElement(Order = 16)] public string vacancyDescription;
        [XmlElement(Order = 17)] public int positionLimit;
        [XmlElement(Order = 18)] public string workType;
        [XmlElement(Order = 19)] public string duration;
        [XmlElement(Order = 20)] public int daysToExpiry;
        [XmlElement(Order = 21)] public string howToApplyCode;
        [XmlElement(Order = 22)] public bool kioskDisplayFlag;
        [XmlElement(Order = 23)] public string contactName;
        [XmlElement(Order = 24)] public string contactPhoneAreaCode;
        [XmlElement(Order = 25)] public string contactPhoneNumber;
        [XmlElement(Order = 26)] public string contactFaxAreaCode;
        [XmlElement(Order = 27)] public string contactFaxNumber;
        [XmlElement(Order = 28)] public string contactEmailAddress;
        [XmlElement(Order = 29)] public string contactAddressLine1;
        [XmlElement(Order = 30)] public string contactAddressLine2;
        [XmlElement(Order = 31)] public string contactAddressLine3;
        [XmlElement(Order = 32)] public string contactAddressSuburb;
        [XmlElement(Order = 33)] public string contactAddressPostCode;
        [XmlElement(Order = 34)] public string jobHoursDescription;
        [XmlElement(Order = 35)] public bool indigenousJobFlag;
        [XmlElement(Order = 36)] public string numberOfNotificationsSent;
        [XmlElement(Order = 37)] public string stateCode;
        [XmlElement(Order = 38)] public string salary;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class EsiMessage
    {
        [XmlElement(Order = 0)] public int id;
        [XmlElement(Order = 1)] public EsiMessageType type;
        [XmlElement(Order = 2)] public string text;
        [XmlElement(Order = 3)] public string help;
        [XmlElement(Order = 4)] public string tag;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public enum EsiMessageType
    {
        Error,
        Warning,
        Information,
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class WsgOutSoapHeader
    {
        [XmlElement(Order = 0)] public string transactionId;
        [XmlElement(Order = 1)] public EsiExecutionStatus executionStatus;
        [XmlArray(Order = 2)] [XmlArrayItem(IsNullable = false)] public EsiMessage[] Messages;
        [XmlElement(Order = 3)] public string reserved;
        [XmlAnyAttribute] public System.Xml.XmlAttribute[] AnyAttr;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public enum EsiExecutionStatus
    {
        Success,
        Failed,
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class SessionSettings
    {
        [XmlElement(Order = 0)] public string org;
        [XmlElement(Order = 1)] public string site;
        [XmlElement(Order = 2)] public DateTime date;
        [XmlElement(Order = 3)] public string currentContract;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class WsgInSoapHeader
    {
        [XmlElement(Order = 0)] public SessionSettings SessionSettings;
        [XmlAnyAttribute] public System.Xml.XmlAttribute[] AnyAttr;
    }

    [Serializable]
    [XmlType(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class SecurityChangePasswordToken
    {
        [XmlElement(Order = 0)] public Username Username;
        [XmlElement(Order = 1)] public Password OldPassword;
        [XmlElement(Order = 2)] public Password NewPassword;
    }

    [Serializable]
    [XmlType(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class Password
    {
        [XmlAttribute] public string Id;
        [XmlAttribute] public string ValueType;
        [XmlText] public string Value;
    }

    [Serializable]
    [XmlType(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
    public class SecurityBinarySecurityToken
    {
        [XmlAttribute] public string Id;
        [XmlAttribute] public string ValueType;
        [XmlAttribute] public string EncodingType;
        [XmlAttribute] public string Created;
        [XmlText] public string Value;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class JobSeekerMatchItem
    {
        [XmlElement(Order = 0)] public long JobSeekerID;
        [XmlElement(Order = 1)] public int JobMatchPreferenceID;
        [XmlElement(Order = 2)] public int Rank;
        [XmlElement(Order = 3)] public int ResumeID;
        [XmlElement(Order = 4)] public string JobTitle;
        [XmlElement(Order = 5)] public string JobSeekerSkillText;
        [XmlElement(Order = 6)] public string PreferredDeliveryMethod;
        [XmlElement(Order = 7)] public string EmailAddress;
        [XmlElement(Order = 8)] public string PhoneNumber;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class AddVacancyResponseBody
    {
        [XmlElement(Order = 0)] public long vacancyID;
        [XmlElement(Order = 1)] public long employerID;
        [XmlElement(Order = 2)] public int integrityControlNumber;
        [XmlElement(Order = 3)] public int detailsIntegrityControlNumber;
        [XmlElement(Order = 4)] public int agentIntegrityControlNumber;
        [XmlElement(Order = 5)] public int contactIntegrityControlNumber;
        [XmlElement(Order = 6)] public int contactAddressIntegrityControlNumber;
        [XmlElement(Order = 7)] public string ErrorString;
        [XmlElement(Order = 8)] public bool LodgementSuccessFlag;
        [XmlArray(Order = 9)] [XmlArrayItem(IsNullable = false)]
        public JobSeekerMatchItem[] JobSeekerMatches;
    }

    [Serializable]
    [XmlType(Namespace = "http://www.dewr.gov.au/")]
    public class AddVacancyRequestBody
    {
        [XmlElement(Order = 0)] public string contactAddressState;
        [XmlElement(Order = 1)] public string vacancyType;
        [XmlElement(Order = 2)] public string orgCode;
        [XmlElement(Order = 3)] public string siteCode;
        [XmlElement(Order = 4)] public long employerID;
        [XmlElement(Order = 5)] public string vacancyTitle;
        [XmlElement(Order = 6)] public string occupationCode;
        [XmlElement(Order = 7)] public string yourReference;
        [XmlElement(Order = 8)] public string vacancySuburb;
        [XmlElement(Order = 9)] public string vacancyPostcode;
        [XmlElement(Order = 10)] public string vacancyDescription;
        [XmlElement(Order = 11)] public int positionLimit;
        [XmlElement(Order = 12)] public string workType;
        [XmlElement(Order = 13)] public string duration;
        [XmlElement(Order = 14)] public int daysToExpiry;
        [XmlElement(Order = 15)] public string howToApplyCode;
        [XmlElement(Order = 16)] public bool kioskDisplayFlag;
        [XmlElement(Order = 17)] public string contactName;
        [XmlElement(Order = 18)] public string contactPhoneAreaCode;
        [XmlElement(Order = 19)] public string contactPhoneNumber;
        [XmlElement(Order = 20)] public string contactFaxAreaCode;
        [XmlElement(Order = 21)] public string contactFaxNumber;
        [XmlElement(Order = 22)] public string contactEmailAddress;
        [XmlElement(Order = 23)] public string contactAddressLine1;
        [XmlElement(Order = 24)] public string contactAddressLine2;
        [XmlElement(Order = 25)] public string contactAddressLine3;
        [XmlElement(Order = 26)] public string contactAddressSuburb;
        [XmlElement(Order = 27)] public string contactAddressPostCode;
        [XmlElement(Order = 28)] public string jobHoursDescription;
        [XmlElement(Order = 29)] public bool indigenousJobFlag;
        [XmlElement(Order = 30)] public string numberOfNotificationsSent;
        [XmlElement(Order = 31)] public string stateCode;
        [XmlElement(Order = 32)] public string salary;
        [XmlElement(Order = 33)] public bool returnMatchesFlag;
    } 

    [MessageContract(WrapperName = "VacancyAddandMatch", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class AddVacancyRequestMessage
    {
        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgInSoapHeader")]
        public WsgInSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "request", Order = 0)]
        public AddVacancyRequestBody Body;
    }

    [MessageContract(WrapperName = "VacancyAddandMatchResponse", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class AddVacancyResponseMessage
    {
        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgOutSoapHeader")]
        public WsgOutSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "VacancyAddandMatchResult", Order = 0)]
        public AddVacancyResponseBody Body;
    }

    [MessageContract(WrapperName = "VacancyUpdate", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class UpdateVacancyRequestMessage
    {
        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgInSoapHeader")]
        public WsgInSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "request", Order = 0)]
        public UpdateVacancyRequestBody Body;
    }

    [MessageContract(WrapperName = "VacancyUpdateResponse", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class UpdateVacancyResponseMessage
    {
        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgOutSoapHeader")]
        public WsgOutSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "VacancyUpdateResult", Order = 0)]
        public UpdateVacancyResponseBody Body;
    }

    [MessageContract(WrapperName = "VacancyDelete", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class DeleteVacancyRequestMessage
    {

        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgInSoapHeader")]
        public WsgInSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "request", Order = 0)]
        public DeleteVacancyRequestBody Body;
    }

    [MessageContract(WrapperName = "VacancyDeleteResponse", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class DeleteVacancyResponseMessage
    {

        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgOutSoapHeader")]
        public WsgOutSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "VacancyDeleteResult", Order = 0)]
        public DeleteVacancyResponseBody Body;
    }

    [MessageContract(WrapperName = "VacancyGetDetails", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class GetVacancyDetailsRequestMessage
    {

        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgInSoapHeader")]
        public WsgInSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "request", Order = 0)]
        public GetVacancyDetailsRequestBody Body;
    }

    [MessageContract(WrapperName = "VacancyGetDetailsResponse", WrapperNamespace = "http://www.dewr.gov.au/", IsWrapped = true)]
    public class GetVacancyDetailsResponseMessage
    {

        [MessageHeader(Namespace = "http://schemas.xmlsoap.org/ws/2002/04/secext")]
        public Security Security;

        [MessageHeader(Namespace = "http://www.dewr.gov.au/", Name = "WsgOutSoapHeader")]
        public WsgOutSoapHeader Header;

        [MessageBodyMember(Namespace = "http://www.dewr.gov.au/", Name = "VacancyGetDetailsResult", Order = 0)]
        public GetVacancyDetailsResponseBody Body;
    }
}