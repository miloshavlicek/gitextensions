﻿using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ResourceManager.Xliff
{
    [DebuggerDisplay("{Name}.{Property}={Value}")]
    public class TranslationItem : IComparable<TranslationItem>, ICloneable
    {
        public TranslationItem()
        {
        }

        public TranslationItem(string name, string property, string source)
        {
            Name = name;
            Property = property;
            Source = source;
        }

        public TranslationItem(string name, string property, string source, string value)
        {
            Name = name;
            Property = property;
            Source = source;
            Value = value;
        }

        [XmlIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        public string Property { get; set; }

        [XmlAttribute("id")]
        public string Id
        {
            get => Name + "." + Property;
            set
            {
                var vals = value.Split(new[] { '.' }, 2);
                Name = vals[0];
                Property = vals.Length > 1 ? vals[1] : "";
            }
        }

        [XmlElement("source")]
        public string Source { get; set; }

        [XmlElement("target")]
        public string Value { get; set; }

        public int CompareTo(TranslationItem other)
        {
            int val = String.Compare(Name, other.Name, StringComparison.Ordinal);
            if (val == 0)
            {
                val = String.Compare(Property, other.Property, StringComparison.Ordinal);
            }

            return val;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public TranslationItem Clone()
        {
            return new TranslationItem(Name, Property, Source, Value);
        }
    }
}