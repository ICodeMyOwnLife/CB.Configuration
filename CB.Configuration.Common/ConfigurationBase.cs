using System.Configuration;


namespace CB.Configuration.Common
{
    public class ConfigurationBase<TConfigurationSection>
        where TConfigurationSection: ConfigurationSection, new()
    {
        #region Fields
        private readonly string _configSectionName;
        #endregion


        #region  Constructors & Destructor
        public ConfigurationBase(string configSectionName)
        {
            _configSectionName = configSectionName;
            ConfigurationSection = (TConfigurationSection)ConfigurationManager.GetSection(configSectionName) ??
                                   new TConfigurationSection();
        }
        #endregion


        #region  Properties & Indexers
        public TConfigurationSection ConfigurationSection { get; set; }
        #endregion


        #region Methods
        public void SaveExeConfiguration(ConfigurationUserLevel userLevel = ConfigurationUserLevel.None)
            => SaveConfiguration(ConfigurationManager.OpenExeConfiguration(userLevel));

        public void SaveMachineConfiguration()
            => SaveConfiguration(ConfigurationManager.OpenMachineConfiguration());

        public void SaveMappedExeConfiguration(ExeConfigurationFileMap fileMap,
            ConfigurationUserLevel userLevel = ConfigurationUserLevel.None, bool preLoad = false)
            => SaveConfiguration(ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel, preLoad));

        public void SaveMappedMachineConfiguration(ConfigurationFileMap fileMap)
            => SaveConfiguration(ConfigurationManager.OpenMappedMachineConfiguration(fileMap));
        #endregion


        #region Implementation
        private void SaveConfiguration(System.Configuration.Configuration config)
        {
            var configurationSection = config.Sections[_configSectionName] as TConfigurationSection;
            if (configurationSection != null)
            {
                config.Sections.Remove(_configSectionName);
            }

            config.Sections.Add(_configSectionName, ConfigurationSection);

            ConfigurationSection.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);
        }
        #endregion
    }
}