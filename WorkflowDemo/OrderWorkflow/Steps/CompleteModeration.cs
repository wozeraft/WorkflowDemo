using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.OrderWorkflow.Steps
{
    public class CompleteModeration : StepBody
    {
        public string Reason { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now:O}Moderation completed by {Reason}");
            return ExecutionResult.Next();
        }
    }
}
