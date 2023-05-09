using Discord;

namespace NetBot.Lib.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public class DescriptionAttribute : System.Attribute
    {
        private string Description;

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        public string GetDescription() => Description;
    }

    public class OptionsAttribute : System.Attribute
    {
        private string Name;
        private string Description;
        public ApplicationCommandOptionType OptionType;
        public bool isRequired;

        public OptionsAttribute(string name, string description, ApplicationCommandOptionType optionType, bool required = false)
        {
            Name = name;
            Description = description;
            OptionType = optionType;
            isRequired = required;
        }

        public string GetOptionName() => Name;
        public string GetDescription() => Description;
    }
}