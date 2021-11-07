namespace WorkflowDemo.OrderWorkflow
{
    public class OrderWorkflowData
    {
        public int OrderId { get; set; }
        public int DemandListId { get; set; }
        public string ModerationCompletedReason { get; set; }
        public bool ModerationCompleted { get; set; }
    }
}
