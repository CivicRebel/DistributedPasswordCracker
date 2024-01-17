namespace QueueConsumer.Operations.Interfaces
{
    internal interface IValidationStep
    {
        IValidationStep NextStep { get; set; }
        void SetNextStep(IValidationStep step);
        void Execute();
    }
}
