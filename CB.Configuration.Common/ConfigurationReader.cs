using System.Configuration;


namespace CB.Configuration.Common
{
    public class ConfigurationReader<TConfigurationSection>
        where TConfigurationSection: ConfigurationSection, new()
    {
        #region  Constructors & Destructor
        public ConfigurationReader(string configSectionName)
        {
            ConfigurationSection = (TConfigurationSection)ConfigurationManager.GetSection(configSectionName) ??
                                   new TConfigurationSection();
        }
        #endregion


        #region  Properties & Indexers
        public TConfigurationSection ConfigurationSection { get; set; }
        #endregion
    }
}