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

namespace LinkMe.Domain.Spam.Data
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
	public partial class SpamDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSpamTextEntity(SpamTextEntity instance);
    partial void UpdateSpamTextEntity(SpamTextEntity instance);
    partial void DeleteSpamTextEntity(SpamTextEntity instance);
    partial void InsertSpammerEntity(SpammerEntity instance);
    partial void UpdateSpammerEntity(SpammerEntity instance);
    partial void DeleteSpammerEntity(SpammerEntity instance);
    partial void InsertSpammerReportEntity(SpammerReportEntity instance);
    partial void UpdateSpammerReportEntity(SpammerReportEntity instance);
    partial void DeleteSpammerReportEntity(SpammerReportEntity instance);
    #endregion
		
		public SpamDataContext() : 
				base(global::LinkMe.Domain.Properties.Settings.Default.LinkMeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SpamDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SpamDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SpamDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SpamDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SpamTextEntity> SpamTextEntities
		{
			get
			{
				return this.GetTable<SpamTextEntity>();
			}
		}
		
		public System.Data.Linq.Table<SpammerEntity> SpammerEntities
		{
			get
			{
				return this.GetTable<SpammerEntity>();
			}
		}
		
		public System.Data.Linq.Table<SpammerReportEntity> SpammerReportEntities
		{
			get
			{
				return this.GetTable<SpammerReportEntity>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SpamText")]
	public partial class SpamTextEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _pattern;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnpatternChanging(string value);
    partial void OnpatternChanged();
    #endregion
		
		public SpamTextEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pattern", DbType="NVarChar(256) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string pattern
		{
			get
			{
				return this._pattern;
			}
			set
			{
				if ((this._pattern != value))
				{
					this.OnpatternChanging(value);
					this.SendPropertyChanging();
					this._pattern = value;
					this.SendPropertyChanged("pattern");
					this.OnpatternChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Spammer")]
	public partial class SpammerEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _id;
		
		private System.Nullable<System.Guid> _userId;
		
		private string _firstName;
		
		private string _lastName;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(System.Guid value);
    partial void OnidChanged();
    partial void OnuserIdChanging(System.Nullable<System.Guid> value);
    partial void OnuserIdChanged();
    partial void OnfirstNameChanging(string value);
    partial void OnfirstNameChanged();
    partial void OnlastNameChanging(string value);
    partial void OnlastNameChanged();
    #endregion
		
		public SpammerEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userId", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				if ((this._userId != value))
				{
					this.OnuserIdChanging(value);
					this.SendPropertyChanging();
					this._userId = value;
					this.SendPropertyChanged("userId");
					this.OnuserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="NVarChar(200)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this.OnfirstNameChanging(value);
					this.SendPropertyChanging();
					this._firstName = value;
					this.SendPropertyChanged("firstName");
					this.OnfirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="NVarChar(200)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this.OnlastNameChanging(value);
					this.SendPropertyChanging();
					this._lastName = value;
					this.SendPropertyChanged("lastName");
					this.OnlastNameChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SpammerReport")]
	public partial class SpammerReportEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _id;
		
		private System.Guid _reportedByUserId;
		
		private System.DateTime _reportedTime;
		
		private System.Nullable<System.Guid> _userId;
		
		private string _firstName;
		
		private string _lastName;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(System.Guid value);
    partial void OnidChanged();
    partial void OnreportedByUserIdChanging(System.Guid value);
    partial void OnreportedByUserIdChanged();
    partial void OnreportedTimeChanging(System.DateTime value);
    partial void OnreportedTimeChanged();
    partial void OnuserIdChanging(System.Nullable<System.Guid> value);
    partial void OnuserIdChanged();
    partial void OnfirstNameChanging(string value);
    partial void OnfirstNameChanged();
    partial void OnlastNameChanging(string value);
    partial void OnlastNameChanged();
    #endregion
		
		public SpammerReportEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reportedByUserId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid reportedByUserId
		{
			get
			{
				return this._reportedByUserId;
			}
			set
			{
				if ((this._reportedByUserId != value))
				{
					this.OnreportedByUserIdChanging(value);
					this.SendPropertyChanging();
					this._reportedByUserId = value;
					this.SendPropertyChanged("reportedByUserId");
					this.OnreportedByUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reportedTime", DbType="DateTime NOT NULL")]
		public System.DateTime reportedTime
		{
			get
			{
				return this._reportedTime;
			}
			set
			{
				if ((this._reportedTime != value))
				{
					this.OnreportedTimeChanging(value);
					this.SendPropertyChanging();
					this._reportedTime = value;
					this.SendPropertyChanged("reportedTime");
					this.OnreportedTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userId", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				if ((this._userId != value))
				{
					this.OnuserIdChanging(value);
					this.SendPropertyChanging();
					this._userId = value;
					this.SendPropertyChanged("userId");
					this.OnuserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_firstName", DbType="NVarChar(200)")]
		public string firstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if ((this._firstName != value))
				{
					this.OnfirstNameChanging(value);
					this.SendPropertyChanging();
					this._firstName = value;
					this.SendPropertyChanged("firstName");
					this.OnfirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastName", DbType="NVarChar(200)")]
		public string lastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if ((this._lastName != value))
				{
					this.OnlastNameChanging(value);
					this.SendPropertyChanging();
					this._lastName = value;
					this.SendPropertyChanged("lastName");
					this.OnlastNameChanged();
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
