namespace WorkflowDemo.ThreeInOne
{
    public class ThreeInOneData
    {
        public string ModerationCompletedReason { get; set; }
        public bool ModerationTimeoutExpired { get; set; }
        public bool ModerationCompleted { get; set; }
        public bool ModerationTimeoutExpiredIterationCompleted { get; set; }
    }
}
