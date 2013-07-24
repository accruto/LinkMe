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

namespace LinkMe.Domain.Roles.Test.Resumes.Data
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
	public partial class ResumesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertResumeEntity(ResumeEntity instance);
    partial void UpdateResumeEntity(ResumeEntity instance);
    partial void DeleteResumeEntity(ResumeEntity instance);
    #endregion
		
		public ResumesDataContext() : 
				base(global::LinkMe.Domain.Roles.Test.Properties.Settings.Default.LinkMeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ResumesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ResumesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ResumesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ResumesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		internal System.Data.Linq.Table<ResumeEntity> ResumeEntities
		{
			get
			{
				return this.GetTable<ResumeEntity>();
			}
		}
		
		public System.Data.Linq.Table<Resume2Entity> Resume2Entities
		{
			get
			{
				return this.GetTable<Resume2Entity>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Resume")]
	internal partial class ResumeEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _id;
		
		private System.DateTime _lastEditedTime;
		
		private string _lensXml;
		
		private System.Guid _candidateId;
		
		private System.Nullable<System.Guid> _parsedFromFileId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(System.Guid value);
    partial void OnidChanged();
    partial void OnlastEditedTimeChanging(System.DateTime value);
    partial void OnlastEditedTimeChanged();
    partial void OnlensXmlChanging(string value);
    partial void OnlensXmlChanged();
    partial void OncandidateIdChanging(System.Guid value);
    partial void OncandidateIdChanged();
    partial void OnparsedFromFileIdChanging(System.Nullable<System.Guid> value);
    partial void OnparsedFromFileIdChanged();
    #endregion
		
		public ResumeEntity()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastEditedTime", DbType="DateTime", UpdateCheck=UpdateCheck.Never)]
		public System.DateTime lastEditedTime
		{
			get
			{
				return this._lastEditedTime;
			}
			set
			{
				if ((this._lastEditedTime != value))
				{
					this.OnlastEditedTimeChanging(value);
					this.SendPropertyChanging();
					this._lastEditedTime = value;
					this.SendPropertyChanged("lastEditedTime");
					this.OnlastEditedTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lensXml", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string lensXml
		{
			get
			{
				return this._lensXml;
			}
			set
			{
				if ((this._lensXml != value))
				{
					this.OnlensXmlChanging(value);
					this.SendPropertyChanging();
					this._lensXml = value;
					this.SendPropertyChanged("lensXml");
					this.OnlensXmlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_candidateId", DbType="UniqueIdentifier NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public System.Guid candidateId
		{
			get
			{
				return this._candidateId;
			}
			set
			{
				if ((this._candidateId != value))
				{
					this.OncandidateIdChanging(value);
					this.SendPropertyChanging();
					this._candidateId = value;
					this.SendPropertyChanged("candidateId");
					this.OncandidateIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_parsedFromFileId", DbType="UniqueIdentifier", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.Guid> parsedFromFileId
		{
			get
			{
				return this._parsedFromFileId;
			}
			set
			{
				if ((this._parsedFromFileId != value))
				{
					this.OnparsedFromFileIdChanging(value);
					this.SendPropertyChanging();
					this._parsedFromFileId = value;
					this.SendPropertyChanged("parsedFromFileId");
					this.OnparsedFromFileIdChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Resume2")]
	public partial class Resume2Entity
	{
		
		private System.Guid _id;
		
		private System.DateTime _lastEditedTime;
		
		private string _lensXml;
		
		private System.Guid _candidateId;
		
		private System.Nullable<System.Guid> _parsedFromFileId;
		
		public Resume2Entity()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="UniqueIdentifier NOT NULL")]
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
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastEditedTime", DbType="DateTime")]
		public System.DateTime lastEditedTime
		{
			get
			{
				return this._lastEditedTime;
			}
			set
			{
				if ((this._lastEditedTime != value))
				{
					this._lastEditedTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lensXml", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string lensXml
		{
			get
			{
				return this._lensXml;
			}
			set
			{
				if ((this._lensXml != value))
				{
					this._lensXml = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_candidateId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid candidateId
		{
			get
			{
				return this._candidateId;
			}
			set
			{
				if ((this._candidateId != value))
				{
					this._candidateId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_parsedFromFileId", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> parsedFromFileId
		{
			get
			{
				return this._parsedFromFileId;
			}
			set
			{
				if ((this._parsedFromFileId != value))
				{
					this._parsedFromFileId = value;
				}
			}
		}
	}
}
#pragma warning restore 1591