using System;
using JetBrains.Annotations;
using WorkflowCore.Interface;
using WorkflowDemo.OrderWorkflow.Steps;

namespace WorkflowDemo.OrderWorkflow
{
    [UsedImplicitly]
    public class OrderWorkflow : IWorkflow<OrderWorkflowData>
    {
        public string Id => "OrderWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<OrderWorkflowData> builder)
        {
            builder
                .StartWith<CreateOrder>()
                .WaitFor("BeginModerationEvent", (data, context) => data.DemandListId.ToString(), data => DateTime.Now)
                .Then<BeginModeration>()
                .Parallel()
                .Do(
                    moderationCompletedBranch => moderationCompletedBranch
                        .WaitFor("ModerationCompletedEvent", (data, context) => data.OrderId.ToString(),
                            data => DateTime.Now)
                        .Output(data => data.ModerationCompletedReason, step => step.EventData)
                        .Output(data => data.ModerationCompleted, step => true))
                .Do(
                    moderationTimeoutBranch => moderationTimeoutBranch
                        .WaitFor("ModerationCompletedTimeoutEvent", (data, context) => data.DemandListId.ToString())
                        .Output(data => data.ModerationCompletedReason, step => step.EventData)
                        .Output(data => data.ModerationCompleted, step => true))
                .Join()
                .CancelCondition(data => data.ModerationCompleted, true)
                .Then<CompleteModeration>()
                .Input(moderation => moderation.Reason, data => data.ModerationCompletedReason)
                .Then<SundToSupplier>()
                .Input(step => step.OrderId, data => data.OrderId);
        }
    }
}
