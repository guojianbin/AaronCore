using System;
using System.Configuration;

namespace Aaron.Core.Infrastructure.Config
{
    public class RegisterTypeConfig : ConfigurationSection
    {
        public static RegisterTypeConfig GetConfig()
        {
            return ConfigurationManager.GetSection("registerTypeConfig") as RegisterTypeConfig;
        }

        [ConfigurationProperty("registerType")]
        public RegisterTypeElementColletion RegisterType
        {
            get
            {
                return this["registerType"] as RegisterTypeElementColletion;
            }
        }
    }

    public class RegisterTypeElement : ConfigurationElement
    {
        [ConfigurationProperty("interface", IsRequired = true)]
        public string Interface
        {
            get
            {
                return this["interface"] as string;
            }
        }

        [ConfigurationProperty("class", IsRequired = true)]
        public string Class
        {
            get
            {
                return this["class"] as string;
            }
        }
    }

    public class RegisterTypeElementColletion : ConfigurationElementCollection
    {
        public RegisterTypeElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as RegisterTypeElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RegisterTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RegisterTypeElement)element).Interface;
        } 
    }
}