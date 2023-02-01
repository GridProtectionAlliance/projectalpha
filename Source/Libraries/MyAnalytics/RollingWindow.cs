using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSF.Diagnostics;
using GSF.TimeSeries;
using GSF.TimeSeries.Adapters;
using ConnectionStringParser = GSF.Configuration.ConnectionStringParser<GSF.TimeSeries.Adapters.ConnectionStringParameterAttribute>;

namespace MyAnalytics
{
    /// <summary>
    /// Represents a simple adapter that is an example of a rolling data window.
    /// </summary>
    [Description("Rolling Window: Defines a simple adapter that is an example of a rolling data window.")]
    public class RollingWindow : ActionAdapterBase
    {
        private const int DefaultTimeWindow = 10;

        // Define a data window for each configured input measurement
        private readonly Dictionary<MeasurementKey, LinkedList<double>> m_data;
        private double m_lastResult;
        private DateTime m_lastMessageTime;
        private DateTime m_lastExceptionTime;
        private int? m_sampleSize;

        /// <summary>
        /// Gets or sets the target time window in integer seconds.
        /// </summary>
        [ConnectionStringParameter]
        [Description("Defines the target time window in integer seconds.")]
        [DefaultValue(DefaultTimeWindow)]
        public int TimeWindow { get; set; } = DefaultTimeWindow;

        /// <summary>
        /// Gets sample of rolling data window, i.e., <c>TimeWindow * FramesPerSecond</c>.
        /// </summary>
        public int SampleSize => m_sampleSize ??= TimeWindow * FramesPerSecond;

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
                status.AppendLine($"  Rolling Data Time Window: {TimeWindow:N0} seconds");
                status.AppendLine($"   Last Calculation Result: {m_lastResult:N3}");

                return status.ToString();
            }
        }

        /// <summary>
        /// Creates a new <see cref="RollingWindow"/>.
        /// </summary>
        public RollingWindow()
        {
            m_data = new Dictionary<MeasurementKey, LinkedList<double>>();
        }

        /// <summary>
        /// Initializes <see cref="RollingWindow" />.
        /// </summary>
        public override void Initialize()
        {
            ConnectionStringParser parser = new();
            parser.ParseConnectionString(ConnectionString, this);

            base.Initialize();

            if (InputMeasurementKeys.Length != OutputMeasurements.Length)
                throw new InvalidOperationException($"Each input needs one defined output for the {nameof(RollingWindow)} adapter");

            // Initialize the rolling data window
            foreach (MeasurementKey measurementKey in InputMeasurementKeys)
                m_data.Add(measurementKey, new LinkedList<double>());
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
            HashSet<MeasurementKey> readySets = new();

            // Add measurement values to rolling data window
            foreach (KeyValuePair<MeasurementKey, IMeasurement> pair in frame.Measurements)
            {
                MeasurementKey key = pair.Key;
                IMeasurement measurement = pair.Value;

                if (!m_data.TryGetValue(key, out LinkedList<double> window))
                    continue;

                // TODO: Handle desired coherency checks, simple checks here just throw out bad measurements
                if (!measurement.TimestampQualityIsGood() || !measurement.ValueQualityIsGood())
                    continue;

                double value = measurement.AdjustedValue;

                if (double.IsNaN(value) || double.IsInfinity(value))
                    continue;

                // Add new measurement value to rolling data window
                window.AddLast(value);
                
                // Maintain sample size
                while (window.Count > SampleSize)
                    window.RemoveFirst();

                // Track which measurement sets are ready for processing and have new samples for processing
                if (window.Count == SampleSize)
                    readySets.Add(key);
            }

            List<IMeasurement> results = new(OutputMeasurements.Length);

            // Handle processing logic in parallel for each measurement set that is ready for processing
            Parallel.ForEach(readySets, key =>
            {
                try
                {
                    // Get data window
                    if (!m_data.TryGetValue(key, out LinkedList<double> window))
                        return;

                    // TODO: Replace the following logic with desired algorithm
                    double result = window.Average();

                    // Track result
                    int keyIndex = Array.IndexOf(InputMeasurementKeys, key);

                    if (keyIndex <= -1)
                        return;

                    lock (results)
                        results.Add(Measurement.Clone(OutputMeasurements[keyIndex], result, frame.Timestamp));
                }
                catch (Exception ex)
                {
                    string message = $"Exception while processing {nameof(RollingWindow)} adapter logic for measurement {key}: {ex.Message}";

                    // Do not display more than one error messages per second
                    if ((DateTime.UtcNow - m_lastExceptionTime).TotalSeconds >= 1.0D)
                        OnProcessException(MessageLevel.Error, new InvalidOperationException(message, ex));
                    else
                        Logger.SwallowException(ex, message);

                    m_lastExceptionTime = DateTime.UtcNow;
                }
            });

            if (results.Count > 0)
            {
                // Publish results
                OnNewMeasurements(results);

                // Track one of the last results for diagnostics
                m_lastResult = results.First().AdjustedValue;
            }

            // Display a message to the console no more often than every 10 seconds
            if ((DateTime.UtcNow - m_lastMessageTime).TotalSeconds < 10.0D)
                return;

            OnStatusMessage(MessageLevel.Info, $"Processed {ProcessedMeasurements:N0} so far, " + (readySets.Any() ? 
                $"last result: {m_lastResult:N3}" : 
                "accumulating window samples for processing..."));

            m_lastMessageTime = DateTime.UtcNow;
        }
    }
}