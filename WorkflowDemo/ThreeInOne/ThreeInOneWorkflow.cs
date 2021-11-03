using System;
using WorkflowCore.Interface;
using WorkflowDemo.ThreeInOne.Steps;

namespace WorkflowDemo.ThreeInOne
{
    public class ThreeInOneWorkflow : IWorkflow<ThreeInOneData>
    {
        public string Id => "SingleWorkflowArePossible";

        public int Version => 1;

        public void Build(IWorkflowBuilder<ThreeInOneData> builder)
        {
            builder
                .StartWith<BeginModeration>()
                .Parallel()
                .Do(
                    moderationCompletedBranch => moderationCompletedBranch
                        .WaitFor("ModerationCompletedEvent", (data, context) => "0")
                        .Output(data => data.ModerationCompletedReason, step => step.EventData)
                        .Then<CompleteModeration>()
                        .Input(moderation => moderation.Reason, data => data.ModerationCompletedReason))
                .Do(
                    moderationTimeoutBranch => moderationTimeoutBranch
                        .While(data => !data.ModerationTimeoutExpired)
                        .Do(
                            moderationTimeoutLoop => moderationTimeoutLoop
                                .Then<InitModerationTimeout>()
                                .Parallel()
                                .Do(
                                    delayBranch => delayBranch
                                        .Delay(data => TimeSpan.FromSeconds(10))
                                        .Then<ModerationTimeoutExpired>())
                                .Do(
                                    moderationTimeChangedEventBranch => moderationTimeChangedEventBranch
                                        .WaitFor("ModerationTimeChangedEvent", (data, context) => "0", data => DateTime.Now)
                                        .Then<ChangeModerationTimeout>())
                                .Join()
                                .CancelCondition(data => data.ModerationTimeoutExpiredIterationCompleted, true))
                        .Then<RaiseModerationTimeoutEvent>())
                .Join()
                .CancelCondition(data => data.ModerationCompleted, true)
                .Then<SundToSupplier>();
        }
    }
}
