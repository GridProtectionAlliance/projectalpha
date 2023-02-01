using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GSF.Diagnostics;
using GSF.TimeSeries;
using GSF.TimeSeries.Adapters;
using ConnectionStringParser = GSF.Configuration.ConnectionStringParser<GSF.TimeSeries.Adapters.ConnectionStringParameterAttribute>;

namespace MyAnalytics
{
    /// <summary>
    /// Represents an example operation option enumeration.
    /// </summary>
    public enum OperationOption
    {
        [Description("Defines example operation option A.")]
        OptionA,

        [Description("Defines example operation option B.")]
        OptionB
    }

    /// <summary>
    /// Represents a simple adapter that can be used as a starting point for custom adapters.
    /// </summary>
    [Description("Example Adapter: Defines a simple adapter that can be used as a starting point for custom adapters.")]
    public class ExampleAdapter : ActionAdapterBase
    {
        private const OperationOption DefaultOperationOption = OperationOption.OptionA;

        private double m_lastResult;
        private DateTime m_lastMessageTime;

        /// <summary>
        /// Gets or sets operation option for the example adapter.
        /// </summary>
        // The [ConnectionStringParameter] attribute marks this property a configuration parameter that shows
        // up in the manager UI as an option for adapter configuration.
        [ConnectionStringParameter]
        [Description("Defines an operation option for the example adapter.")]
        [DefaultValue(DefaultOperationOption)]
        public OperationOption OperationOption { get; set; } = DefaultOperationOption;

        /// <summary>
        /// Gets the flag indicating if this adapter supports temporal processing.
        /// </summary>
        public override bool SupportsTemporalProcessing => false;

        /// <summary>
        /// Returns the detailed status of the action adapter.
        /// </summary>
        /// <remarks>
        /// Derived classes should extend status with implementation specific information.
        /// </remarks>
        public override string Status
        {
            get
            {
                StringBuilder status = new();

                status.Append(base.Status);
                status.AppendLine();

                // Provide adapter runtime status details for diagnostics
                status.AppendLine($"          Operation Option: {OperationOption}");
                status.AppendLine($"   Last Calculation Result: {m_lastResult:N3}");

                return status.ToString();
            }
        }

        /// <summary>
        /// Initializes <see cref="ExampleAdapter" />.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            ConnectionStringParser parser = new();
            parser.ParseConnectionString(ConnectionString, this);

            // Handle any custom initialization steps here
        }

        /// <summary>
        /// Publish frame of time-aligned collection of measurement values that arrived within the concentrator's defined LagTime.
        /// </summary>
        /// <param name="frame">Frame of measurements with the same timestamp that arrived within configured LagTime that are ready for processing.</param>
        /// <param name="index">Index of frame within a second ranging from zero to <c>FramesPerSecond - 1</c>.</param>
        /// <remarks>
        /// If user implemented publication function consistently exceeds available publishing time (i.e., <c>1 / FramesPerSecond</c> seconds),
        /// concentration will fall behind. A small amount of this time is required by the ActionAdapterBase for processing overhead, so actual
        /// total time available for user function process will always be slightly less than <c>1 / FramesPerSecond</c> seconds.
        /// </remarks>
        protected override void PublishFrame(IFrame frame, int index)
        {
            // Get all frame measurements
            ICollection<IMeasurement> measurements = frame.Measurements.Values;

            // Execute calculation on time-aligned measurements
            m_lastResult = measurements.Select(measurement => measurement.AdjustedValue).Sum();

            // Display a message to the console no more often than every 10 seconds
            if ((DateTime.UtcNow - m_lastMessageTime).TotalSeconds < 10.0D)
                return;

            OnStatusMessage(MessageLevel.Info, $"Processed {ProcessedMeasurements:N0} so far, last result: {m_lastResult:N3}");
            m_lastMessageTime = DateTime.UtcNow;
        }
    }
}