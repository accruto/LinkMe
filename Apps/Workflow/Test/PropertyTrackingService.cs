using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.Runtime.Tracking;

namespace LinkMe.Apps.Workflow.Test
{
    /// <summary>
    /// This class is used for getting data from a workflow at runtime during the tests.
    /// See http://www.pluralsight.com/community/blogs/matt/archive/2006/11/25/42637.aspx.
    /// </summary>
    public class PropertyTrackingService : TrackingService
    {
        private readonly Dictionary<Guid, Dictionary<string, object>> _workflowParameters = new Dictionary<Guid, Dictionary<string, object>>();

        public Dictionary<string, object> GetWorkflowProperties(Guid instanceId)
        {
            if (_workflowParameters.ContainsKey(instanceId))
                return _workflowParameters[instanceId];

            return null;
        }

        internal void UpdateProperties(Guid instanceId, Dictionary<string, object> parameters)
        {
            _workflowParameters[instanceId] = parameters;
        }

        internal void RemoveProperties(Guid instanceId)
        {
            _workflowParameters.Remove(instanceId);
        }

        protected override TrackingProfile GetProfile(Guid workflowInstanceId)
        {
            throw new NotImplementedException();
        }

        protected override TrackingProfile GetProfile(Type workflowType, Version profileVersionId)
        {
            return GetProfileCore(workflowType);
        }

        protected override TrackingChannel GetTrackingChannel(TrackingParameters parameters)
        {
            return new PropertyTrackingChannel(parameters, this);
        }

        protected override bool TryGetProfile(Type workflowType, out TrackingProfile profile)
        {
            profile = GetProfileCore(workflowType);
            return true;
        }

        protected override bool TryReloadProfile(Type workflowType, Guid workflowInstanceId, out TrackingProfile profile)
        {
            profile = null;
            return false;
        }

        /// <summary>
        /// Gets a tracking profile that tracks on every activity close
        /// and uses data extracts to get the workflow properties
        /// </summary>
        /// <param name="workflowType"></param>
        /// <returns></returns>
        private static TrackingProfile GetProfileCore(Type workflowType)
        {
            var profile = new TrackingProfile { Version = new Version(1, 0, 0) };

            var trackPoint = new ActivityTrackPoint();
            var location = new ActivityTrackingLocation(typeof(Activity), true, new[] { ActivityExecutionStatus.Closed });
            trackPoint.MatchingLocations.Add(location);

            //get the track points for all of the public properties declared in the workflow
            trackPoint.Extracts.AddRange(GetWorkflowExtracts(workflowType));

            profile.ActivityTrackPoints.Add(trackPoint);

            return profile;

        }

        /// <summary>
        /// Uses reflection to get all of the public properties declared 
        /// directly within the workflow (i.e. not in the base).  
        /// </summary>
        /// <param name="workflowType"></param>
        /// <returns></returns>
        private static IEnumerable<TrackingExtract> GetWorkflowExtracts(Type workflowType)
        {
            //this works for all properties in the derived class.  If you use a base workflow then 
            //you can use the default overload for the Getproperties which returns all public properties.
            PropertyInfo[] properties = workflowType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            return properties.Select(p => new WorkflowDataTrackingExtract(p.Name))
                .Cast<TrackingExtract>();
        }
    }
}