using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    public enum EnumValue
    {
        No,
        Yes,
    }

    [Flags]
    public enum EnumFlagsValue1
    {
        None,
        Banana,
        Orange,
    }

    [Flags]
    public enum EnumFlagsValue2
    {
        None,
        Raspberry,
        Plum,
    }

    [Flags]
    public enum EnumFlagsValue3
    {
        None,
        Apple,
        Grape,
    }

    public class MockModelValues
    {
        public string NullStringValue { get; set; }
        public string NotNullStringValue { get; set; }
        public string[] NullStringArrayValue { get; set; }
        public string[] NotNullStringArrayValue { get; set; }
        public bool? NullBoolValue { get; set; }
        public bool? NotNullBoolValue { get; set; }
        public bool BoolValue { get; set; }
        public int? NullIntValue { get; set; }
        public int? NotNullIntValue { get; set; }
        public int IntValue { get; set; }
        public decimal? NullDecimalValue { get; set; }
        public decimal? NotNullDecimalValue { get; set; }
        public decimal DecimalValue { get; set; }
        public Guid? NullGuidValue { get; set; }
        public Guid? NotNullGuidValue { get; set; }
        public Guid GuidValue { get; set; }
        public Guid[] NullGuidArrayValue { get; set; }
        public Guid[] NotNullGuidArrayValue { get; set; }
        public DateTime? NullDateTimeValue { get; set; }
        public DateTime? NotNullDateTimeValue { get; set; }
        public PartialDate? NullPartialDateValue { get; set; }
        public PartialDate? NotNullPartialDateValue { get; set; }
        public EnumValue EnumValue { get; set; }
        public EnumValue? NullEnumValue { get; set; }
        public EnumValue? NotNullEnumValue { get; set; }
        public EnumFlagsValue1 EnumFlagsValue { get; set; }
        public EnumFlagsValue2? NullEnumFlagsValue { get; set; }
        public EnumFlagsValue3? NotNullEnumFlagsValue { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(MockModelValues))
                return false;
            return Equals((MockModelValues)obj);
        }

        public bool Equals(MockModelValues other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.NullStringValue, NullStringValue)
                && Equals(other.NotNullStringValue, NotNullStringValue)
                && other.NullStringArrayValue.NullableSequenceEqual(NullStringArrayValue)
                && other.NotNullStringArrayValue.NullableSequenceEqual(NotNullStringArrayValue)
                && other.NullBoolValue.Equals(NullBoolValue)
                && other.NotNullBoolValue.Equals(NotNullBoolValue)
                && other.BoolValue.Equals(BoolValue)
                && other.NullIntValue.Equals(NullIntValue)
                && other.NotNullIntValue.Equals(NotNullIntValue)
                && other.IntValue == IntValue
                && other.NullDecimalValue.Equals(NullDecimalValue)
                && other.NotNullDecimalValue.Equals(NotNullDecimalValue)
                && other.DecimalValue == DecimalValue
                && other.NullGuidValue.Equals(NullGuidValue)
                && other.NotNullGuidValue.Equals(NotNullGuidValue)
                && other.GuidValue.Equals(GuidValue)
                && other.NullGuidArrayValue.NullableSequenceEqual(NullGuidArrayValue)
                && other.NotNullGuidArrayValue.NullableSequenceEqual(NotNullGuidArrayValue)
                && other.NullDateTimeValue.Equals(NullDateTimeValue)
                && other.NotNullDateTimeValue.Equals(NotNullDateTimeValue)
                && other.NullPartialDateValue.Equals(NullPartialDateValue)
                && other.NotNullPartialDateValue.Equals(NotNullPartialDateValue)
                && Equals(other.EnumValue, EnumValue)
                && other.NullEnumValue.Equals(NullEnumValue)
                && other.NotNullEnumValue.Equals(NotNullEnumValue)
                && Equals(other.EnumFlagsValue, EnumFlagsValue)
                && other.NullEnumFlagsValue.Equals(NullEnumFlagsValue)
                && other.NotNullEnumFlagsValue.Equals(NotNullEnumFlagsValue);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (NullStringValue != null ? NullStringValue.GetHashCode() : 0);
                result = (result*397) ^ (NotNullStringValue != null ? NotNullStringValue.GetHashCode() : 0);
                result = (result*397) ^ (NullStringArrayValue != null ? NullStringArrayValue.GetHashCode() : 0);
                result = (result*397) ^ (NotNullStringArrayValue != null ? NotNullStringArrayValue.GetHashCode() : 0);
                result = (result*397) ^ (NullBoolValue.HasValue ? NullBoolValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullBoolValue.HasValue ? NotNullBoolValue.Value.GetHashCode() : 0);
                result = (result*397) ^ BoolValue.GetHashCode();
                result = (result*397) ^ (NullIntValue.HasValue ? NullIntValue.Value : 0);
                result = (result*397) ^ (NotNullIntValue.HasValue ? NotNullIntValue.Value : 0);
                result = (result*397) ^ IntValue;
                result = (result*397) ^ (NullDecimalValue.HasValue ? NullDecimalValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullDecimalValue.HasValue ? NotNullDecimalValue.Value.GetHashCode() : 0);
                result = (result*397) ^ DecimalValue.GetHashCode();
                result = (result*397) ^ (NullGuidValue.HasValue ? NullGuidValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullGuidValue.HasValue ? NotNullGuidValue.Value.GetHashCode() : 0);
                result = (result*397) ^ GuidValue.GetHashCode();
                result = (result*397) ^ (NullGuidArrayValue != null ? NullGuidArrayValue.GetHashCode() : 0);
                result = (result*397) ^ (NotNullGuidArrayValue != null ? NotNullGuidArrayValue.GetHashCode() : 0);
                result = (result*397) ^ (NullDateTimeValue.HasValue ? NullDateTimeValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullDateTimeValue.HasValue ? NotNullDateTimeValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NullPartialDateValue.HasValue ? NullPartialDateValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullPartialDateValue.HasValue ? NotNullPartialDateValue.Value.GetHashCode() : 0);
                result = (result*397) ^ EnumValue.GetHashCode();
                result = (result*397) ^ (NullEnumValue.HasValue ? NullEnumValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullEnumValue.HasValue ? NotNullEnumValue.Value.GetHashCode() : 0);
                result = (result*397) ^ EnumFlagsValue.GetHashCode();
                result = (result*397) ^ (NullEnumFlagsValue.HasValue ? NullEnumFlagsValue.Value.GetHashCode() : 0);
                result = (result*397) ^ (NotNullEnumFlagsValue.HasValue ? NotNullEnumFlagsValue.Value.GetHashCode() : 0);
                return result;
            }
        }

        public static MockModelValues CreateMockModelValues()
        {
            return new MockModelValues
            {
                NullStringValue = null,
                NotNullStringValue = "something",
                NullStringArrayValue = null,
                NotNullStringArrayValue = new[] { "something1", "something2" },
                NullBoolValue = null,
                NotNullBoolValue = true,
                BoolValue = true,
                NullIntValue = null,
                NotNullIntValue = 434,
                IntValue = 578,
                NullDecimalValue = null,
                NotNullDecimalValue = 235.23m,
                DecimalValue = 890.25m,
                NullGuidValue = null,
                NotNullGuidValue = Guid.NewGuid(),
                GuidValue = Guid.NewGuid(),
                NullGuidArrayValue = null,
                NotNullGuidArrayValue = new[] { Guid.NewGuid(), Guid.NewGuid() },
                NullDateTimeValue = null,
                NotNullDateTimeValue = DateTime.SpecifyKind(new DateTime(1970, 1, 12, 3, 4, 5, 6), DateTimeKind.Utc),
                NullPartialDateValue = null,
                NotNullPartialDateValue = new PartialDate(1972, 8),
                EnumValue = EnumValue.Yes,
                NullEnumValue = null,
                NotNullEnumValue = EnumValue.Yes,
                EnumFlagsValue = EnumFlagsValue1.Banana,
                NullEnumFlagsValue = null,
                NotNullEnumFlagsValue = EnumFlagsValue3.Apple | EnumFlagsValue3.Grape
            };
        }
    }

    internal class MockModelValuesConverter
        : Converter<MockModelValues>
    {
        public override void Convert(MockModelValues obj, ISetValues values)
        {
            values.SetValue("NullStringValue", obj.NullStringValue);
            values.SetValue("NotNullStringValue", obj.NotNullStringValue);
            values.SetValue("NullStringArrayValue", obj.NullStringArrayValue);
            values.SetValue("NotNullStringArrayValue", obj.NotNullStringArrayValue);
            values.SetValue("NullBoolValue", obj.NullBoolValue);
            values.SetValue("NotNullBoolValue", obj.NotNullBoolValue);
            values.SetValue("BoolValue", obj.BoolValue);
            values.SetValue("NullIntValue", obj.NullIntValue);
            values.SetValue("NotNullIntValue", obj.NotNullIntValue);
            values.SetValue("IntValue", obj.IntValue);
            values.SetValue("NullDecimalValue", obj.NullDecimalValue);
            values.SetValue("NotNullDecimalValue", obj.NotNullDecimalValue);
            values.SetValue("DecimalValue", obj.DecimalValue);
            values.SetValue("NullGuidValue", obj.NullGuidValue);
            values.SetValue("NotNullGuidValue", obj.NotNullGuidValue);
            values.SetValue("GuidValue", obj.GuidValue);
            values.SetValue("NullGuidArrayValue", obj.NullGuidArrayValue);
            values.SetValue("NotNullGuidArrayValue", obj.NotNullGuidArrayValue);
            values.SetValue("NullDateTimeValue", obj.NullDateTimeValue);
            values.SetValue("NotNullDateTimeValue", obj.NotNullDateTimeValue);
            values.SetValue("NullPartialDateValue", obj.NullPartialDateValue);
            values.SetValue("NotNullPartialDateValue", obj.NotNullPartialDateValue);
            values.SetValue("EnumValue", obj.EnumValue);
            values.SetValue("NullEnumValue", obj.NullEnumValue);
            values.SetValue("NotNullEnumValue", obj.NotNullEnumValue);
            values.SetFlagsValue(obj.EnumFlagsValue);
            values.SetFlagsValue(obj.NullEnumFlagsValue);
            values.SetFlagsValue(obj.NotNullEnumFlagsValue);
        }

        public override MockModelValues Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new MockModelValues
            {
                NullStringValue = values.GetStringValue("NullStringValue"),
                NotNullStringValue = values.GetStringValue("NotNullStringValue"),
                NullStringArrayValue = values.GetStringArrayValue("NullStringArrayValue"),
                NotNullStringArrayValue = values.GetStringArrayValue("NotNullStringArrayValue"),
                NullBoolValue = values.GetBooleanValue("NullBoolValue"),
                NotNullBoolValue = values.GetBooleanValue("NotNullBoolValue"),
                BoolValue = values.GetBooleanValue("BoolValue") ?? false,
                NullIntValue = values.GetIntValue("NullIntValue"),
                NotNullIntValue = values.GetIntValue("NotNullIntValue"),
                IntValue = values.GetIntValue("IntValue") ?? 0,
                NullDecimalValue = values.GetDecimalValue("NullDecimalValue"),
                NotNullDecimalValue = values.GetDecimalValue("NotNullDecimalValue"),
                DecimalValue = values.GetDecimalValue("DecimalValue") ?? 0,
                NullGuidValue = values.GetGuidValue("NullGuidValue"),
                NotNullGuidValue = values.GetGuidValue("NotNullGuidValue"),
                GuidValue = values.GetGuidValue("GuidValue") ?? Guid.Empty,
                NullGuidArrayValue = values.GetGuidArrayValue("NullGuidArrayValue"),
                NotNullGuidArrayValue = values.GetGuidArrayValue("NotNullGuidArrayValue"),
                NullDateTimeValue = values.GetDateTimeValue("NullDateTimeValue"),
                NotNullDateTimeValue = values.GetDateTimeValue("NotNullDateTimeValue"),
                NullPartialDateValue = values.GetPartialDateValue("NullPartialDateValue"),
                NotNullPartialDateValue = values.GetPartialDateValue("NotNullPartialDateValue"),
                EnumValue = values.GetValue<EnumValue>("EnumValue") ?? EnumValue.No,
                NullEnumValue = values.GetValue<EnumValue>("NullEnumValue"),
                NotNullEnumValue = values.GetValue<EnumValue>("NotNullEnumValue"),
                EnumFlagsValue = values.GetFlagsValue<EnumFlagsValue1>() ?? EnumFlagsValue1.None,
                NullEnumFlagsValue = values.GetFlagsValue<EnumFlagsValue2>(),
                NotNullEnumFlagsValue = values.GetFlagsValue<EnumFlagsValue3>(),
            };
        }
    }

    internal class MockModelValuesJavaScriptConverter
        : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return new MockModelValuesConverter().Deconvert(new JavaScriptValues(dictionary, serializer), null);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            new MockModelValuesConverter().Convert((MockModelValues)obj, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(MockModelValues) }; }
        }
    }
}