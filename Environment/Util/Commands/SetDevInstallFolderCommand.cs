namespace LinkMe.Environment.Util.Commands
{
    public class SetDevInstallFolderCommand
        : SetInstallFolderCommand
    {
        protected override EnvironmentType EnvironmentType
        {
            get { return EnvironmentType.Development; }
        }
    }
}