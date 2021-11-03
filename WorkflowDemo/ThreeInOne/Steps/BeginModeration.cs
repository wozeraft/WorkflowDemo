using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowDemo.ThreeInOne.Steps
{
    public class BeginModeration : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Moderation started");

            return ExecutionResult.Next();
        }
    }
}