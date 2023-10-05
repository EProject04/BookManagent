namespace BEWebtoon.Helpers
{
    public class CustomException : Exception
    {
        public CustomException(string exception) : base(exception)
        {

        }

        public override string ToString()
        {
            return Message;
        }
        }
    }
