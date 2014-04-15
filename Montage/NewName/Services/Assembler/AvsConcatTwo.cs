namespace NewName.Services.Assembler
{
    class AvsConcatTwo : AvsNode
    {
        public AvsNode First { get; set; }

        public AvsNode Second { get; set; }

        public override void SerializeToContext(AvsContext context)
        {
            First.SerializeToContext(context);
            Second.SerializeToContext(context);
            var script = string.Format(Format, Id, First.Id, Second.Id);
            context.AddData(script);
        }

        private string Format { get { return "{0} = {1} + {2}"; } }
    }
}