﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinkMe.Domain.Roles.Affiliations.Verticals.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="LinkMe")]
	internal partial class VerticalsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertVerticalEntity(VerticalEntity instance);
    partial void UpdateVerticalEntity(VerticalEntity instance);
    partial void DeleteVerticalEntity(VerticalEntity instance);
    #endregion
		
		public VerticalsDataContext() : 
				base(global::LinkMe.Domain.Roles.Properties.Settings.Default.LinkMeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public VerticalsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VerticalsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VerticalsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VerticalsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		internal System.Data.Linq.Table<VerticalEntity> VerticalEntities
		{
			get
			{
				return this.GetTable<VerticalEntity>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Vertical")]
	internal partial class VerticalEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _id;
		
		private string _name;
		
		private string _url;
		
		private string _host;
		
		private System.Nullable<int> _countryId;
		
		private string _secondaryHost;
		
		private string _externalLoginUrl;
		
		private bool _requiresExternalLogin;
		
		private bool _enabled;
		
		private string _returnEmailAddress;
		
		private string _memberServicesEmailAddress;
		
		private string _employerServicesEmailAddress;
		
		private string _emailDisplayName;
		
		private string _externalCookieDomain;
		
		private string _tertiaryHost;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(System.Guid value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnurlChanging(string value);
    partial void OnurlChanged();
    partial void OnhostChanging(string value);
    partial void OnhostChanged();
    partial void OncountryIdChanging(System.Nullable<int> value);
    partial void OncountryIdChanged();
    partial void OnsecondaryHostChanging(string value);
    partial void OnsecondaryHostChanged();
    partial void OnexternalLoginUrlChanging(string value);
    partial void OnexternalLoginUrlChanged();
    partial void OnrequiresExternalLoginChanging(bool value);
    partial void OnrequiresExternalLoginChanged();
    partial void OnenabledChanging(bool value);
    partial void OnenabledChanged();
    partial void OnreturnEmailAddressChanging(string value);
    partial void OnreturnEmailAddressChanged();
    partial void OnmemberServicesEmailAddressChanging(string value);
    partial void OnmemberServicesEmailAddressChanged();
    partial void OnemployerServicesEmailAddressChanging(string value);
    partial void OnemployerServicesEmailAddressChanged();
    partial void OnemailDisplayNameChanging(string value);
    partial void OnemailDisplayNameChanged();
    partial void OnexternalCookieDomainChanging(string value);
    partial void OnexternalCookieDomainChanged();
    partial void OntertiaryHostChanging(string value);
    partial void OntertiaryHostChanged();
    #endregion
		
		public VerticalEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true, UpdateCheck=UpdateCheck.Never)]
		public System.Guid id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="NVarChar(100) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_url", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string url
		{
			get
			{
				return this._url;
			}
			set
			{
				if ((this._url != value))
				{
					this.OnurlChanging(value);
					this.SendPropertyChanging();
					this._url = value;
					this.SendPropertyChanged("url");
					this.OnurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_host", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string host
		{
			get
			{
				return this._host;
			}
			set
			{
				if ((this._host != value))
				{
					this.OnhostChanging(value);
					this.SendPropertyChanging();
					this._host = value;
					this.SendPropertyChanged("host");
					this.OnhostChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_countryId", DbType="Int", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> countryId
		{
			get
			{
				return this._countryId;
			}
			set
			{
				if ((this._countryId != value))
				{
					this.OncountryIdChanging(value);
					this.SendPropertyChanging();
					this._countryId = value;
					this.SendPropertyChanged("countryId");
					this.OncountryIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_secondaryHost", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string secondaryHost
		{
			get
			{
				return this._secondaryHost;
			}
			set
			{
				if ((this._secondaryHost != value))
				{
					this.OnsecondaryHostChanging(value);
					this.SendPropertyChanging();
					this._secondaryHost = value;
					this.SendPropertyChanged("secondaryHost");
					this.OnsecondaryHostChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_externalLoginUrl", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string externalLoginUrl
		{
			get
			{
				return this._externalLoginUrl;
			}
			set
			{
				if ((this._externalLoginUrl != value))
				{
					this.OnexternalLoginUrlChanging(value);
					this.SendPropertyChanging();
					this._externalLoginUrl = value;
					this.SendPropertyChanged("externalLoginUrl");
					this.OnexternalLoginUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_requiresExternalLogin", DbType="Bit NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public bool requiresExternalLogin
		{
			get
			{
				return this._requiresExternalLogin;
			}
			set
			{
				if ((this._requiresExternalLogin != value))
				{
					this.OnrequiresExternalLoginChanging(value);
					this.SendPropertyChanging();
					this._requiresExternalLogin = value;
					this.SendPropertyChanged("requiresExternalLogin");
					this.OnrequiresExternalLoginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_enabled", DbType="Bit NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public bool enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((this._enabled != value))
				{
					this.OnenabledChanging(value);
					this.SendPropertyChanging();
					this._enabled = value;
					this.SendPropertyChanged("enabled");
					this.OnenabledChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_returnEmailAddress", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string returnEmailAddress
		{
			get
			{
				return this._returnEmailAddress;
			}
			set
			{
				if ((this._returnEmailAddress != value))
				{
					this.OnreturnEmailAddressChanging(value);
					this.SendPropertyChanging();
					this._returnEmailAddress = value;
					this.SendPropertyChanged("returnEmailAddress");
					this.OnreturnEmailAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_memberServicesEmailAddress", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string memberServicesEmailAddress
		{
			get
			{
				return this._memberServicesEmailAddress;
			}
			set
			{
				if ((this._memberServicesEmailAddress != value))
				{
					this.OnmemberServicesEmailAddressChanging(value);
					this.SendPropertyChanging();
					this._memberServicesEmailAddress = value;
					this.SendPropertyChanged("memberServicesEmailAddress");
					this.OnmemberServicesEmailAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_employerServicesEmailAddress", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string employerServicesEmailAddress
		{
			get
			{
				return this._employerServicesEmailAddress;
			}
			set
			{
				if ((this._employerServicesEmailAddress != value))
				{
					this.OnemployerServicesEmailAddressChanging(value);
					this.SendPropertyChanging();
					this._employerServicesEmailAddress = value;
					this.SendPropertyChanged("employerServicesEmailAddress");
					this.OnemployerServicesEmailAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_emailDisplayName", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string emailDisplayName
		{
			get
			{
				return this._emailDisplayName;
			}
			set
			{
				if ((this._emailDisplayName != value))
				{
					this.OnemailDisplayNameChanging(value);
					this.SendPropertyChanging();
					this._emailDisplayName = value;
					this.SendPropertyChanged("emailDisplayName");
					this.OnemailDisplayNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_externalCookieDomain", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string externalCookieDomain
		{
			get
			{
				return this._externalCookieDomain;
			}
			set
			{
				if ((this._externalCookieDomain != value))
				{
					this.OnexternalCookieDomainChanging(value);
					this.SendPropertyChanging();
					this._externalCookieDomain = value;
					this.SendPropertyChanged("externalCookieDomain");
					this.OnexternalCookieDomainChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tertiaryHost", DbType="NVarChar(100)", UpdateCheck=UpdateCheck.Never)]
		public string tertiaryHost
		{
			get
			{
				return this._tertiaryHost;
			}
			set
			{
				if ((this._tertiaryHost != value))
				{
					this.OntertiaryHostChanging(value);
					this.SendPropertyChanging();
					this._tertiaryHost = value;
					this.SendPropertyChanged("tertiaryHost");
					this.OntertiaryHostChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591