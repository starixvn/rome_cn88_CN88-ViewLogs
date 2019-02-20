namespace Alpha.ViewLogs.Site.Tests.ViewModelsForTesting
{
    using System.Text;
    using ViewModels;

    public class LogActionTypeViewModelForTesting : LogActionTypeViewModel
    {
        public LogActionTypeViewModelForTesting(int id, string name) : base(id, name)
        {
        }

        public static LogActionTypeViewModelForTesting FromLogActionTypeViewModel(LogActionTypeViewModel logActionTypeViewModel)
        {
            if (logActionTypeViewModel == null)
            {
                return null;
            }

            return new LogActionTypeViewModelForTesting(logActionTypeViewModel.Id, logActionTypeViewModel.Name);
        }

        public override bool Equals(object obj)
        {
            var compareObject = (LogActionTypeViewModel)obj;

            var isEqual = Id == compareObject.Id
                && Name == compareObject.Name;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("Id: " + Id);
            result.AppendLine("Name: " + Name);
            result.AppendLine(base.ToString());

            return result.ToString();
        }
    }
}
