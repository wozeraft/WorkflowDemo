using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class ChangeModerationTimeout : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Moderation time changed");
            ((ThreeInOneData)context.Workflow.Data).ModerationTimeoutExpiredIterationCompleted = true;

            return ExecutionResult.Next();
        }
    }
}