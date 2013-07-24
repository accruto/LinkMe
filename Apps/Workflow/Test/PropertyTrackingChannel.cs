using System.Collections.Generic;
using System.Linq;
using System.Workflow.Runtime.Tracking;

namespace LinkMe.Apps.Workflow.Test
{
    public class PropertyTrackingChannel : TrackingChannel
    {
        private readonly TrackingParameters _parameters;
        private readonly PropertyTrackingService _trackingService;

        public PropertyTrackingChannel(TrackingParameters trackingParams, PropertyTrackingService service)
        {
            _trackingService = service; 
            _parameters = trackingParams;
        }

        /// <summary>
        /// Remove the data for this workflow since this instance is done
        /// and the most recent values can come form the output parameters.
        /// </summary>
        protected override void InstanceCompletedOrTerminated()
        {
            _trackingService.RemoveProperties(_parameters.InstanceId);
        }

        /// <summary>
        /// For the activity record, get the extracts which represent the properties
        /// on the workflow.  Store these in the service so they are accessible to 
        /// anyone in the host application that gets a reference to the service.
        /// </summary>
        /// <param name="record"></param>
        protected override void Send(TrackingRecord record)
        {
            var activityRecord = record as ActivityTrackingRecord;

            if (activityRecord != null)
            {

                Dictionary<string, object> properties = activityRecord.Body.ToDictionary(
                    item => item.FieldName, item => item.Data);

                _trackingService.UpdateProperties(_parameters.InstanceId, properties);
            }
        }
    }
}