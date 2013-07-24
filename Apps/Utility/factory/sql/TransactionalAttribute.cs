using System;

namespace LinkMe.Utility.Factory.Sql
{
	[AttributeUsage(AttributeTargets.Method, Inherited=true, AllowMultiple=false)]
	public class TransactionalAttribute : Attribute
	{
		TransactionType transactionType = TransactionType.JoinOrCreate;

		public TransactionalAttribute(TransactionType transactionType)
		{
			this.transactionType = transactionType;
		}

		public TransactionType TransactionType
		{
			get { return transactionType; }
		}
	}

	public enum TransactionType
	{
		None,
		JoinOrCreate
	}
}
