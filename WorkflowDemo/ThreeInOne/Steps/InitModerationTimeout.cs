using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class InitModerationTimeout : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Init moderation timeout");
            ((ThreeInOneData)context.Workflow.Data).ModerationTimeoutExpiredIterationCompleted = false;

            return ExecutionResult.Next();
        }
    }
}