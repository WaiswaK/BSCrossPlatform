namespace BSCrossPlatform.Models
{
    class ModulesModel
    {
        public string Module { get; set; }
        public string ModuleImage { get; set; }

        public ModulesModel(string _module, string _moduleimage)
        {
            Module = _module;
            ModuleImage = _moduleimage;
        }
    }
}
