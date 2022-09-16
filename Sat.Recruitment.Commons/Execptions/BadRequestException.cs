using System;

namespace Sat.Recruitment.Common.Execptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
