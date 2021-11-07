using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.OrderWorkflow.Steps
{
    public class BeginModeration : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now:O}Moderation started");

            return ExecutionResult.Next();
        }
    }
    
    public class CreateOrder : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now:O}Order created");

            return ExecutionResult.Next();
        }
    }
}
