using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class CompleteModeration : StepBody
    {
        public string Reason { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            ((ThreeInOneData)context.Workflow.Data).ModerationCompleted = true;

            Console.WriteLine($"Moderation completed by {Reason}");
            return ExecutionResult.Next();
        }
    }
}