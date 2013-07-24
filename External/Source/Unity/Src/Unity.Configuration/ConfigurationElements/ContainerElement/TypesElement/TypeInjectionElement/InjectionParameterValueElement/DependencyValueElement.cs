﻿//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;

namespace Microsoft.Practices.Unity.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyValueElement : InjectionParameterValueElement
    {
        /// <summary>
        /// Name of the dependency to resolve.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false, DefaultValue = null)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Type of the dependency to resolve.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = false, DefaultValue = null)]
        public string TypeName
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        /// <summary>
        /// Return an instance of <see cref="InjectionParameterValue"/> based
        /// on the contents of this 
        /// </summary>
        /// <param name="targetType">Type of the containing parameter.</param>
        /// <returns>The created InjectionParameterValue, ready to pass to the container config API.</returns>
        public override InjectionParameterValue CreateParameterValue(Type targetType)
        {
            Type dependencyType;
            if( string.IsNullOrEmpty(TypeName))
            {
                dependencyType = targetType;
            }
            else
            {
                dependencyType = TypeResolver.ResolveType(TypeName);
            }
            return new ResolvedParameter(dependencyType, Name);
        }
    }
}
