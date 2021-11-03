using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class RaiseModerationTimeoutEvent : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Publish moderation timeout event");

            HostKeeper.Host.PublishEvent("ModerationCompletedEvent", "0", "timeout");
            return ExecutionResult.Next();
        }
    }
}