using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.OrderWorkflow.Steps
{
    public class SundToSupplier : StepBody
    {
        public int OrderId { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now:O}Send order {OrderId} to supplier");
            return ExecutionResult.Next();
        }
    }
}
