using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class SundToSupplier : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Send order to supplier");
            return ExecutionResult.Next();
        }
    }
}