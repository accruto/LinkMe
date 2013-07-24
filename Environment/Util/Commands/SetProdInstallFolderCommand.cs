namespace LinkMe.Environment.Util.Commands
{
    public class SetProdInstallFolderCommand
        : SetInstallFolderCommand
    {
        protected override EnvironmentType EnvironmentType
        {
            get { return EnvironmentType.Production; }
        }
    }
}