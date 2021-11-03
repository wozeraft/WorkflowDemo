using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class ModerationTimeoutExpired : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            ((ThreeInOneData)context.Workflow.Data).ModerationTimeoutExpired = true;
            ((ThreeInOneData)context.Workflow.Data).ModerationTimeoutExpiredIterationCompleted = true;
            Console.WriteLine("Moderation timeout expired");

            return ExecutionResult.Next();
        }
    }
}