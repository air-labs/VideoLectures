using System;
using System.Text;

namespace NewName.Services.Assembler
{
    class AvsContext
    {
        public void AddData(string data)
        {
            internalData.Append(data);
            internalData.AppendLine();
        }

        public string Data
        {
            get { return internalData.ToString(); }
        }


        private readonly StringBuilder internalData = new StringBuilder();
    }
}